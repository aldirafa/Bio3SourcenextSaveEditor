Imports System.IO

''' <summary>
''' Represents a RE3 Save File.
''' </summary>
Public Class SaveGame
    Private ReadOnly _file As IO.FileInfo
    Private bytes() As Byte

    ' Add near the top of the class (after bytes declaration)
    Private Const InventorySlots As Integer = 10
    Private Const ItemBoxSlots As Integer = 63
    Private Const ItemSize As Integer = 4

    ''' <summary>
    ''' Gets the file info of the save game.
    ''' </summary>
    ''' <returns>A <see cref="FileInfo"/> object representing the save file.</returns>
    Public ReadOnly Property File As FileInfo
        Get
            Return _file
        End Get
    End Property

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SaveGame"/> class from a file.
    ''' </summary>
    ''' <param name="file">The save file.</param>
    Public Sub New(file As IO.FileInfo)
        _file = file
        LoadSaveData()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SaveGame"/> class from a file path.
    ''' </summary>
    ''' <param name="filePath">The path to the save file.</param>
    Public Sub New(filePath As String)
        _file = New IO.FileInfo(filePath)
        LoadSaveData()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SaveGame"/> class from a byte array and file path.
    ''' </summary>
    ''' <param name="data">The byte array containing the save data.</param>
    ''' <param name="filePath">The path to the save file.</param>
    Public Sub New(data As Byte(), filePath As String)
        _file = New IO.FileInfo(filePath)
        LoadSaveData(data:=data)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SaveGame"/> class from a byte array and file info.
    ''' </summary>
    ''' <param name="data">The byte array containing the save data.</param>
    ''' <param name="fileInfo">The file info for the save file.</param>
    Public Sub New(data As Byte(), fileInfo As FileInfo)
        _file = fileInfo
        LoadSaveData(data:=data)
    End Sub

    ''' <summary>
    ''' Loads the save data from the file.
    ''' </summary>
    Private Sub LoadSaveData()
        ' Load the save data from the file into the bytes array
        bytes = IO.File.ReadAllBytes(_file.FullName)
    End Sub

    ''' <summary>
    ''' Loads the save data from a byte array.
    ''' </summary>
    ''' <param name="data">The byte array containing the save data.</param>
    Private Sub LoadSaveData(data As Byte())
        bytes = data
    End Sub

    ''' <summary>
    ''' Saves the modified save data back to the file.
    ''' </summary>
    Public Sub Save()
        ' Save the bytes array back to the file
        IO.File.WriteAllBytes(_file.FullName, bytes)
    End Sub

    ''' <summary>
    ''' Gets or sets the difficulty mode of the save game.
    ''' </summary>
    ''' <returns>True if the difficulty is set to easy mode; false if hard mode.</returns>
    Public Property Difficulty As Boolean
        Get
            ' offset 0x0208, 1 byte
            ' 1 = easy mode
            ' 0 = hard mode
            Return bytes(&H208) = 1
        End Get
        Set(value As Boolean)
            bytes(&H208) = If(value, CByte(1), CByte(0))
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the X and Y coordinates of the player in the save game.
    ''' </summary>
    ''' <returns>A <see cref="KeyValuePair(Of Short, Short)"/> representing the X and Y coordinates.</returns>
    Public Property XYCoordinates As KeyValuePair(Of Short, Short)
        Get
            ' offset 0x020E = X coordinates, 0000-FFFF
            ' offset 0x0212 = Y coordinates, 0000-FFFF
            ' both 16 bit values
            Dim x As Short = BitConverter.ToInt16(bytes, &H20E)
            Dim y As Short = BitConverter.ToInt16(bytes, &H212)
            Return New KeyValuePair(Of Short, Short)(x, y)
        End Get
        Set(value As KeyValuePair(Of Short, Short))
            ' offset 0x020E = X coordinates, 0000-FFFF
            BitConverter.GetBytes(value.Key).CopyTo(bytes, &H20E)
            ' offset 0x0212 = Y coordinates, 0000-FFFF
            BitConverter.GetBytes(value.Value).CopyTo(bytes, &H212)
        End Set
    End Property

    ''' <summary>
    ''' Gets the X coordinate of the player in the save game.
    ''' </summary>
    ''' <returns>The X coordinate as a <see cref="Short"/>.</returns>
    Public ReadOnly Property XCoordinate As Short
        Get
            Return XYCoordinates.Key
        End Get
    End Property

    ''' <summary>
    ''' Gets the Y coordinate of the player in the save game.
    ''' </summary>
    ''' <returns>The Y coordinate as a <see cref="Short"/>.</returns>
    Public ReadOnly Property YCoordinate As Short
        Get
            Return XYCoordinates.Value
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the number of epilogues unlocked in the save game.
    ''' </summary>
    ''' <returns>The number of epilogues unlocked as a <see cref="Byte"/>.</returns>
    ''' <remarks>Set to 08 to unlock all epilogues, but the game still needs to be finished at least once.</remarks>
    Public Property EpiloguesUnlocked As Byte
        Get
            ' 0x0216, 1 byte
            ' 08 = unlocked all epilogues
            ' basically, how many epilogues have been unlocked
            Return bytes(&H216)
        End Get
        Set(value As Byte)
            If value > &H8 Then
                Throw New ArgumentOutOfRangeException(NameOf(value), "EpiloguesUnlocked cannot be greater than 8.")
            End If
            bytes(&H216) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the number of saves made in the save game.
    ''' </summary>
    ''' <returns>The number of saves made as a <see cref="Byte"/>.</returns>
    Public Property NumOfSaves As Byte
        Get
            ' 0x0218 1 byte
            ' number of saves made, 00-FF
            Return bytes(&H218)
        End Get
        Set(value As Byte)
            bytes(&H218) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the save game.
    ''' </summary>
    ''' <returns>The name of the save game as a <see cref="Byte"/>.</returns>
    ''' <remarks>Still a TODO: for research.</remarks>
    Public Property SaveGameName As Byte
        Get
            '0x021B = name of savegame (warehouse, alley, etc.)
            'todo: which value shows which name
            ' 8-bit 00-ff value
            Return bytes(&H21B)
        End Get
        Set(value As Byte)
            bytes(&H21B) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the current play area in the save game.
    ''' </summary>
    ''' <returns>The current play area as a <see cref="PlayArea"/>.</returns>
    Public Property Area As PlayArea
        Get
            ' 0x024E = current play area
            ' 0x00 - 0x06 (see PlayArea enum)
            Return CType(bytes(&H24E), PlayArea)
        End Get
        Set(value As PlayArea)
            bytes(&H24E) = CByte(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the current room/event in the save game.
    ''' </summary>
    ''' <returns>The current room/event as a <see cref="Byte"/>.</returns>
    ''' <remarks>Still a TODO: for research.</remarks>
    Public Property Room As Byte
        Get
            ' 0x0250 &H0-&H20 mostly work
            ' todo: still need more research
            Return bytes(&H250)
        End Get
        Set(value As Byte)
            bytes(&H250) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the player character in the save game.
    ''' </summary>
    ''' <returns>The player character as a <see cref="PlayerCharacter"/>.</returns>
    ''' <remarks>
    ''' <see cref="Outfit"/> is more reliable in setting Jill's outfit since it seems
    ''' to override the values <see cref="PlayerCharacter.Jill_NormalClothes"/> until <see cref="PlayerCharacter.Jill_AnotherNormal"/> set here.
    ''' </remarks>
    Public Property Character As PlayerCharacter
        Get
            ' 0x025E 00-0F based on enum
            ' basically the character being played
            Return CType(bytes(&H25E), PlayerCharacter)
        End Get
        Set(value As PlayerCharacter)
            bytes(&H25E) = CByte(value)
        End Set
    End Property

    ''' <summary>
    ''' Unlocks all maps in the save game.
    ''' </summary>
    Public Sub UnlockAllMaps()
        ' 0x03FF - 0x0403 -> if set to FF, all maps are unlocked
        For i As Integer = &H3FF To &H403
            bytes(i) = &HFF
        Next
    End Sub

    ''' <summary>
    ''' Unlocks all files in the game, optionally setting if Jill's Diary is
    ''' available as well.
    ''' </summary>
    ''' <param name="JillsDiary">Set to True if you wish to unlock Jill's diary.</param>
    Public Sub UnlockAllFiles(Optional ByVal JillsDiary As Boolean = False)
        ' 0x0404 until 0x409 -> if set to FF, all files are unlocked
        ' if JillsDiary = true, 0x0408 = 1E and 0x0409 = 00
        For i As Integer = &H404 To &H409
            If JillsDiary AndAlso i = &H408 Then
                bytes(i) = &H1E
            ElseIf JillsDiary AndAlso i = &H409 Then
                bytes(i) = &H0
            Else
                bytes(i) = &HFF
            End If
        Next
    End Sub

    ''' <summary>
    ''' Gets or sets the inventory of Jill in the save game.
    ''' </summary>
    ''' <returns>The inventory of Jill as an <see cref="Inventory"/>.</returns>
    Public Property JillsInventory As Inventory
        Get
            Dim inventoryBytes(InventorySlots * ItemSize - 1) As Byte
            Array.Copy(bytes, &H40C, inventoryBytes, 0, inventoryBytes.Length)
            Return New Inventory(inventoryBytes)
        End Get
        Set(value As Inventory)
            Dim invBytes = value.ToBytes()
            If invBytes.Length <> InventorySlots * ItemSize Then
                Throw New ArgumentException($"Jill inventory must be {InventorySlots * ItemSize} bytes.", NameOf(value))
            End If
            Array.Copy(invBytes, 0, bytes, &H40C, invBytes.Length)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the item box of Jill in the save game.
    ''' </summary>
    ''' <returns>The item box of Jill as an <see cref="Inventory"/>.</returns>
    Public Property JillsItemBox As Inventory
        Get
            Dim itemBoxBytes(ItemBoxSlots * ItemSize - 1) As Byte
            Array.Copy(bytes, &H434, itemBoxBytes, 0, itemBoxBytes.Length)
            Return New Inventory(itemBoxBytes)
        End Get
        Set(value As Inventory)
            Dim boxBytes = value.ToBytes()
            If boxBytes.Length <> ItemBoxSlots * ItemSize Then
                Throw New ArgumentException($"Jill item box must be {ItemBoxSlots * ItemSize} bytes.", NameOf(value))
            End If
            Array.Copy(boxBytes, 0, bytes, &H434, boxBytes.Length)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the current weapon of Jill in the save game.
    ''' </summary>
    ''' <returns>The current weapon of Jill as an <see cref="ItemID"/>.</returns>
    Public Property JillsWeapon As ItemID
        Get
            ' 0x0535 = Jill's current weapon
            Return CType(bytes(&H535), ItemID)
        End Get
        Set(value As ItemID)
            bytes(&H535) = CByte(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether Jill's sidepack is enabled in the save game.
    ''' </summary>
    ''' <returns>True if Jill's sidepack is enabled; otherwise, false.</returns>
    Public Property JillSidepackEnabled As Boolean
        Get
            ' 0x0536 = Sidepack status
            ' enabled: 0A
            ' disabled: 08
            Return bytes(&H536) = &HA
        End Get
        Set(value As Boolean)
            bytes(&H536) = If(value, &HA, &H8)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the inventory of Carlos in the save game.
    ''' </summary>
    ''' <returns>The inventory of Carlos as an <see cref="Inventory"/>.</returns>
    Public Property CarlosInventory As Inventory
        Get
            ' inventory 1: 0x054C, 2: 0x0550, ..., 10: 0x0570, each item is 4 bytes
            Dim inventoryBytes(InventorySlots * ItemSize - 1) As Byte
            Array.Copy(bytes, &H54C, inventoryBytes, 0, inventoryBytes.Length)
            Return New Inventory(inventoryBytes)
        End Get
        Set(value As Inventory)
            Dim invBytes = value.ToBytes()
            If invBytes.Length <> InventorySlots * ItemSize Then
                Throw New ArgumentException($"Carlos inventory must be {InventorySlots * ItemSize} bytes.", NameOf(value))
            End If
            Array.Copy(value.ToBytes(), 0, bytes, &H54C, invBytes.Length)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the item box of Carlos in the save game.
    ''' </summary>
    ''' <returns>The item box of Carlos as an <see cref="Inventory"/>.</returns>
    Public Property CarlosItemBox As Inventory
        Get
            ' item box slot 1: 0x0574, 2: 0x0578, ..., bottom: 0x0670, each item is 4 bytes
            Dim itemBoxBytes(ItemBoxSlots * ItemSize - 1) As Byte
            Array.Copy(bytes, &H574, itemBoxBytes, 0, itemBoxBytes.Length)
            Return New Inventory(itemBoxBytes)
        End Get
        Set(value As Inventory)
            Dim boxBytes = value.ToBytes()
            If boxBytes.Length <> ItemBoxSlots * ItemSize Then
                Throw New ArgumentException($"Carlos item box must be {ItemBoxSlots * ItemSize} bytes.", NameOf(value))
            End If
            Array.Copy(boxBytes, 0, bytes, &H574, boxBytes.Length)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the current weapon of Carlos in the save game.
    ''' </summary>
    ''' <returns>The current weapon of Carlos as an <see cref="ItemID"/>.</returns>
    Public Property CarlosWeapon As ItemID
        Get
            ' 0x0675 = Carlos's current weapon
            Return CType(bytes(&H675), ItemID)
        End Get
        Set(value As ItemID)
            bytes(&H675) = CByte(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether Carlos's sidepack is enabled in the save game.
    ''' </summary>
    ''' <returns>True if Carlos's sidepack is enabled; otherwise, false.</returns>
    Public Property CarlosSidepack As Boolean
        Get
            ' 0x0676 = Sidepack status
            ' enabled: 0A
            ' disabled: 08
            Return bytes(&H676) = &HA
        End Get
        Set(value As Boolean)
            bytes(&H676) = If(value, &HA, &H8)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Jill's outfit in the save game.
    ''' </summary>
    ''' <returns>The outfit of Jill as a <see cref="JillOutfit"/>.</returns>
    ''' <remarks>
    ''' You should select Jill's outfit here instead of <see cref="Character"/>.
    '''  The value here might override Jill's appearance set on <see cref="Character"/>.
    ''' </remarks>
    Public Property Outfit As JillOutfit
        Get
            ' 0x08D4, 1 byte, 00-07 based on enum
            Return CType(bytes(&H8D4), JillOutfit)
        End Get
        Set(value As JillOutfit)
            bytes(&H8D4) = CByte(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether the player is poisoned in the save game.
    ''' </summary>
    ''' <returns>True if the player is poisoned; otherwise, false.</returns>
    Public Property Poisoned As Boolean
        Get
            ' 0x021A, 1 byte
            ' 8F = not poisoned
            ' 00 = poisoned
            Return bytes(&H21A) = &H8F
        End Get
        Set(value As Boolean)
            bytes(&H21A) = If(value, &H8F, &H0)
        End Set
    End Property
End Class
