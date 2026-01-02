# Bio3SourcenextSaveEditor (WPF UI)

A WPF-based graphical editor for Resident Evil 3 `.bio3` save files. This project provides the user interface for loading, viewing, editing and saving save-game data using the platform-agnostic `Library` project for the save model and helpers.

This project is proud to be fully developed in Visual Basic .NET.

## Quick overview

- Project: `Bio3SourcenextSaveEditor` (WPF application)
- Purpose: edit `.bio3` save files (inventory, player state, variables)
- Depends on: `Library` (provides `SaveGame`, `Inventory`, `Item`, enums and helpers)

## Prerequisites

- Windows with .NET Desktop (WPF) support.
- .NET SDK / Visual Studio with the .NET Desktop Development workload.
- Recommended .NET SDK: .NET 6 or later (project uses WPF runtime).

## Build and run

1. Open the solution in Visual Studio or your preferred IDE.
2. Set `Bio3SourcenextSaveEditor` as the startup project.
3. Build the solution.
4. Run the application. Use the "Load Save File" button to open a `.bio3` save.

## Usage

- Load a save file using the top-left "Load Save File" button.
- Edit save variables in the "Save Data Variables" panel.
- Edit items by clicking an item tile — a dialog opens where you can change item ID, ammo and attributes.
- Save changes using the `Save` action in the UI (add a `SaveButton` if needed).

## Asynchronous operations and status

- Loading and saving run asynchronously so the UI remains responsive.
- Progress and status messages are displayed in `StatusTextBlock`.
- Controls are temporarily disabled while load/save is active to avoid conflicting changes.

## Item atlas and images

The item atlas (`26073.png`) is embedded into the `Library` assembly as a resource. The WPF project renders item images using the stream API exposed by the library so the library remains UI-agnostic.

Example: open the embedded atlas stream directly from `Library` (VB.NET / WPF):

```vbnet
Dim stream = Library.ItemImage.OpenItemAtlasStream()
If stream IsNot Nothing Then
    Dim bmp As New System.Windows.Media.Imaging.BitmapImage()
    bmp.BeginInit()
    bmp.StreamSource = stream
    bmp.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad
    bmp.EndInit()
    ' Use bmp as an ImageSource
End If
```

This project contains UI helpers in `Bio3SourcenextSaveEditor/ItemImages/ItemImage.vb` which read that stream and produce WPF `ImageSource` and `DrawingVisual` objects for item drawing.

## Project structure

- `MainWindow.xaml` / `MainWindow.xaml.vb` — main UI and logic.
- `SaveDataInformation.xaml` / `.vb` — save-data editing control.
- `ItemImages/ItemImage.vb` — UI drawing helpers for items (loads atlas using `Library.ItemImage.OpenItemAtlasStream`).
- Other UI components under the project folder.

## Contributing and feedback

- Open issues or PRs on the repository to report bugs or request features.
- For quick feedback, add an issue explaining steps to reproduce and relevant save files if possible.

## License

This project contains code and assets from multiple sources. Check the repository root for a `LICENSE` file and follow its terms when redistributing.