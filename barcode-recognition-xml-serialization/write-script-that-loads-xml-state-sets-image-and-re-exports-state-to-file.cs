// Title: Load BarCodeReader state from XML, set image, and re-export state
// Description: This example demonstrates loading a previously saved BarCodeReader XML state, assigning a barcode image for recognition, and exporting the updated state back to XML.
// Category-Description: Shows how to work with Aspose.BarCode's state management APIs, including BarCodeReader, ExportToXml, and ImportFromXml. Typical use cases involve persisting reader configurations, reusing them across sessions, and dynamically assigning images for decoding. Developers often need to save and reload reader settings to streamline batch processing or integrate with external workflows.
// Prompt: Write a script that loads an XML state, sets an image, and re‑exports the state to a file.
// Tags: code128, xml, state, export, import, barcode reader, aspose.barcode, png, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates loading a BarCodeReader state from XML, assigning an image, and re‑exporting the updated state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode, saves reader state, reloads it with an image, reads barcodes, and re‑exports the state.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder to store generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the barcode image and XML state files
        string barcodePath = Path.Combine(workFolder, "sample.png");
        string statePath = Path.Combine(workFolder, "readerState.xml");
        string reloadedStatePath = Path.Combine(workFolder, "readerStateReloaded.xml");

        // 1. Generate a sample Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // 2. Create a BarCodeReader, configure its settings, and export its initial state to XML
        using (BarCodeReader reader = new BarCodeReader())
        {
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.ExportToXml(statePath);
        }

        // 3. Load the saved reader state, assign the barcode image, set the decode type, and re‑export the updated state
        using (BarCodeReader loadedReader = BarCodeReader.ImportFromXml(statePath))
        {
            // Assign the image that should be processed by the reader
            loadedReader.SetBarCodeImage(barcodePath);

            // Restrict decoding to Code128 barcodes only
            loadedReader.SetBarCodeReadType(DecodeType.Code128);

            // Perform barcode recognition to demonstrate that the configuration works
            BarCodeResult[] results = loadedReader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Export the modified reader state back to a new XML file
            loadedReader.ExportToXml(reloadedStatePath);
        }

        // Output the locations of the generated files for reference
        Console.WriteLine($"Initial state saved to: {statePath}");
        Console.WriteLine($"Reloaded state saved to: {reloadedStatePath}");
        Console.WriteLine($"Generated barcode image: {barcodePath}");
    }
}