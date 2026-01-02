Imports System.Collections.Generic
Imports System.IO

''' <summary>
''' Provides a mapping from ItemID to image index. Kept in the Library project so other
''' non-UI code can use the mapping without depending on WPF types.
''' </summary>
Public Module ItemImage

    ' Map of ItemID to image index
    Private ReadOnly itemIdToImageIndexMap As New Dictionary(Of ItemID, Integer) From {
        {ItemID.CombatKnife, 1}, ' Row 1
        {ItemID.MercsHandgun, 2},
        {ItemID.MercsHandgunEnhanced, 2},
        {ItemID.Handgun, 3},
        {ItemID.HandgunEnhanced, 3},
        {ItemID.Shotgun, 4},
        {ItemID.ShotgunEnhanced, 4},
        {ItemID.Magnum, 5},
        {ItemID.GLauncherAcid, 6},
        {ItemID.GLauncherBurst, 6},
        {ItemID.GLauncherFlame, 6},
        {ItemID.GLauncherFreeze, 6},
        {ItemID.RocketLauncher, 7}, ' Row 2
        {ItemID.GatlingGun, 8},
        {ItemID.MineThrower, 9},
        {ItemID.EagleHandgun, 10},
        {ItemID.AssaultRifleAuto, 11},
        {ItemID.AssaultRifleManual, 11},
        {ItemID.WesternCustomShotgun, 12},
        {ItemID.MineThrowerEnhanced, 13},
        {ItemID.HandgunBullets, 14}, ' Row 3
        {ItemID.MagnumRounds, 15},
        {ItemID.ShotgunShells, 16},
        {ItemID.GrenadeRounds, 17},
        {ItemID.FlameRounds, 18},
        {ItemID.AcidRounds, 19},
        {ItemID.FreezeRounds, 20},
        {ItemID.MineThrowerRounds, 21}, ' Row 4
        {ItemID.AssaultRifleClip, 22},
        {ItemID.EnhancedHandgunBullets, 23},
        {ItemID.EnhancedShotgunShells, 24},
        {ItemID.FirstAidSpray, 25},
        {ItemID.GreenHerb, 26},
        {ItemID.BlueHerb, 27},
        {ItemID.RedHerb, 28}, ' Row 5
        {ItemID.MixedHerb_Green2, 29},
        {ItemID.MixedHerb_GreenBlue, 30},
        {ItemID.MixedHerb_GreenRed, 31},
        {ItemID.MixedHerb_Green3, 32},
        {ItemID.MixedHerb_Green2Blue, 33},
        {ItemID.MixedHerb_GreenRedBlue, 34},
        {ItemID.FirstAidBox, 35}, ' Row 6
        {ItemID.SquareCrank, 36},
        {ItemID.Unknown_RedMedal, 37},
        {ItemID.Unknown_BlueMedal, 38},
        {ItemID.Unknown_GoldenMedal, 39},
        {ItemID.STARSCard_Jill, 40},
        {ItemID.Unknown_GigaOil, 41},
        {ItemID.Battery, 42}, ' Row 7
        {ItemID.FireHook, 43},
        {ItemID.PowerCable, 44},
        {ItemID.Fuse, 45},
        {ItemID.Unknown_BrokenFireHose, 46},
        {ItemID.OilAdditive, 47},
        {ItemID.CardCase_Brad, 48},
        {ItemID.STARSCard_Brad, 49}, ' Row 8
        {ItemID.MachineOil, 50},
        {ItemID.MixedOil, 51},
        {ItemID.Unknown_SteelChain, 52},
        {ItemID.Wrench, 53},
        {ItemID.IronPipe, 54},
        {ItemID.Unknown_Cylinder, 55},
        {ItemID.FireHose, 56}, ' Row 9
        {ItemID.TapeRecorder, 57},
        {ItemID.LighterOil, 58},
        {ItemID.Lighter_NoOil, 59},
        {ItemID.Lighter_WithOil, 60},
        {ItemID.GreenGem, 61},
        {ItemID.BlueGem, 62},
        {ItemID.AmberBall, 63}, ' Row 10
        {ItemID.ObsidianBall, 64},
        {ItemID.CrystalBall, 65},
        {ItemID.Unknown_RemoteControlWithoutBatteries, 66},
        {ItemID.Unknown_RemoteControlWithBatteries, 67},
        {ItemID.Unknown_AA_Batteries, 68},
        {ItemID.GoldGear, 69},
        {ItemID.SilverGear, 70}, ' Row 11
        {ItemID.ChronosGear, 71},
        {ItemID.BronzeBook, 72},
        {ItemID.BronzeCompass, 73},
        {ItemID.VaccineBase, 74},
        {ItemID.VaccineMedium, 75},
        {ItemID.Vaccine, 76},
        {ItemID.MediumBase, 77}, ' Row 12
        {ItemID.EAGLEPartsA, 78},
        {ItemID.EAGLEPartsB, 79},
        {ItemID.M37PartsA, 80},
        {ItemID.M37PartsB, 81},
        {ItemID.ChronosChain, 82},
        {ItemID.RustedCrank, 83},
        {ItemID.CardKey, 84}, ' Row 13
        {ItemID.GunPowder_A, 85},
        {ItemID.GunPowder_B, 86},
        {ItemID.GunPowder_C, 87},
        {ItemID.GunPowder_AA, 88},
        {ItemID.GunPowder_BB, 89},
        {ItemID.GunPowder_AC, 90},
        {ItemID.GunPowder_BC, 91}, ' Row 14
        {ItemID.GunPowder_CC, 92},
        {ItemID.GunPowder_AAA, 93},
        {ItemID.GunPowder_AAB, 94},
        {ItemID.GunPowder_BBA, 95},
        {ItemID.GunPowder_BBB, 96},
        {ItemID.GunPowder_CCC, 97},
        {ItemID.InfiniteBullets, 98}, ' Row 15
        {ItemID.WaterSample, 99},
        {ItemID.SystemDisk, 100},
        {ItemID.Unknown_DummyKey, 101},
        {ItemID.Lockpick, 102},
        {ItemID.WarehouseKey, 103},
        {ItemID.SickroomKey, 104},
        {ItemID.STARSKey, 105}, ' Row 16
        {ItemID.Unknown_Keyring, 106},
        {ItemID.ClockTowerBezelKey, 107},
        {ItemID.ClockTowerWinderKey, 108},
        {ItemID.ChronosKey, 109},
        {ItemID.ParkMainGateKey, 110},
        {ItemID.ParkGraveyardKey, 111},
        {ItemID.ParkRearGateKey, 112}, ' Row 17
        {ItemID.FacilityKey_NoBarcode, 113},
        {ItemID.FacilityKey_WithBarcode, 114},
        {ItemID.BoutiqueKey, 115},
        {ItemID.InkRibbon, 116},
        {ItemID.ReloadingTool, 117},
        {ItemID.GameInstructionsA, 118},
        {ItemID.GameInstructionsB, 119}, ' Row 18
        {ItemID.Unknown_BOTU7, 120}
    }

    ''' <summary>
    ''' Get the image index for a specific <see cref="ItemID"/> item.
    ''' </summary>
    ''' <param name="itemID">The <see cref="ItemID"/> to get the image index for.</param>
    ''' <returns>The image index associated with the specified <see cref="ItemID"/>.</returns>
    Public Function ItemIDToImageIndex(itemID As ItemID) As Integer
        Dim index As Integer
        If itemIdToImageIndexMap.TryGetValue(itemID, index) Then
            Return index
        Else
            Return 0 ' BOTU
        End If
    End Function

    ''' <summary>
    ''' Opens a read-only stream to the embedded item atlas (26073.png) stored as an embedded resource in the Library assembly.
    ''' UI code can call this method to obtain a stream without bringing WPF/UI types into the Library project.
    ''' Returns Nothing if the resource can't be found.
    ''' </summary>
    Public Function OpenItemAtlasStream() As Stream
        Dim asm = GetType(Item).Assembly
        Dim names = asm.GetManifestResourceNames()
        For Each n In names
            If n.EndsWith("26073.png", StringComparison.OrdinalIgnoreCase) Then
                Dim s = asm.GetManifestResourceStream(n)
                If s IsNot Nothing Then
                    Return s
                End If
            End If
        Next
        Return Nothing
    End Function

End Module
