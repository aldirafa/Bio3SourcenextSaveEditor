Imports System.ComponentModel

''' <summary>
''' Represents different play areas in the game.
''' </summary>
Public Enum PlayArea As Byte
    ''' <summary>
    ''' Uptown (including the RPD-building)
    ''' </summary>
    <Description("Uptown (including the RPD-building)")>
    Uptown = &H0
    ''' <summary>
    ''' Downtown (including the Clock Tower and Hospital)
    ''' </summary>
    <Description("Downtown (including the Clock Tower and Hospital)")>
    Downtown = &H1
    ''' <summary>
    ''' Clock tower/park before the hospital explosion
    ''' </summary>
    <Description("Clock tower/park before the hospital explosion")>
    ClockTower_BeforeHospitalExplosion = &H2
    ''' <summary>
    ''' Clock tower/park after the hospital explosion
    ''' </summary>
    <Description("Clock tower/park after the hospital explosion")>
    ClockTower_AfterHospitalExplosion = &H3
    ''' <summary>
    ''' Dead factory
    ''' </summary>
    <Description("Dead factory")>
    DeadFactory = &H4
    ''' <summary>
    ''' Shows first the Mercenaries-minigame ending, then the
    ''' ending video of the main game (the one without Barry
    ''' Burton, assuming 0x2250 has been set to 00. If not,
    ''' then it only crashes).
    ''' </summary>
    <Description("Shows first the Mercenaries-minigame ending, then the ending video of the main game (the one without Barry Burton, assuming 0x2250 has been set to 00. If not, then it only crashes).")>
    Weird1 = &H5
    ''' <summary>
    ''' Downtown again. I have no idea what's different here.
    ''' This may be after the scene where Jill falls trough
    ''' the parking lot floor.
    ''' </summary>
    <Description("Downtown again. I have no idea what's different here. This may be after the scene where Jill falls trough the parking lot floor.")>
    Weird2 = &H6
End Enum
