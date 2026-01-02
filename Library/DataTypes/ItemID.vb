Imports System.ComponentModel

''' <summary>
''' Enumeration of all item IDs in Resident Evil 3
''' </summary>
Public Enum ItemID As Byte
    ''' <summary>
    ''' Empty Slot
    ''' </summary>
    <Description("Empty Slot")>
    Empty = &H0 ' Empty slot
    ''' <summary>
    ''' Combat Knife
    ''' </summary>
    <Description("Combat Knife")>
    CombatKnife = &H1 ' Combat Knife
    ''' <summary>
    ''' Mercs Handgun (Sigpro SP 2009)
    ''' </summary>
    <Description("Mercs Handgun (Sigpro SP 2009)")>
    MercsHandgun = &H2 ' Sigpro SP 2009 handgun
    ''' <summary>
    ''' Handgun (Beretta M92F)
    ''' </summary>
    <Description("Handgun (Beretta M92F)")>
    Handgun = &H3 ' Beretta M92F handgun, custom S.T.A.R.S edition
    ''' <summary>
    ''' Shotgun (Benelli M3S)
    ''' </summary>
    <Description("Shotgun (Benelli M3S)")>
    Shotgun = &H4 ' Shotgun Benelli M3S
    ''' <summary>
    ''' Magnum (Smith & Wesson M629C)
    ''' </summary>
    <Description("Magnum (Smith & Wesson M629C)")>
    Magnum = &H5 ' Smith & Wesson M629C .44-caliber magnum revolver
    ''' <summary>
    ''' Grenade Launcher (Hk-p)
    ''' </summary>
    <Description("Grenade Launcher (Hk-p)")>
    GLauncherBurst = &H6 ' Hk-p Grenade launcher with burst rounds
    ''' <summary>
    ''' Grenade Launcher (Hk-p) with Flame Rounds
    ''' </summary>
    <Description("Grenade Launcher (Hk-p) with Flame Rounds")>
    GLauncherFlame = &H7 ' Hk-p Grenade launcher with flame rounds
    ''' <summary>
    ''' Grenade Launcher (Hk-p) with Acid Rounds
    ''' </summary>
    <Description("Grenade Launcher (Hk-p) with Acid Rounds")>
    GLauncherAcid = &H8 ' Hk-p Grenade launcher with acid rounds
    ''' <summary>
    ''' Grenade Launcher (Hk-p) with Freeze Rounds
    ''' </summary>
    <Description("Grenade Launcher (Hk-p) with Freeze Rounds")>
    GLauncherFreeze = &H9 ' Hk-p Grenade launcher with freeze rounds
    ''' <summary>
    ''' Rocket Launcher (M66)
    ''' </summary>
    <Description("Rocket Launcher (M66)")>
    RocketLauncher = &HA ' M66 Rocket launcher
    ''' <summary>
    ''' Gatling Gun
    ''' </summary>
    '''  <remarks>
    ''' The gatling gun is hardcoded to always have infinite ammo, 
    ''' regardless of what it's attributes in the savegame are.
    ''' </remarks>
    <Description("Gatling Gun")>
    GatlingGun = &HB ' Gatling gun
    ''' <summary>
    ''' Mine Thrower
    ''' </summary>
    <Description("Mine Thrower")>
    MineThrower = &HC ' Mine thrower
    ''' <summary>
    ''' Eagle Handgun (STI Eagle 6.0)
    ''' </summary>
    <Description("Eagle Handgun (STI Eagle 6.0)")>
    EagleHandgun = &HD ' STI Eagle 6.0
    ''' <summary>
    ''' Assault Rifle (M4A1)
    ''' </summary>
    <Description("Assault Rifle (M4A1) Manual")>
    AssaultRifleManual = &HE ' M4A1 Assault rifle set to manual mode
    ''' <summary>
    ''' Assault Rifle (M4A1) Auto
    ''' </summary>
    <Description("Assault Rifle (M4A1) Auto")>
    AssaultRifleAuto = &HF ' M4A1 Assault rifle set to auto mode
    ''' <summary>
    ''' Western Custom Shotgun (M37)
    ''' </summary>
    ''' <remarks>
    ''' Terminator 2 - Judgement day, anyone?
    ''' </remarks>
    <Description("Western Custom Shotgun (M37)")>
    WesternCustomShotgun = &H10 ' Western Custom M37 lever action shotgun
    ''' <summary>
    ''' Mercs Handgun (Sigpro SP 2009) with enhanced ammo loaded
    ''' </summary>
    <Description("Mercs Handgun (Sigpro SP 2009) with enhanced ammo loaded")>
    MercsHandgunEnhanced = &H11 ' Sigpro SP 2009 with enhanced ammo loaded
    ''' <summary>
    ''' Handgun (Beretta M92F) with enhanced ammo loaded
    ''' </summary>
    <Description("Handgun (Beretta M92F) with enhanced ammo loaded")>
    HandgunEnhanced = &H12 ' Beretta M92F custom with enhanced ammo loaded
    ''' <summary>
    ''' Shotgun (Benelli M3S) with enhanced ammo loaded
    ''' </summary>
    <Description("Shotgun (Benelli M3S) with enhanced ammo loaded")>
    ShotgunEnhanced = &H13 ' Shotgun Benelli M3S with enhanced ammo loaded
    ''' <summary>
    ''' Mine Thrower with enhanced ammo loaded
    ''' </summary>
    <Description("Mine Thrower with enhanced ammo loaded")>
    MineThrowerEnhanced = &H14 ' Mine thrower with enhanced ammo
    ''' <summary>
    ''' Handgun Bullets (9x19 parabellum)
    ''' </summary>
    <Description("Handgun Bullets (9x19 parabellum)")>
    HandgunBullets = &H15 ' Handgun bullets (9x19 parabellum)
    ''' <summary>
    ''' Magnum Rounds (.44-caliber)
    ''' </summary>
    <Description("Magnum Rounds (.44-caliber)")>
    MagnumRounds = &H16 ' Magnum rounds (.44-caliber)
    ''' <summary>
    ''' Shotgun Shells
    ''' </summary>
    <Description("Shotgun Shells")>
    ShotgunShells = &H17 ' Shotgun shells
    ''' <summary>
    ''' Burst Rounds
    ''' </summary>
    <Description("Burst Rounds")>
    GrenadeRounds = &H18 ' Grenade
    ''' <summary>
    ''' Flame Rounds
    ''' </summary>
    <Description("Flame Rounds")>
    FlameRounds = &H19 ' Flame rounds
    ''' <summary>
    ''' Acid Rounds
    ''' </summary>
    <Description("Acid Rounds")>
    AcidRounds = &H1A ' Acid rounds
    ''' <summary>
    ''' Freeze Rounds
    ''' </summary>
    <Description("Freeze Rounds")>
    FreezeRounds = &H1B ' Freeze rounds
    ''' <summary>
    ''' Mine Thrower Ammo
    ''' </summary>
    <Description("Mine Thrower Ammo")>
    MineThrowerRounds = &H1C ' Minethrower rounds
    ''' <summary>
    ''' Assault Rifle Clip (5.56 NATO rounds)
    ''' </summary>
    <Description("Assault Rifle Clip (5.56 NATO rounds)")>
    AssaultRifleClip = &H1D ' Assault rifle clip (5.56 NATO rounds)
    ''' <summary>
    ''' Enhanced Handgun Bullets
    ''' </summary>
    <Description("Enhanced Handgun Bullets")>
    EnhancedHandgunBullets = &H1E ' Enhanced handgun bullets
    ''' <summary>
    ''' Enhanced Shotgun Shells
    ''' </summary>
    <Description("Enhanced Shotgun Shells")>
    EnhancedShotgunShells = &H1F ' Enhanced shotgun shells

    ''' <summary>
    ''' First Aid Spray
    ''' </summary>
    <Description("First Aid Spray")>
    FirstAidSpray = &H20 ' First aid spray
    ''' <summary>
    ''' Green Herb
    ''' </summary>
    <Description("Green Herb")>
    GreenHerb = &H21 ' Green herb
    ''' <summary>
    ''' Blue Herb
    ''' </summary>
    <Description("Blue Herb")>
    BlueHerb = &H22 ' Blue herb
    ''' <summary>
    ''' Red Herb
    ''' </summary>
    <Description("Red Herb")>
    RedHerb = &H23 ' Red herb
    ''' <summary>
    ''' Mixed Herb (2x Green)
    ''' </summary>
    <Description("Mixed Herb (2x Green)")>
    MixedHerb_Green2 = &H24 ' 2x Green herb mix
    ''' <summary>
    ''' Mixed Herb (Green + Blue)
    ''' </summary>
    <Description("Mixed Herb (Green + Blue)")>
    MixedHerb_GreenBlue = &H25 ' Green + blue herb mix
    ''' <summary>
    ''' Mixed Herb (Green + Red)
    ''' </summary>
    <Description("Mixed Herb (Green + Red)")>
    MixedHerb_GreenRed = &H26 ' Green + red herb mix
    ''' <summary>
    ''' Mixed Herb (3x Green)
    ''' </summary>
    <Description("Mixed Herb (3x Green)")>
    MixedHerb_Green3 = &H27 ' 3x Green herb mix
    ''' <summary>
    ''' Mixed Herb (2x Green + Blue)
    ''' </summary>
    <Description("Mixed Herb (2x Green + Blue)")>
    MixedHerb_Green2Blue = &H28 ' 2x Green herb + blue herb mix
    ''' <summary>
    ''' Mixed Herb (Green + Red + Blue)
    ''' </summary>
    <Description("Mixed Herb (Green + Red + Blue)")>
    MixedHerb_GreenRedBlue = &H29 ' Green + Red + Blue herb mix
    ''' <summary>
    ''' First Aid Box
    ''' </summary>
    ''' <remarks>
    ''' This cannot be hacked to contain infinite sprays, unfortunately.
    ''' </remarks>
    <Description("First Aid Box")>
    FirstAidBox = &H2A ' First aid spray box

    ''' <summary>
    ''' Square Crank
    ''' </summary>
    <Description("Square Crank")>
    SquareCrank = &H2B ' Square crank
    ''' <summary>
    ''' Unknown item (Red Medal)
    ''' </summary>
    <Description("Unknown item (Red Medal)")>
    Unknown_RedMedal = &H2C ' Unknown red medal
    ''' <summary>
    ''' Unknown item (Blue Medal)
    ''' </summary>
    <Description("Unknown item (Blue Medal)")>
    Unknown_BlueMedal = &H2D ' Unknown blue medal
    ''' <summary>
    ''' Unknown item (Golden Medal)
    ''' </summary>
    <Description("Unknown item (Golden Medal)")>
    Unknown_GoldenMedal = &H2E ' Unknown golden medal
    ''' <summary>
    ''' Jill's S.T.A.R.S Card
    ''' </summary>
    <Description("Jill's S.T.A.R.S Card")>
    STARSCard_Jill = &H2F ' Jill's S.T.A.R.S card
    ''' <summary>
    ''' Unknown item (Giga Oil)
    ''' </summary>
    <Description("Unknown item (Giga Oil)")>
    Unknown_GigaOil = &H30 ' Unknown oil can labeled "Giga Oil"
    ''' <summary>
    ''' Battery
    ''' </summary>
    <Description("Battery")>
    Battery = &H31 ' Battery
    ''' <summary>
    ''' Fire Hook
    ''' </summary>
    <Description("Fire Hook")>
    FireHook = &H32 ' Fire hook
    ''' <summary>
    ''' Power Cable
    ''' </summary>
    <Description("Power Cable")>
    PowerCable = &H33 ' Power cable
    ''' <summary>
    ''' Fuse
    ''' </summary>
    <Description("Fuse")>
    Fuse = &H34 ' Fuse
    ''' <summary>
    ''' Unknown item (Broken Fire Hose)
    ''' </summary>
    <Description("Unknown item (Broken Fire Hose)")>
    Unknown_BrokenFireHose = &H35 ' Unknown broken fire hose
    ''' <summary>
    ''' Oil Additive
    ''' </summary>
    <Description("Oil Additive")>
    OilAdditive = &H36 ' Oil Additive
    ''' <summary>
    ''' Brad Vickers' Card Case
    ''' </summary>
    <Description("Brad Vickers' Card Case")>
    CardCase_Brad = &H37 ' Brad Vickers' card case
    ''' <summary>
    ''' Brad Vickers' S.T.A.R.S Card
    ''' </summary>
    <Description("Brad Vickers' S.T.A.R.S Card")>
    STARSCard_Brad = &H38 ' Brad Vickers' S.T.A.R.S card
    ''' <summary>
    ''' Machine Oil
    ''' </summary>
    <Description("Machine Oil")>
    MachineOil = &H39 ' Machine oil
    ''' <summary>
    ''' Mixed Oil
    ''' </summary>
    <Description("Mixed Oil")>
    MixedOil = &H3A ' Mixed oil
    ''' <summary>
    ''' Unknown item (Steel Chain)
    ''' </summary>
    <Description("Unknown item (Steel Chain)")>
    Unknown_SteelChain = &H3B ' Unknown steel chain
    ''' <summary>
    ''' Wrench
    ''' </summary>
    <Description("Wrench")>
    Wrench = &H3C ' Wrench
    ''' <summary>
    ''' Iron Pipe
    ''' </summary>
    <Description("Iron Pipe")>
    IronPipe = &H3D ' Iron pipe
    ''' <summary>
    ''' Unknown item (Cylinder)
    ''' </summary>
    <Description("Unknown item (Cylinder)")>
    Unknown_Cylinder = &H3E ' Unknown cylinder
    ''' <summary>
    ''' Fire Hose
    ''' </summary>
    <Description("Fire Hose")>
    FireHose = &H3F ' Fire hose
    ''' <summary>
    ''' Tape recorder
    ''' </summary>
    <Description("Tape recorder")>
    TapeRecorder = &H40 ' Tape recorder
    ''' <summary>
    ''' Lighter Oil
    ''' </summary>
    <Description("Lighter Oil")>
    LighterOil = &H41 ' Lighter oil
    ''' <summary>
    ''' Lighter (lid closed, no oil)
    ''' </summary>
    <Description("Lighter (lid closed, no oil)")>
    Lighter_NoOil = &H42 ' Lighter (lid closed, no oil)
    ''' <summary>
    ''' Lighter (lid open, has oil)
    ''' </summary>
    <Description("Lighter (lid open, has oil)")>
    Lighter_WithOil = &H43 ' Lighter (lid open, has oil)
    ''' <summary>
    ''' Green Gem
    ''' </summary>
    <Description("Green Gem")>
    GreenGem = &H44 ' Green gem
    ''' <summary>
    ''' Blue Gem
    ''' </summary>
    <Description("Blue Gem")>
    BlueGem = &H45 ' Blue gem
    ''' <summary>
    ''' Amber ball
    ''' </summary>
    <Description("Amber ball")>
    AmberBall = &H46 ' Amber ball
    ''' <summary>
    ''' Obsidian ball
    ''' </summary>
    <Description("Obsidian ball")>
    ObsidianBall = &H47 ' Obsidian ball
    ''' <summary>
    ''' Crystal ball
    ''' </summary>
    <Description("Crystal ball")>
    CrystalBall = &H48 ' Crystal ball
    ''' <summary>
    ''' Unknown item (Remote Control without batteries)
    ''' </summary>
    <Description("Unknown item (Remote Control without batteries)")>
    Unknown_RemoteControlWithoutBatteries = &H49 ' Unknown remote control without batteries
    ''' <summary>
    ''' Unknown item (Remote Control with batteries)
    ''' </summary>
    <Description("Unknown item (Remote Control with batteries)")>
    Unknown_RemoteControlWithBatteries = &H4A ' Unknown remote control with batteries
    ''' <summary>
    ''' Unknown item (AA-batteries)
    ''' </summary>
    <Description("Unknown item (AA-batteries)")>
    Unknown_AA_Batteries = &H4B ' Unknown AA-batteries
    ''' <summary>
    ''' Gold Gear
    ''' </summary>
    <Description("Gold Gear")>
    GoldGear = &H4C ' Gold gear
    ''' <summary>
    ''' Silver Gear
    ''' </summary>
    <Description("Silver Gear")>
    SilverGear = &H4D ' Silver gear
    ''' <summary>
    ''' Chronos Gear
    ''' </summary>
    <Description("Chronos Gear")>
    ChronosGear = &H4E ' Chronos gear
    ''' <summary>
    ''' Bronze Book
    ''' </summary>
    <Description("Bronze Book")>
    BronzeBook = &H4F ' Bronze book
    ''' <summary>
    ''' Bronze Compass
    ''' </summary>
    <Description("Bronze Compass")>
    BronzeCompass = &H50 ' Bronze compass
    ''' <summary>
    ''' Vaccine medium
    ''' </summary>
    <Description("Vaccine medium")>
    VaccineMedium = &H51 ' Vaccine medium
    ''' <summary>
    ''' Vaccine base
    ''' </summary>
    <Description("Vaccine base")>
    VaccineBase = &H52 ' Vaccine base
    ''' <summary>
    ''' Unknown item (Sigpro SP 2009 handgun)
    ''' </summary>
    ''' <remarks>
    ''' Cannot be used as a weapon, puzzle item perhaps?
    ''' </remarks>
    <Description("Unknown item (Sigpro SP 2009 handgun)")>
    Unknown_BOTU1 = &H53 ' Unknown Sigpro SP 2009 handgun
    ''' <summary>
    ''' Unknown item (Sigpro SP 2009 handgun)
    ''' </summary>
    ''' <remarks>
    ''' Cannot be used as a weapon, puzzle item perhaps?
    ''' </remarks>
    <Description("Unknown item (Sigpro SP 2009 handgun)")>
    Unknown_BOTU2 = &H54 ' Unknown Sigpro SP 2009 handgun
    ''' <summary>
    ''' Vaccine
    ''' </summary>
    <Description("Vaccine")>
    Vaccine = &H55 ' Vaccine
    ''' <summary>
    ''' Unknown item (Sigpro SP 2009 handgun)
    ''' </summary>
    ''' <remarks>
    ''' Cannot be used as a weapon, puzzle item perhaps?
    ''' </remarks>
    <Description("Unknown item (Sigpro SP 2009 handgun)")>
    Unknown_BOTU3 = &H56 ' Unknown Sigpro SP 2009 handgun
    ''' <summary>
    ''' Unknown item (Sigpro SP 2009 handgun)
    ''' </summary>
    ''' <remarks>
    ''' Cannot be used as a weapon, puzzle item perhaps?
    ''' </remarks>
    <Description("Unknown item (Sigpro SP 2009 handgun)")>
    Unknown_BOTU4 = &H57 ' Unknown Sigpro SP 2009 handgun
    ''' <summary>
    ''' Medium base
    ''' </summary>
    <Description("Medium base")>
    MediumBase = &H58 ' Medium base
    ''' <summary>
    ''' EAGLE Handgun Parts (A)
    ''' </summary>
    <Description("EAGLE Handgun Parts (A)")>
    EAGLEPartsA = &H59 ' Eagle parts A
    ''' <summary>
    ''' EAGLE Handgun Parts (B)
    ''' </summary>
    <Description("EAGLE Handgun Parts (B)")>
    EAGLEPartsB = &H5A ' Eagle parts B
    ''' <summary>
    ''' M37 Shotgun Parts (A)
    ''' </summary>
    <Description("M37 Shotgun Parts (A)")>
    M37PartsA = &H5B ' M37 parts A
    ''' <summary>
    ''' M37 Shotgun Parts (B)
    ''' </summary>
    <Description("M37 Shotgun Parts (B)")>
    M37PartsB = &H5C ' M37 parts B
    ''' <summary>
    ''' Unknown item (Sigpro SP 2009 handgun)
    ''' </summary>
    ''' <remarks>
    ''' Cannot be used as a weapon, puzzle item perhaps?
    ''' </remarks>
    <Description("Unknown item (Sigpro SP 2009 handgun)")>
    Unknown_BOTU5 = &H5D ' Unknown Sigpro SP 2009 handgun
    ''' <summary>
    ''' Chronos Chain
    ''' </summary>
    <Description("Chronos Chain")>
    ChronosChain = &H5E ' Chronos chain
    ''' <summary>
    ''' Rusted Crank
    ''' </summary>
    <Description("Rusted Crank")>
    RustedCrank = &H5F ' Rusted crank
    ''' <summary>
    ''' Card Key
    ''' </summary>
    <Description("Card Key")>
    CardKey = &H60 ' Card key
    ''' <summary>
    ''' Gun Powder A
    ''' </summary>
    <Description("Gun Powder A")>
    GunPowder_A = &H61 ' Gun powder A
    ''' <summary>
    ''' Gun Powder B
    ''' </summary>
    <Description("Gun Powder B")>
    GunPowder_B = &H62 ' Gun powder B
    ''' <summary>
    ''' Gun Powder C
    ''' </summary>
    <Description("Gun Powder C")>
    GunPowder_C = &H63 ' Gun powder C
    ''' <summary>
    ''' Gun Powder AA
    ''' </summary>
    <Description("Gun Powder AA")>
    GunPowder_AA = &H64 ' Gun powder AA
    ''' <summary>
    ''' Gun Powder BB
    ''' </summary>
    <Description("Gun Powder BB")>
    GunPowder_BB = &H65 ' Gun powder BB
    ''' <summary>
    ''' Gun Powder AC
    ''' </summary>
    <Description("Gun Powder AC")>
    GunPowder_AC = &H66 ' Gun powder AC
    ''' <summary>
    ''' Gun Powder BC
    ''' </summary>
    <Description("Gun Powder BC")>
    GunPowder_BC = &H67 ' Gun powder BC
    ''' <summary>
    ''' Gun Powder CC
    ''' </summary>
    <Description("Gun Powder CC")>
    GunPowder_CC = &H68 ' Gun powder CC
    ''' <summary>
    ''' Gun Powder AAA
    ''' </summary>
    <Description("Gun Powder AAA")>
    GunPowder_AAA = &H69 ' Gun powder AAA
    ''' <summary>
    ''' Gun Powder AAB
    ''' </summary>
    <Description("Gun Powder AAB")>
    GunPowder_AAB = &H6A ' Gun powder AAB
    ''' <summary>
    ''' Gun Powder BBA
    ''' </summary>
    <Description("Gun Powder BBA")>
    GunPowder_BBA = &H6B ' Gun powder BBA
    ''' <summary>
    ''' Gun Powder BBB
    ''' </summary>
    <Description("Gun Powder BBB")>
    GunPowder_BBB = &H6C ' Gun powder BBB
    ''' <summary>
    ''' Gun Powder CCC
    ''' </summary>
    <Description("Gun Powder CCC")>
    GunPowder_CCC = &H6D ' Gun powder CCC
    ''' <summary>
    ''' Infinite Bullets
    ''' </summary>
    <Description("Infinite Bullets")>
    InfiniteBullets = &H6E ' Infinite bullets
    ''' <summary>
    ''' Water Sample
    ''' </summary>
    <Description("Water Sample")>
    WaterSample = &H6F ' Water sample
    ''' <summary>
    ''' System Disk
    ''' </summary>
    <Description("System Disk")>
    SystemDisk = &H70 ' System disk
    ''' <summary>
    ''' Unknown item (Dummy Key)
    ''' </summary>
    ''' <remarks>
    ''' This Is the spade shaped precint key from RE2
    ''' </remarks>
    <Description("Unknown item (Dummy Key)")>
    Unknown_DummyKey = &H71 ' Dummy key
    ''' <summary>
    ''' Lockpick
    ''' </summary>
    <Description("Lockpick")>
    Lockpick = &H72 ' Lockpick
    ''' <summary>
    ''' Warehouse (Backdoor) Key
    ''' </summary>
    <Description("Warehouse (Backdoor) Key")>
    WarehouseKey = &H73 ' Warehouse (backdoor) key
    ''' <summary>
    ''' Sickroom Key (Room 402)
    ''' </summary>
    <Description("Sickroom Key (Room 402)")>
    SickroomKey = &H74 ' Sickroom key (room 402)
    ''' <summary>
    ''' Emblem (S.T.A.R.S) Key
    ''' </summary>
    <Description("Emblem (S.T.A.R.S) Key")>
    STARSKey = &H75 ' Emblem (S.T.A.R.S) key
    ''' <summary>
    ''' Unknown item (Keyring with 4 unknown keys)
    ''' </summary>
    <Description("Unknown item (Keyring with 4 unknown keys)")>
    Unknown_Keyring = &H76 ' Unknown keyring with 4 unknown keys
    ''' <summary>
    ''' Clock tower (bezel) key
    ''' </summary>
    <Description("Clock tower (bezel) key")>
    ClockTowerBezelKey = &H77 ' Clock tower (bezel) key
    ''' <summary>
    ''' Clock tower (winder) key
    ''' </summary>
    <Description("Clock tower (winder) key")>
    ClockTowerWinderKey = &H78 ' Clock tower (winder) key
    ''' <summary>
    ''' Chronos Key
    ''' </summary>
    <Description("Chronos Key")>
    ChronosKey = &H79 ' Chronos key
    ''' <summary>
    ''' Unknown item (Sigpro SP 2009 handgun)
    ''' </summary>
    ''' <remarks>
    ''' Cannot be used as a weapon, puzzle item perhaps?
    ''' </remarks>
    <Description("Unknown item (Sigpro SP 2009 handgun)")>
    Unknown_BOTU6 = &H7A ' Unknown Sigpro SP 2009 handgun
    ''' <summary>
    ''' Park Key (main gate)
    ''' </summary>
    <Description("Park Key (main gate)")>
    ParkMainGateKey = &H7B ' Park (main gate) key
    ''' <summary>
    ''' Park Key (graveyard)
    ''' </summary>
    <Description("Park Key (graveyard)")>
    ParkGraveyardKey = &H7C ' Park (graveyard) key
    ''' <summary>
    ''' Park Key (rear gate)
    ''' </summary>
    <Description("Park Key (rear gate)")>
    ParkRearGateKey = &H7D ' Park (rear gate) key
    ''' <summary>
    ''' Facility Key (no barcode)
    ''' </summary>
    <Description("Facility Key (no barcode)")>
    FacilityKey_NoBarcode = &H7E ' Facility key (no barcode)
    ''' <summary>
    ''' Facility Key (with barcode)
    ''' </summary>
    <Description("Facility Key (with barcode)")>
    FacilityKey_WithBarcode = &H7F ' Facility key (with barcode)
    ''' <summary>
    ''' Boutique Key
    ''' </summary>
    <Description("Boutique Key")>
    BoutiqueKey = &H80 ' Boutique key
    ''' <summary>
    ''' Ink Ribbon
    ''' </summary>
    <Description("Ink Ribbon")>
    InkRibbon = &H81 ' Ink ribbon
    ''' <summary>
    ''' Reloading Tool
    ''' </summary>
    <Description("Reloading Tool")>
    ReloadingTool = &H82 ' Reloading tool
    ''' <summary>
    ''' Game Instructions A
    ''' </summary>
    <Description("Game Instructions A")>
    GameInstructionsA = &H83 ' Game inst. A
    ''' <summary>
    ''' Game Instructions B
    ''' </summary>
    <Description("Game Instructions B")>
    GameInstructionsB = &H84 ' Game inst. B
    ''' <summary>
    ''' Unknown item (Book A)
    ''' </summary>
    ''' <remarks>
    ''' Not a book exactly, but a glass vial with blue chemical inside
    ''' </remarks>
    <Description("Unknown item (Book A)")>
    Unknown_BOTU7 = &H85 ' Game instructions A

End Enum
