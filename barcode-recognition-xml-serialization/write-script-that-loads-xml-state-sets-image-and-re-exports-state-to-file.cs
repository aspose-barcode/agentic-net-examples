// Title: Load BarCodeReader XML state, set image, and re‑export
// Description: Demonstrates loading a previously exported BarCodeReader XML state, assigning a barcode image, and exporting the updated state back to XML.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to use BarCodeReader.ImportFromXml and ExportToXml for persisting reader configurations. It covers generating a barcode image with BarcodeGenerator, configuring BarCodeReader settings, importing a saved state, setting a barcode image, and re‑exporting the state. Developers working with barcode scanning automation often need to save and restore reader settings across sessions, making these APIs essential for reliable, repeatable barcode processing pipelines.
// Prompt: Write a script that loads an XML state, sets an image, and re‑exports the state to a file.
// Tags: barcode symbology, state import, state export, image handling, aspose.barcode, qr, xml

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, exports a BarCodeReader state to XML,
/// imports the state, assigns the generated image, and re‑exports the updated state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, state export/import,
    /// and re‑export workflow.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder to store generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the barcode image, original state XML, and re‑exported state XML
        string imagePath = Path.Combine(workFolder, "sample.png");
        string statePath = Path.Combine(workFolder, "readerState.xml");
        string reexportPath = Path.Combine(workFolder, "readerStateReexport.xml");

        // 1. Generate a sample QR code image and save it as PNG
        var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText");
        generator.Parameters.Barcode.XDimension.Pixels = 4f;
        generator.Save(imagePath, BarCodeImageFormat.Png);

        // 2. Create a BarCodeReader, configure settings, and export its state to XML
        using (var writer = new BarCodeReader())
        {
            writer.BarcodeSettings.StripFNC = true;
            writer.QualitySettings.XDimension = XDimensionMode.Small;
            writer.ExportToXml(statePath);
        }

        // 3. Import the previously saved state, assign the barcode image, and re‑export
        using (var reader = BarCodeReader.ImportFromXml(statePath))
        {
            // Verify that the generated image file exists before assigning it
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Image file not found: {imagePath}");
                return;
            }

            // Set the barcode image that the reader will process
            reader.SetBarCodeImage(imagePath);

            // Optionally specify the expected decode type (matches the generated QR code)
            reader.SetBarCodeReadType(DecodeType.QR);

            // Export the updated reader state, now containing the image reference
            reader.ExportToXml(reexportPath);
        }

        // Output the locations of the exported XML files for verification
        Console.WriteLine("Original state exported to: " + statePath);
        Console.WriteLine("Re‑exported state with image set to: " + reexportPath);
    }
}