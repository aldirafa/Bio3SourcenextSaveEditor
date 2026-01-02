Imports System.Globalization
Imports Library

Public Class ItemModifyWindow
    Public Item As Item

    Public Sub New(item As Item)
        InitializeComponent()
        Me.Item = item
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As RoutedEventArgs)
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub ItemNameComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ItemNameComboBox.SelectionChanged
        If IsNothing(Item) OrElse IsNothing(ItemNameComboBox.SelectedValue) Then Return

        ' SelectedValuePath is "Key" so we can set SelectedValue directly to the enum value.
        Dim selectedItemID = CType(ItemNameComboBox.SelectedValue, ItemID)
        If Item.ID <> selectedItemID Then
            Item.ID = selectedItemID
            SetInteractableItemIfNeeded()
        End If
    End Sub

    Private Sub AmmoQuantityBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles AmmoQuantityBox.TextChanged
        If IsNothing(Item) Then Return

        Dim ammo As Integer
        If Integer.TryParse(AmmoQuantityBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, ammo) Then
            If ammo < 0 OrElse ammo > 255 Then Return
            If Item.Ammo <> ammo Then
                Item.Ammo = ammo
                SetInteractableItemIfNeeded()
            End If
        End If
    End Sub

    Private Sub ItemAttributesComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ItemAttributesComboBox.SelectionChanged
        If IsNothing(Item) OrElse ItemAttributesComboBox.SelectedValue Is Nothing Then Return

        Dim selectedAttribute = CType(ItemAttributesComboBox.SelectedValue, ItemAttribute)
        If Item.Attribute <> selectedAttribute Then
            Item.Attribute = selectedAttribute
            SetInteractableItemIfNeeded()
        End If
    End Sub

    Private Sub FluentWindow_Loaded(sender As Object, e As RoutedEventArgs)
        If IsNothing(Item) Then Return

        ' Populate name combo and select by SelectedValue (faster than LINQ search)
        ItemNameComboBox.ItemsSource = Utils.GetAllItems()
        ItemNameComboBox.DisplayMemberPath = "Value"
        ItemNameComboBox.SelectedValuePath = "Key"
        ItemNameComboBox.SelectedValue = Item.ID

        ' Initialize ammo text with invariant culture
        AmmoQuantityBox.Text = Item.Ammo.ToString(CultureInfo.InvariantCulture)

        ' Populate attributes combo and select by SelectedValue (faster than LINQ search)
        ItemAttributesComboBox.ItemsSource = Utils.GetAllAttributes()
        ItemAttributesComboBox.DisplayMemberPath = "Value"
        ItemAttributesComboBox.SelectedValuePath = "Key"
        ItemAttributesComboBox.SelectedValue = Item.Attribute

        SetInteractableItemIfNeeded()
    End Sub

    Private Sub SetInteractableItemIfNeeded()
        If ItemInteractableControl Is Nothing OrElse IsNothing(Item) Then Return
        If Not Object.Equals(ItemInteractableControl.Item, Item) Then
            ItemInteractableControl.Item = Item
        End If
    End Sub
End Class
