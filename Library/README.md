# Library (platform-agnostic class library)

`Library` contains the data model and helpers for working with Resident Evil 3 `.bio3` save files. It is intentionally UI-free so it can be referenced from any .NET project (desktop, web, console, or other platforms that support .NET).

This library is proud to be fully developed in Visual Basic .NET.

## Key features

- `SaveGame` model for reading, modifying and saving `.bio3` files.
- `Inventory` and `Item` types to represent player inventories and items.
- Enum helpers and descriptions in `Utils` for display text.
- `ItemImage` helper that maps `ItemID` → image atlas index and exposes a platform-agnostic `Stream` to the embedded item atlas PNG via `OpenItemAtlasStream()`.

## Target framework

- The project targets `netstandard2.0` to maximize compatibility across runtime types. See the project file `Library.vbproj` for details.

## Getting started

1. Reference the `Library` project or its compiled assembly from your application.
2. Use `SaveGame` to load a save file, inspect or modify values, and call `Save()` to persist changes.

Example: load, modify, and save

```vbnet
Dim sg = New Library.SaveGame("C:\path\to\save.bio3")
sg.EpiloguesUnlocked = 8
sg.Save()
```

### Important note about inventories and items

- `Inventory` returns new `Inventory` instances constructed from the internal byte buffer on each call (this keeps the internal representation simple). Also `Item` is a `Structure` (value type).
- To persist item or inventory changes back into the save-memory, assign modified inventories back to the `SaveGame` properties, for example:

```vbnet
Dim inv = sg.JillsInventory     ' returns a new Inventory constructed from internal bytes
inv.SetItem(0, New Library.Item(Library.ItemID.Handgun, 15, Library.ItemAttribute.AmmoDisplayBlue))
sg.JillsInventory = inv         ' writes back Inventory.ToBytes() into SaveGame internal buffer
sg.Save()
```

### Item image atlas access (UI-agnostic)

- The library embeds the item atlas PNG as an assembly resource. To avoid pulling UI dependencies into the library, the API exposes a `Stream` to that embedded image resource:

```vbnet
Dim stream As IO.Stream = Library.ItemImage.OpenItemAtlasStream()
If stream IsNot Nothing Then
    ' Your UI code can create platform-specific image objects from the stream.
End If
```

- UI projects (WPF, WinUI, etc.) should consume the stream and construct the appropriate image objects. The sample WPF UI in this repository (`Bio3SourcenextSaveEditor`) uses that stream to create a `BitmapImage` and to draw `DrawingVisual` item tiles.

### Other helpers

- `Library.Utils` provides methods to enumerate enums and produce human-readable descriptions (used by UI combo boxes and lists).
- `ItemImage.ItemIDToImageIndex(itemID)` returns the atlas index for a given `ItemID`.

## Contributing

- The library is intended to remain UI-free. If you need to add UI-specific helpers, add them in the consuming project instead.
- Open issues and pull requests for bug fixes, improvements, or missing mappings.

## License

- Check the repository root `LICENSE` file for license terms.

## Questions

- For integration questions, examples, or if you want a small helper added (for example: a `Stream` provider that accepts resource name or culture-specific variants), open an issue describing your use-case.