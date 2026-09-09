// Title: Barcode checkpoint/restart demo using state export/import
// Description: Demonstrates exporting a BarCodeReader state to XML, closing the reader, reopening it, and continuing barcode detection on a PDF417 image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to manage reader state across application sessions. It uses BarcodeGenerator for creating barcodes, BarCodeReader for detection, and the ExportToXml/ImportFromXml methods to persist and restore reader configuration. Developers often need checkpoint/restart capabilities when processing large batches or when pausing/resuming scans.
// Prompt: Create a demo that shows checkpoint/restart by exporting state, closing the reader, reopening, and continuing detection.
// Tags: pdf417, checkpoint, restart, state export, state import, barcode generation, barcode recognition, aspose.barcode, csharp

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
    /// Entry point that creates a barcode, exports reader state, reimports it, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated barcode image and the exported reader state
        string imagePath = Path.Combine(tempFolder, "sample.png");
        string statePath = Path.Combine(tempFolder, "reader_state.xml");

        // Generate a sample PDF417 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "DemoCheckpoint"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2; // Set module size
            generator.Save(imagePath, BarCodeImageFormat.Png);   // Save as PNG
        }

        // Initialize a reader, configure it, and export its state to XML
        using (var reader = new BarCodeReader())
        {
            reader.SetBarCodeReadType(DecodeType.Pdf417); // Limit detection to PDF417
            reader.SetBarCodeImage(imagePath);           // Assign the barcode image
            reader.BarcodeSettings.StripFNC = true;     // Example setting
            reader.ExportToXml(statePath);               // Persist reader configuration
        }

        // Import the previously saved reader state, reassign image and read type, then continue detection
        using (var reader = BarCodeReader.ImportFromXml(statePath))
        {
            reader.SetBarCodeImage(imagePath);           // Reassign the image after import
            reader.SetBarCodeReadType(DecodeType.Pdf417); // Reapply read type

            var results = reader.ReadBarCodes();         // Perform detection
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(imagePath);
            File.Delete(statePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}