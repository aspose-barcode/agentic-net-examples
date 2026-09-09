// Title: Generate QR barcode, export/import reader state via XML, and decode it
// Description: This example creates a QR code image, saves the barcode reader's configuration to an XML file, then imports that XML to read the barcode again, demonstrating state persistence.
// Category-Description: This sample belongs to the Aspose.BarCode state management category, showcasing how to use BarcodeGenerator, BarCodeReader, and XML export/import APIs. Developers often need to persist reader settings for batch or background processing scenarios, such as automated services that monitor folders, import XML states, and decode pending barcode images.
// Prompt: Develop a background service that monitors a folder, imports XML states, and processes pending barcode images automatically.
// Tags: qr, barcode generation, barcode recognition, xml export, xml import, aspose.barcode, state management

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode, exporting the reader state to XML,
/// importing the state back, and decoding the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, state export/import,
    /// and decoding workflow.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the demo
        string workFolder = Path.Combine(Path.GetTempPath(), "BarCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define paths for the generated image and the exported XML state
        string imagePath = Path.Combine(workFolder, "sample.png");
        string xmlPath = Path.Combine(workFolder, "readerState.xml");

        // Generate a sample QR barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            // Set the module size (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Create a BarCodeReader, configure it, and export its state to XML
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Example setting: ignore FNC characters during decoding
            reader.BarcodeSettings.StripFNC = true;
            // Export the current reader configuration to an XML file
            reader.ExportToXml(xmlPath);
        }

        // List of XML state files to process (explicitly created list)
        List<string> xmlFiles = new List<string> { xmlPath };

        // Process each XML state file: import settings, set image, read barcodes
        foreach (string xmlFile in xmlFiles)
        {
            if (!File.Exists(xmlFile))
            {
                Console.WriteLine($"XML file not found: {xmlFile}");
                continue;
            }

            // Import the reader configuration from the XML file
            using (var importedReader = BarCodeReader.ImportFromXml(xmlFile))
            {
                // The image source is not stored in the XML, set it explicitly
                importedReader.SetBarCodeImage(imagePath);

                BarCodeResult[] results;
                try
                {
                    // Attempt to read barcodes from the image using the imported settings
                    results = importedReader.ReadBarCodes();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Failed to read barcodes from image: {ex.Message}");
                    continue;
                }

                // Output the decoding results
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    }
                }
            }
        }

        // Cleanup: delete temporary folder and its contents
        try
        {
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}