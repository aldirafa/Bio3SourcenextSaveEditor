Imports System.ComponentModel
Imports System.Text

Public Module Utils
    ''' <summary>
    ''' Gets the description of an enum member from its Description attribute.
    ''' </summary>
    ''' <typeparam name="T">The enum type.</typeparam>
    ''' <param name="enumValue">The enum value.</param>
    ''' <returns>A description of the enum member.</returns>
    <Description("Gets the description of an enum member from its Description attribute.")>
    Public Function GetEnumMemberDescription(Of T)(enumValue As T) As String
        Dim enumType As Type = GetType(T)
        Dim memberInfo = enumType.GetMember(enumValue.ToString())
        If memberInfo.Length > 0 Then
            Dim attrs = memberInfo(0).GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False)
            If attrs.Length > 0 Then
                Return CType(attrs(0), System.ComponentModel.DescriptionAttribute).Description
            End If
        End If

        ' Fallback to the enum value's name if no description is found.
        Return SplitCamelCase(enumValue.ToString())
    End Function

    ''' <summary>
    ''' Splits a camel case string into separate words.
    ''' </summary>
    ''' <param name="input">The camel case string to split.</param>
    ''' <returns>A string with the separate words.</returns>
    <Description("Splits a camel case string into separate words.")>
    Public Function SplitCamelCase(input As String) As String
        Return System.Text.RegularExpressions.Regex.Replace(input, "([a-z])([A-Z])", "$1 $2")
    End Function

    ''' <summary>
    ''' Gets a dictionary of all <see cref="ItemID"/> enum members and their descriptions.
    ''' </summary>
    ''' <returns>A dictionary of <see cref="ItemID"/> enum members and their descriptions.</returns>
    Public Function GetAllItems() As Dictionary(Of ItemID, String)
        Dim items As New Dictionary(Of ItemID, String)()
        For Each value As ItemID In [Enum].GetValues(GetType(ItemID))
            items(value) = GetEnumMemberDescription(Of ItemID)(value)
        Next
        Return items
    End Function

    ''' <summary>
    ''' Gets a dictionary of all <see cref="ItemAttribute"/> enum members and their descriptions.
    ''' </summary>
    ''' <returns>A dictionary of <see cref="ItemAttribute"/> enum members and their descriptions.</returns>
    Public Function GetAllAttributes() As Dictionary(Of ItemAttribute, String)
        Dim attributes As New Dictionary(Of ItemAttribute, String)()
        For Each value As ItemAttribute In [Enum].GetValues(GetType(ItemAttribute))
            attributes(value) = GetEnumMemberDescription(Of ItemAttribute)(value)
        Next
        Return attributes
    End Function

    ''' <summary>
    ''' Gets all <see cref="JillOutfit"/> enum members and their descriptions.
    ''' </summary>
    ''' <returns>A dictionary of <see cref="JillOutfit"/> enum members and their descriptions.</returns>
    Public Function GetAllJillOutfit() As Dictionary(Of JillOutfit, String)
        Dim outfits As New Dictionary(Of JillOutfit, String)()
        For Each value As JillOutfit In [Enum].GetValues(GetType(JillOutfit))
            outfits(value) = GetEnumMemberDescription(Of JillOutfit)(value)
        Next
        Return outfits
    End Function

    ''' <summary>
    ''' Gets all <see cref="PlayArea"/> enum members and their descriptions.
    ''' </summary>
    ''' <returns>A dictionary of <see cref="PlayArea"/> enum members and their descriptions.</returns>
    Public Function GetAllPlayArea() As Dictionary(Of PlayArea, String)
        Dim areas As New Dictionary(Of PlayArea, String)()
        For Each value As PlayArea In [Enum].GetValues(GetType(PlayArea))
            areas(value) = GetEnumMemberDescription(Of PlayArea)(value)
        Next
        Return areas
    End Function

    ''' <summary>
    ''' Gets all <see cref="PlayerCharacter" /> enum members and their descriptions.
    ''' </summary>
    ''' <returns>A dictionary of <see cref="PlayerCharacter" /> enum members and their descriptions.</returns>
    Public Function GetAllPlayerCharacter() As Dictionary(Of PlayerCharacter, String)
        Dim characters As New Dictionary(Of PlayerCharacter, String)()
        For Each value As PlayerCharacter In [Enum].GetValues(GetType(PlayerCharacter))
            characters(value) = GetEnumMemberDescription(Of PlayerCharacter)(value)
        Next
        Return characters
    End Function

    ''' <summary>
    ''' Gets a string containing the credits information of all components used by the library
    ''' and sources of informations for the library's algorithm.
    ''' </summary>
    ''' <returns>A <see cref="String"/> containing the credits information.</returns>
    Public Function GetCreditsString() As String
        ' Returns a string containing the credits information.
        Dim sb As New StringBuilder()
        ' Inventory Items from Resident Evil 3: Nemesis (Playstation)
        'Ripped by Ultimecia: https : //www.spriters-resource.com/playstation/resi3/asset/26073/
        sb.AppendLine("Inventory Items from Resident Evil 3: Nemesis (Playstation)")
        sb.AppendLine("Ripped by Ultimecia: https://www.spriters-resource.com/playstation/resi3/asset/26073/")
        'Resident Evil 3: Nemesis – Save Game Hacking Guide
        'by Shockproof_Jamo, Version 1.1, Updated 8 October 2009: https : //gamefaqs.gamespot.com/pc/431704-resident-evil-3-nemesis/faqs/36465
        sb.AppendLine("Resident Evil 3: Nemesis – Save Game Hacking Guide")
        sb.AppendLine("by Shockproof_Jamo, Version 1.1, Updated 8 October 2009: https://gamefaqs.gamespot.com/pc/431704-resident-evil-3-nemesis/faqs/36465")
        Return sb.ToString()
    End Function
End Module
