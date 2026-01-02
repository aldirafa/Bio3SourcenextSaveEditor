Imports System.ComponentModel

''' <summary>
''' Defines an item with its ID, ammo count, and attribute.
''' </summary>
Public Structure Item
    ' 4 bytes
    ''' <summary>
    ''' The ID of the item.
    ''' </summary>
    <Description("The ID of the item.")>
    Public ID As ItemID
    ''' <summary>
    ''' The ammo count of the item.
    ''' </summary>
    <Description("The ammo count of the item.")>
    Public Ammo As Byte
    ''' <summary>
    ''' The attribute of the item.
    ''' </summary>
    <Description("The attribute of the item.")>
    Public Attribute As ItemAttribute
    ''' <summary>
    ''' The fourth byte is always 0x00.
    ''' </summary>
    <Description("The fourth byte, usually 0x00.")>
    Public FourthByte As Byte

    ''' <summary>
    ''' Initializes a new instance of the Item structure.
    ''' </summary>
    ''' <param name="id">The ID of the item.</param>
    ''' <param name="ammo">The ammo count of the item.</param>
    ''' <param name="attribute">The attribute of the item.</param>
    <Description("Initializes a new instance of the Item structure.")>
    Public Sub New(id As ItemID, ammo As Byte, attribute As ItemAttribute)
        Me.ID = id
        Me.Ammo = ammo
        Me.Attribute = attribute
        Me.FourthByte = &H0
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the Item structure from a byte array.
    ''' </summary>
    ''' <param name="bytes">The byte array representing the item.</param>
    ''' <exception cref="InvalidCastException">Thrown when the byte array is not exactly 4 bytes long.</exception>
    ''' <exception cref="InvalidCastException">Thrown when the fourth byte is not 0x00.</exception>
    <Description("Initializes a new instance of the Item structure from a byte array.")>
    Public Sub New(bytes As Byte())
        If bytes.Length <> 4 Then
            Throw New InvalidCastException($"Byte array for Item must be exactly 4 bytes long. Current bytes data: {BitConverter.ToString(bytes)}.")
        End If

        ID = bytes(0)
        Ammo = bytes(1)
        Attribute = bytes(2)
        FourthByte = bytes(3)
    End Sub

    ''' <summary>
    ''' Converts the Item structure to a byte array.
    ''' </summary>
    ''' <returns>A byte array representing the item.</returns>
    ''' <remarks>The byte array length will always be 4 bytes.</remarks>
    <Description("Converts the Item structure to a byte array.")>
    Public Function ToBytes() As Byte()
        Return New Byte() {CByte(ID), Ammo, CByte(Attribute), FourthByte}
    End Function
End Structure
