Option Strict On
Option Explicit On

Imports Wpf.Ui.Controls
Imports System.Windows

''' <summary>
''' Provides a user control for displaying and editing save data information.
''' </summary>
Class SaveDataInformation
    Inherits UserControl

    ' Replace public field with a DependencyProperty so it can be set from XAML/designer
    Public Shared ReadOnly SaveDataProperty As DependencyProperty = DependencyProperty.Register(NameOf(SaveData), GetType(Library.SaveGame), GetType(SaveDataInformation), New PropertyMetadata(Nothing, AddressOf OnSaveDataChanged))

    Public Property SaveData As Library.SaveGame
        Get
            Return CType(GetValue(SaveDataProperty), Library.SaveGame)
        End Get
        Set(value As Library.SaveGame)
            SetValue(SaveDataProperty, value)
        End Set
    End Property

    Private Shared Sub OnSaveDataChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim control = TryCast(d, SaveDataInformation)
        If control Is Nothing Then Return
        control.PrepareControlDisplays()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SaveDataInformation"/> class.
    ''' </summary>
    ''' <param name="d">A <see cref="Library.SaveGame"/> object containing the save data.</param>
    Public Sub New(d As Library.SaveGame)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SaveData = d
        ' PrepareControlDisplays is called from the property-changed callback
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SaveDataInformation"/> class.
    ''' </summary>
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Prepares and initializes the control displays based on the current SaveData.
    ''' </summary>
    Private Sub PrepareControlDisplays()
        If SaveData Is Nothing Then Return

        ' Cache repeated item sources
        Dim allItems = Library.Utils.GetAllItems()
        Dim playAreas = Library.Utils.GetAllPlayArea()
        Dim playerChars = Library.Utils.GetAllPlayerCharacter()
        Dim jillOutfits = Library.Utils.GetAllJillOutfit()

        ' Initialize ComboBoxes using a small helper to reduce duplication
        ConfigureComboBox(PlayAreaComboBox, playAreas)
        ConfigureComboBox(PlayerCharacterComboBox, playerChars)
        ConfigureComboBox(JillsOutfitComboBox, jillOutfits)
        ConfigureComboBox(JillsWeaponComboBox, allItems)
        ConfigureComboBox(CarlosWeaponComboBox, allItems)

        ' Initialize controls from SaveData
        SaveFileNameTextBlock.Text = SaveData.File?.Name
        DifficultyToggleSwitch.IsChecked = SaveData.Difficulty
        UnlockedEpiloguesNumberBox.Value = SaveData.EpiloguesUnlocked
        NumberOfSavesNumberBox.Value = SaveData.NumOfSaves

        ' Use SelectedValue for typed selections to avoid unnecessary boxing/unboxing where possible
        PlayAreaComboBox.SelectedValue = SaveData.Area
        PlayerCharacterComboBox.SelectedValue = SaveData.Character
        JillsOutfitComboBox.SelectedValue = SaveData.Outfit
        JillsWeaponComboBox.SelectedValue = SaveData.JillsWeapon
        JillsWeaponDisplay.Item = New Library.Item(SaveData.JillsWeapon, 1, Library.ItemAttribute.NoAmmoDisplay)
        JillsSidepackToggleSwitch.IsChecked = SaveData.JillSidepackEnabled
        CarlosWeaponComboBox.SelectedValue = SaveData.CarlosWeapon
        CarlosWeaponDisplay.Item = New Library.Item(SaveData.CarlosWeapon, 1, Library.ItemAttribute.NoAmmoDisplay)
        CarlosSidepackToggleSwitch.IsChecked = SaveData.CarlosSidepack
        PoisonedStatusToggleSwitch.IsChecked = SaveData.Poisoned

        ' Add handlers (remove first to avoid duplicates if PrepareControlDisplays is called more than once)
        RemoveHandler PlayAreaComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        RemoveHandler PlayerCharacterComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        RemoveHandler JillsOutfitComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        RemoveHandler JillsWeaponComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        RemoveHandler CarlosWeaponComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged

        AddHandler PlayAreaComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        AddHandler PlayerCharacterComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        AddHandler JillsOutfitComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        AddHandler JillsWeaponComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        AddHandler CarlosWeaponComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged

        Dim toggleHandler As RoutedEventHandler = AddressOf ToggleSwitch_Checked
        RemoveHandler DifficultyToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        RemoveHandler DifficultyToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)
        RemoveHandler JillsSidepackToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        RemoveHandler JillsSidepackToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)
        RemoveHandler CarlosSidepackToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        RemoveHandler CarlosSidepackToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)
        RemoveHandler PoisonedStatusToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        RemoveHandler PoisonedStatusToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)

        AddHandler DifficultyToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        AddHandler DifficultyToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)
        AddHandler JillsSidepackToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        AddHandler JillsSidepackToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)
        AddHandler CarlosSidepackToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        AddHandler CarlosSidepackToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)
        AddHandler PoisonedStatusToggleSwitch.Checked, CType(toggleHandler, RoutedEventHandler)
        AddHandler PoisonedStatusToggleSwitch.Unchecked, CType(toggleHandler, RoutedEventHandler)

        RemoveHandler UnlockedEpiloguesNumberBox.ValueChanged, AddressOf NumberBox_ValueChanged
        RemoveHandler NumberOfSavesNumberBox.ValueChanged, AddressOf NumberBox_ValueChanged
        AddHandler UnlockedEpiloguesNumberBox.ValueChanged, AddressOf NumberBox_ValueChanged
        AddHandler NumberOfSavesNumberBox.ValueChanged, AddressOf NumberBox_ValueChanged
    End Sub

    ''' <summary>
    ''' Configures a ComboBox with the given items source.
    ''' </summary>
    ''' <param name="cb">The ComboBox to configure.</param>
    ''' <param name="itemsSource">The items source to set.</param>
    Private Sub ConfigureComboBox(cb As ComboBox, itemsSource As IEnumerable)
        If cb Is Nothing Then Return
        cb.ItemsSource = itemsSource
        cb.DisplayMemberPath = "Value"
        cb.SelectedValuePath = "Key"
    End Sub

    ''' <summary>
    ''' Handles selection changes in ComboBoxes and updates the SaveData accordingly.
    ''' </summary>
    ''' <param name="sender">The ComboBox that triggered the event.</param>
    ''' <param name="e">The event arguments.</param>
    Private Sub ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim sd = SaveData
        If sd Is Nothing Then Return
        Dim comboBox = TryCast(sender, ComboBox)
        If comboBox Is Nothing Then Return

        If ReferenceEquals(comboBox, PlayAreaComboBox) Then
            Dim val = PlayAreaComboBox.SelectedValue
            If val IsNot Nothing Then sd.Area = CType(val, Library.PlayArea)
        ElseIf ReferenceEquals(comboBox, PlayerCharacterComboBox) Then
            Dim val = PlayerCharacterComboBox.SelectedValue
            If val IsNot Nothing Then sd.Character = CType(val, Library.PlayerCharacter)
        ElseIf ReferenceEquals(comboBox, JillsOutfitComboBox) Then
            Dim val = JillsOutfitComboBox.SelectedValue
            If val IsNot Nothing Then sd.Outfit = CType(val, Library.JillOutfit)
        ElseIf ReferenceEquals(comboBox, JillsWeaponComboBox) Then
            Dim val = JillsWeaponComboBox.SelectedValue
            If val IsNot Nothing Then
                sd.JillsWeapon = CType(val, Library.ItemID)
                JillsWeaponDisplay.Item = New Library.Item(sd.JillsWeapon, 1, Library.ItemAttribute.NoAmmoDisplay)
            End If
        ElseIf ReferenceEquals(comboBox, CarlosWeaponComboBox) Then
            Dim val = CarlosWeaponComboBox.SelectedValue
            If val IsNot Nothing Then
                sd.CarlosWeapon = CType(val, Library.ItemID)
                CarlosWeaponDisplay.Item = New Library.Item(sd.CarlosWeapon, 1, Library.ItemAttribute.NoAmmoDisplay)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles toggle switch checked/unchecked events and updates the SaveData accordingly.
    ''' </summary>
    ''' <param name="sender">The ToggleSwitch that triggered the event.</param>
    ''' <param name="e">The event arguments.</param>
    Private Sub ToggleSwitch_Checked(sender As Object, e As RoutedEventArgs)
        Dim sd = SaveData
        If sd Is Nothing Then Return
        Dim toggleSwitch = TryCast(sender, Wpf.Ui.Controls.ToggleSwitch)
        If toggleSwitch Is Nothing Then Return

        Dim isChecked = toggleSwitch.IsChecked.GetValueOrDefault(False)

        If ReferenceEquals(toggleSwitch, DifficultyToggleSwitch) Then
            sd.Difficulty = isChecked
        ElseIf ReferenceEquals(toggleSwitch, JillsSidepackToggleSwitch) Then
            sd.JillSidepackEnabled = isChecked
        ElseIf ReferenceEquals(toggleSwitch, CarlosSidepackToggleSwitch) Then
            sd.CarlosSidepack = isChecked
        ElseIf ReferenceEquals(toggleSwitch, PoisonedStatusToggleSwitch) Then
            sd.Poisoned = isChecked
        End If
    End Sub

    ''' <summary>
    ''' Handles value changes in NumberBoxes and updates the SaveData accordingly.
    ''' </summary>
    ''' <param name="sender">The NumberBox that triggered the event.</param>
    ''' <param name="e">The event arguments.</param>
    Private Sub NumberBox_ValueChanged(sender As Object, e As NumberBoxValueChangedEventArgs)
        Dim sd = SaveData
        If sd Is Nothing Then Return
        Dim numberBox = TryCast(sender, Wpf.Ui.Controls.NumberBox)
        If numberBox Is Nothing Then Return
        If Not e.NewValue.HasValue Then Return
        Dim newVal As Double = e.NewValue.Value

        If ReferenceEquals(numberBox, UnlockedEpiloguesNumberBox) Then
            sd.EpiloguesUnlocked = CByte(Math.Max(Byte.MinValue, Math.Min(Byte.MaxValue, CInt(Math.Truncate(newVal)))))
        ElseIf ReferenceEquals(numberBox, NumberOfSavesNumberBox) Then
            sd.NumOfSaves = CByte(Math.Max(Byte.MinValue, Math.Min(Byte.MaxValue, CInt(Math.Truncate(newVal)))))
        End If
    End Sub
End Class
