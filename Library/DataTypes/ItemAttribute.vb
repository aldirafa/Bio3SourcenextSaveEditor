Imports System.ComponentModel

''' <summary>
''' Defines the attribute of an item, specifically how its ammo is displayed.
''' </summary>
Public Enum ItemAttribute As Byte
    ''' <summary>
    ''' Use this for (puzzle) items that don't have the ammo display.
    ''' </summary>
    <Description("No Ammo Display")>
    NoAmmoDisplay = &H0
    ''' <summary>
    ''' Ammo remaining display (Green)
    ''' </summary>
    <Description("Ammo remaining display (Green)")>
    AmmoDisplayGreen = &H1
    ''' <summary>
    ''' % (percentage) of ammo remaining display (Green)
    ''' </summary>
    <Description("% (percentage) of ammo remaining display (Green)")>
    AmmoDisplayPercentageGreen = &H2
    ''' <summary>
    ''' Infinite ammo (Green)
    ''' </summary>
    <Description("Infinite ammo (Green)")>
    AmmoDisplayInfiniteGreen = &H3

    ''' <summary>
    ''' Ammo remaining display (Red)
    ''' </summary>
    <Description("Ammo remaining display (Red)")>
    AmmoDisplayRed = &H5
    ''' <summary>
    ''' % (percentage) of ammo remaining display (Red)
    ''' </summary>
    <Description("% (percentage) of ammo remaining display (Red)")>
    AmmoDisplayPercentageRed = &H6
    ''' <summary>
    ''' Infinite ammo (Red)
    ''' </summary>
    <Description("Infinite ammo (Red)")>
    AmmoDisplayInfiniteRed = &H7

    ''' <summary>
    ''' Ammo remaining display (Blue)
    ''' </summary>
    <Description("Ammo remaining display (Blue)")>
    AmmoDisplayBlue = &HD
    ''' <summary>
    ''' % (percentage) of ammo remaining display (Blue)
    ''' </summary>
    <Description("% (percentage) of ammo remaining display (Blue)")>
    AmmoDisplayPercentageBlue = &HE
    ''' <summary>
    ''' Infinite ammo (Blue)
    ''' </summary>
    <Description("Infinite ammo (Blue)")>
    AmmoDisplayInfiniteBlue = &HF
End Enum
