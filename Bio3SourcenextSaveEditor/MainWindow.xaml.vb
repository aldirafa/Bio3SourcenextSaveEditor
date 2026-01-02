Imports Library
Imports System.Windows.Input
Imports System.Windows.Media.Effects
Imports System.Windows.Media
Imports System.Threading.Tasks

Partial Class MainWindow
    Inherits Wpf.Ui.Controls.FluentWindow

    Private saveGame As Library.SaveGame

    ' Cache delegates to avoid allocating new delegate instances for every item
    Private ReadOnly ItemClickHandler As RoutedEventHandler = AddressOf ItemInteractable_Click
    Private ReadOnly ItemMouseEnterHandler As MouseEventHandler = AddressOf ItemInteractable_OnMouseEnter
    Private ReadOnly ItemMouseLeaveHandler As MouseEventHandler = AddressOf ItemInteractable_OnMouseLeave

    ' Reuse a frozen DropShadowEffect to avoid reallocations on every MouseEnter
    Private Shared ReadOnly HoverShadow As DropShadowEffect = CreateFrozenHoverShadow()

    Private Async Sub LoadButton_Click(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog As New Microsoft.Win32.OpenFileDialog With {
            .Filter = "Resident Evil 3 Save Files (*.bio3)|*.bio3|All files (*.*)|*.*"
        }
        If openFileDialog.ShowDialog() = True Then
            Dim fileName = openFileDialog.FileName
            StatusTextBlock.Text = "Loading save file..."

            ' Disable controls during load
            SetControlsEnabled(False)

            Dim loadedSave As Library.SaveGame = Nothing
            Try
                ' Load the save file on a background thread to avoid blocking the UI
                loadedSave = Await Task.Run(Function()
                                                Return New Library.SaveGame(fileName)
                                            End Function)

                StatusTextBlock.Text = "Preparing UI..."

                saveGame = loadedSave

                ' Ensure the status text renders before heavy UI work
                Await Task.Yield()

                Await DisplayInfoAsync()

                StatusTextBlock.Text = "Ready"
            Catch ex As Exception
                StatusTextBlock.Text = $"Error loading/preparing save: {ex.Message}"
            Finally
                ' Re-enable controls after load finishes (success or failure)
                SetControlsEnabled(True)
            End Try
        End If
    End Sub

    Private Async Function DisplayInfoAsync() As Task
        If saveGame Is Nothing Then Return

        SaveButton.IsEnabled = saveGame IsNot Nothing

        ' Use a typed array of panels for better performance and clarity
        Dim panels As Panel() = {JillsInventoryPanel, JillsItemBoxPanel, CarlosInventoryPanel, CarlosItemBoxPanel}

        For Each control As Panel In panels
            control.Children.Clear()

            Dim source As Inventory = Nothing
            Dim panelName As String = ""
            If control Is JillsInventoryPanel Then
                source = saveGame.JillsInventory
                panelName = "Jill's Inventory"
            ElseIf control Is JillsItemBoxPanel Then
                source = saveGame.JillsItemBox
                panelName = "Jill's Item Box"
            ElseIf control Is CarlosInventoryPanel Then
                source = saveGame.CarlosInventory
                panelName = "Carlos's Inventory"
            ElseIf control Is CarlosItemBoxPanel Then
                source = saveGame.CarlosItemBox
                panelName = "Carlos's Item Box"
            End If

            If source Is Nothing Then Continue For

            StatusTextBlock.Text = $"Preparing {panelName}..."

            ' Iterate typed item collection and reuse cached delegates/effect
            For Each item As Item In source.GetItems()
                Dim itemControl As New ItemInteractable With {
                    .Item = item,
                    .Cursor = Cursors.Hand
                }

                AddHandler itemControl.Click, ItemClickHandler
                AddHandler itemControl.MouseEnter, ItemMouseEnterHandler
                AddHandler itemControl.MouseLeave, ItemMouseLeaveHandler

                control.Children.Add(itemControl)
            Next

            ' Yield to allow UI to update the StatusTextBlock between panels
            Await Task.Yield()
        Next

        StatusTextBlock.Text = "Initializing Save Data panel..."

        ' Assign SaveData to the SaveDataInfoPanel (this will trigger PrepareControlDisplays via the dependency property)
        SaveDataInfoPanel.SaveData = saveGame

        ' Yield again so the UI can reflect the final updates
        Await Task.Yield()
    End Function

    Private Sub ItemInteractable_Click(sender As Object, e As RoutedEventArgs)
        Dim itemControl = TryCast(sender, ItemInteractable)
        If itemControl Is Nothing Then Return

        Dim originalItem = itemControl.Item
        Dim modifyWindow As New ItemModifyWindow(originalItem)

        If modifyWindow.ShowDialog() = True Then
            ' Update the control's item reference with the modified item.
            ' If underlying Inventory stores references, this will reflect in saveGame as well.
            itemControl.Item = modifyWindow.Item

            ' Also write the modified item back into the saveGame's bytes so it persists on disk.
            If saveGame Is Nothing Then Return

            Dim parentPanel = TryCast(VisualTreeHelper.GetParent(itemControl), Panel)
            If parentPanel Is Nothing Then
                ' Try a fallback: find which panel contains this control
                For Each p As Panel In New Panel() {JillsInventoryPanel, JillsItemBoxPanel, CarlosInventoryPanel, CarlosItemBoxPanel}
                    If p.Children.Contains(itemControl) Then
                        parentPanel = p
                        Exit For
                    End If
                Next
            End If

            If parentPanel Is Nothing Then Return

            Dim itemIndex As Integer = parentPanel.Children.IndexOf(itemControl)
            If itemIndex < 0 Then Return

            Try
                If parentPanel Is JillsInventoryPanel Then
                    Dim inv = saveGame.JillsInventory
                    inv.SetItem(itemIndex, modifyWindow.Item)
                    saveGame.JillsInventory = inv
                ElseIf parentPanel Is JillsItemBoxPanel Then
                    Dim inv = saveGame.JillsItemBox
                    inv.SetItem(itemIndex, modifyWindow.Item)
                    saveGame.JillsItemBox = inv
                ElseIf parentPanel Is CarlosInventoryPanel Then
                    Dim inv = saveGame.CarlosInventory
                    inv.SetItem(itemIndex, modifyWindow.Item)
                    saveGame.CarlosInventory = inv
                ElseIf parentPanel Is CarlosItemBoxPanel Then
                    Dim inv = saveGame.CarlosItemBox
                    inv.SetItem(itemIndex, modifyWindow.Item)
                    saveGame.CarlosItemBox = inv
                End If
            Catch ex As Exception
                StatusTextBlock.Text = $"Error updating save data in memory: {ex.Message}"
            End Try
        End If
    End Sub

    Private Sub ItemInteractable_OnMouseEnter(sender As Object, e As MouseEventArgs)
        Dim itemControl = TryCast(sender, ItemInteractable)
        If itemControl Is Nothing Then Return

        ' Assign the shared frozen effect (no mutation)
        itemControl.Effect = HoverShadow
    End Sub

    Private Sub ItemInteractable_OnMouseLeave(sender As Object, e As MouseEventArgs)
        Dim itemControl = TryCast(sender, ItemInteractable)
        If itemControl Is Nothing Then Return

        itemControl.Effect = Nothing
    End Sub

    Private Shared Function CreateFrozenHoverShadow() As DropShadowEffect
        Dim effect As New DropShadowEffect With {
            .Color = Colors.Yellow,
            .BlurRadius = 10,
            .ShadowDepth = 0,
            .Opacity = 1.0
        }
        If effect.CanFreeze Then
            effect.Freeze()
        End If
        Return effect
    End Function

    Private Async Sub SaveButton_Click(sender As Object, e As RoutedEventArgs)
        If saveGame Is Nothing Then
            StatusTextBlock.Text = "No save loaded to save."
            Return
        End If

        StatusTextBlock.Text = "Saving..."
        SetControlsEnabled(False)

        Try
            Await Task.Run(Sub()
                               saveGame.Save()
                           End Sub)

            StatusTextBlock.Text = $"Saved: {saveGame.File.FullName}"
        Catch ex As Exception
            StatusTextBlock.Text = $"Error saving save: {ex.Message}"
        Finally
            SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub SetControlsEnabled(enabled As Boolean)
        ' Keep StatusTextBlock enabled so user can see progress; disable interactive UI
        LoadButton.IsEnabled = enabled
        If Me.FindName("SaveButton") IsNot Nothing Then
            SaveButton.IsEnabled = enabled
        End If

        SaveDataInfoPanel.IsEnabled = enabled
        JillsInventoryPanel.IsEnabled = enabled
        JillsItemBoxPanel.IsEnabled = enabled
        CarlosInventoryPanel.IsEnabled = enabled
        CarlosItemBoxPanel.IsEnabled = enabled
    End Sub

    Private Sub AppMainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles AppMainWindow.Loaded
        CreditsTextBlock.Text = String.Join(Environment.NewLine, {"Credits:", Library.Utils.GetCreditsString()})
    End Sub
End Class