''' <summary>
''' Defines the inventory of a player character, containing a fixed number of items.
''' </summary>
Public Class Inventory
    ''' <summary>
    ''' The fixed size of the inventory (number of items).
    ''' </summary>
    Private InventorySize As Integer = 10
    ''' <summary>
    ''' The size of each item in bytes.
    ''' </summary>
    Private ItemSize As Integer = 4
    ''' <summary>
    ''' The array of <see cref="Item"/> objects in the inventory.
    ''' </summary>
    Private Items(InventorySize - 1) As Item

    ''' <summary>
    ''' Initializes a new instance of the Inventory class from a byte array.
    ''' </summary>
    ''' <param name="Whose">Whose inventory is being created.</param>
    ''' <param name="Items">The byte array containing the item data.</param>
    Public Sub New(Items() As Byte)
        InventorySize = Items.Length \ ItemSize
        ReDim Me.Items(InventorySize - 1)
        For i = 0 To InventorySize - 1
            Dim itemBytes(3) As Byte
            Array.Copy(Items, i * ItemSize, itemBytes, 0, ItemSize)
            Me.Items(i) = New Item(itemBytes)
        Next
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the Inventory class from an array of Item objects.
    ''' </summary>
    ''' <param name="Whose">Whose inventory is being created.</param>
    ''' <param name="Items">The array of Item objects.</param>
    Public Sub New(Items As Item())
        Me.Items = Items
    End Sub

    ''' <summary>
    ''' Converts the Inventory to a byte array.
    ''' </summary>
    ''' <returns>The byte array representation of the inventory.</returns>
    Public Function ToBytes() As Byte()
        Dim result(InventorySize * ItemSize - 1) As Byte
        For i = 0 To InventorySize - 1
            Dim itemBytes As Byte() = Items(i).ToBytes()
            Array.Copy(itemBytes, 0, result, i * ItemSize, ItemSize)
        Next
        Return result
    End Function

    ''' <summary>
    ''' Gets the array of Item objects in the inventory.
    ''' </summary>
    ''' <returns>The array of Item objects.</returns>
    Public Function GetItems() As Item()
        Return Items
    End Function

    ''' <summary>
    ''' Sets the array of Item objects in the inventory.
    ''' </summary>
    ''' <param name="NewItems">The array of Item objects to set.</param>
    ''' <returns>True if the items were set successfully; otherwise, false.</returns>
    ''' <exception cref="ArgumentException">Thrown when the length of NewItems does not match <see cref="InventorySize"/>.</exception>
    Public Function SetItems(NewItems As Item()) As Boolean
        If NewItems.Length <> InventorySize Then
            Throw New ArgumentException($"NewItems must have exactly {InventorySize} items.")
        End If
        Me.Items = NewItems
        Return True
    End Function

    ''' <summary>
    ''' Sets an individual item in the inventory at the specified index.
    ''' </summary>
    ''' <param name="index">The index of the item to set.</param>
    ''' <param name="NewItem">The new item to set.</param>
    ''' <returns>True if the item was set successfully; otherwise, false.</returns>
    ''' <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
    Public Function SetItem(index As Integer, NewItem As Item) As Boolean
        If index < 0 Or index >= InventorySize Then
            Throw New ArgumentOutOfRangeException($"Index must be between 0 and {InventorySize - 1}.")
        End If
        Me.Items(index) = NewItem
        Return True
    End Function

    ''' <summary>
    ''' Sets an individual item in the inventory at the specified index using a byte array.
    ''' </summary>
    ''' <param name="index">The index of the item to set.</param>
    ''' <param name="NewItem">The new item to set.</param>
    ''' <returns>True if the item was set successfully; otherwise, false.</returns>
    ''' <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
    Public Function SetItem(index As Integer, NewItem As Byte()) As Boolean
        If index < 0 Or index >= InventorySize Then
            Throw New ArgumentOutOfRangeException($"Index must be between 0 and {InventorySize - 1}.")
        End If
        Me.Items(index) = New Item(NewItem)
        Return True
    End Function

    ''' <summary>
    ''' Sets an individual item in the inventory at the specified index using item properties.
    ''' </summary>
    ''' <param name="index">The index of the item to set.</param>
    ''' <param name="ItemID">The ID of the item.</param>
    ''' <param name="Ammo">The ammo count of the item.</param>
    ''' <param name="Attribute">The attributes of the item.</param>
    ''' <returns>True if the item was set successfully; otherwise, false.</returns>
    ''' <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
    Public Function SetItem(index As Integer, ItemID As ItemID, Ammo As Byte, Attribute As ItemAttribute) As Boolean
        If index < 0 Or index >= InventorySize Then
            Throw New ArgumentOutOfRangeException($"Index must be between 0 and {InventorySize - 1}.")
        End If
        Me.Items(index) = New Item(ItemID, Ammo, Attribute)
        Return True
    End Function
End Class

