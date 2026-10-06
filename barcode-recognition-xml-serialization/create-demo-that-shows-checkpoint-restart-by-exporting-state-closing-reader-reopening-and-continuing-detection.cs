// Title: Checkpoint/Restart Demo Using BarCodeReader State Export/Import
// Description: Demonstrates exporting a BarCodeReader's configuration to XML, closing the reader, reopening it, and continuing barcode detection on the same image.
// Category-Description: This example belongs to the Aspose.BarCode recognition and state‑management category. It showcases how to use BarcodeGenerator to create a barcode, BarCodeReader to configure recognition settings, and the ExportToXml/ImportFromXml methods to persist and restore reader state. Typical use cases include long‑running scanning jobs that need to be paused, checkpointed, and resumed without losing configuration. Developers often need to manage settings such as StripFNC, decode types, and image sources across application restarts.
// Prompt: Create a demo that shows checkpoint/restart by exporting state, closing the reader, reopening, and continuing detection.
// Tags: barcode symbology, checkpoint restart, state export, state import, generation, recognition, aspose.barcode, code128, xml

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates checkpoint/restart of barcode detection by exporting and importing reader state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, exports reader settings, re‑imports them, and continues detection.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary working folder for generated files.
        // ------------------------------------------------------------
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Paths for the barcode image and the exported reader state.
        string imagePath = Path.Combine(workFolder, "barcode.png");
        string statePath = Path.Combine(workFolder, "readerState.xml");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode image.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Initialize a BarCodeReader, configure a setting, and export its state.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Example setting: ignore FNC characters during decoding.
            reader.BarcodeSettings.StripFNC = true;

            // Export the current reader configuration to an XML file.
            reader.ExportToXml(statePath);
            Console.WriteLine($"Reader state exported to: {statePath}");
        }

        // Ensure the state file was written.
        if (!File.Exists(statePath))
        {
            Console.WriteLine("Failed to export reader state.");
            return;
        }

        // ------------------------------------------------------------
        // Import the previously saved reader state, set the image and decode type,
        // then continue barcode detection.
        // ------------------------------------------------------------
        using (var importedReader = BarCodeReader.ImportFromXml(statePath))
        {
            importedReader.SetBarCodeImage(imagePath);
            importedReader.SetBarCodeReadType(DecodeType.Code128);

            Console.WriteLine("Imported reader settings:");
            Console.WriteLine($"StripFNC: {importedReader.BarcodeSettings.StripFNC}");

            // Perform barcode detection using the restored settings.
            var results = importedReader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");

            // Output each detected barcode.
            foreach (var result in importedReader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files (optional).
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            File.Delete(statePath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}