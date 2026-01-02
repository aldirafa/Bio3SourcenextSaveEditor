# Bio3SourcenextSaveEditor — Solution README

This repository contains a small solution for editing Resident Evil 3 `.bio3` save files for The Resident Evil 3: Nemesis (Sourcenext) version. It is split into two projects:

- `Bio3SourcenextSaveEditor` — a WPF desktop application providing a graphical editor for save files.
- `Library` — a platform-agnostic class library that implements the save-file model and helpers.

Both projects are developed in Visual Basic .NET.

## Purpose

The solution demonstrates a separation of concerns between UI and core logic:
- The `Library` project performs all save-file parsing, in-memory modifications, and exposes a minimal UI-agnostic API (including a stream to the embedded item atlas asset).
- The `Bio3SourcenextSaveEditor` project implements a responsive WPF user interface that consumes the `Library` API and renders item images.

## Contents

- `Library`
  - `SaveGame` — main model for reading/writing `.bio3` save files.
  - `Inventory`, `Item`, and enums describing in-save values.
  - `ItemImage` mapping (ItemID → atlas index) and `OpenItemAtlasStream()` to get a read-only `Stream` to the embedded atlas image.
  - Targets `netstandard2.0` so it can be referenced from many types of .NET projects.

- `Bio3SourcenextSaveEditor`
  - WPF UI project that depends on `Library`.
  - Implements drawing/rendering helpers for items in `ItemImages/ItemImage.vb` (WPF-specific).
  - Asynchronous load/save flows and status reporting in `MainWindow`.

## Getting Started

To get started with Bio3SourcenextSaveEditor, follow these steps:

1. **Installation**:
   - Ensure you have the **.NET SDK** installed on your machine. This solution targets **.NET 6**. You can download it from the [.NET downloads page](https://dotnet.microsoft.com/download).

   - Clone this repository to your local machine using Git:
     ```bash
     git clone https://github.com/yourusername/Bio3SourcenextSaveEditor.git
     ```
     (Replace `yourusername` with the actual username or organization name on GitHub.)

   - Open the solution file `Bio3SourcenextSaveEditor.sln` in **Visual Studio**.

2. **Building the Solution**:
   - Restore the required NuGet packages. In Visual Studio, this can be done by right-clicking on the solution in the Solution Explorer and selecting "Restore NuGet Packages".
   - Build the solution by clicking on the "Build" menu and selecting "Build Solution" or simply press `Ctrl + Shift + B`.

3. **Running the Application**:
   - Set `Bio3SourcenextSaveEditor` as the startup project. Right-click on the project in the Solution Explorer and select "Set as Startup Project".
   - Run the application by clicking on the "Start" button in Visual Studio or press `F5`.

4. **Loading and Editing a Save File**:
   - Once the application is running, click on the "Load Save File" button.
   - Navigate to and select a `.bio3` save file.
   - Make the desired changes to the save file using the application's interface.
   - Save the changes by clicking on the "Save" button.

## Important Usage Notes

- **Inventory and Item semantics**:
  - `Inventory` getters construct and return new `Inventory` instances from the internal save buffer. `Item` is a value type (`Structure`). To persist changes to the `SaveGame` internal buffer, you must set the modified `Inventory` back on the `SaveGame` property (e.g., `sg.JillsInventory = inv`).

- **Item atlas resource**:
  - The item atlas PNG is embedded in the `Library` assembly. The library exposes `OpenItemAtlasStream()` which returns a `Stream` that UI code can use to build UI-specific image objects. This avoids bringing UI dependencies into the `Library` project.

    Example (WPF):

    ```vb
    Dim s = Library.ItemImage.OpenItemAtlasStream()
    If s IsNot Nothing Then
        Dim bmp As New System.Windows.Media.Imaging.BitmapImage()
        bmp.BeginInit()
        bmp.StreamSource = s
        bmp.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad
        bmp.EndInit()
        ' Use bmp
    End If
    ```

## Troubleshooting

- If images do not appear, ensure the embedded resource `ItemImages/26073.png` is present in the `Library` assembly manifest. You can inspect assembly resources or ensure the `Library.vbproj` includes the PNG as an `EmbeddedResource`.
- If modifications are not persisted when saving, double-check that you wrote changes back into the `SaveGame` (see Inventory note above). Many helpers create copies; the `SaveGame` writes the raw bytes when `Save()` is called.

## Contributing

- The solution adopts a clear UI / core split. When adding features that involve visuals (drawing item overlays, alternative renderers), add them to the UI project rather than `Library`.
- For fixes to save parsing, enum mappings, or resource packaging, update the `Library` project.
- Open issues or pull requests with reproducible steps and example save files where possible.

## License

- See `LICENSE` in the repository root for licensing terms.

## Contact and Feedback

- Open issues in this repository for bugs, feature requests, or questions.
- When opening issues provide steps to reproduce and the environment you are using (OS, .NET version, Visual Studio version).