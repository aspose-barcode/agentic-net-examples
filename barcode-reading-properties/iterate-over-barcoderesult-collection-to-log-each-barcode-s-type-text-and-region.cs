// Title: Barcode generation, recognition, and logging example
// Description: Demonstrates creating barcodes, reading them back, and logging each barcode's type, text, and region.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator for creating various symbologies and BarCodeReader for decoding them. Typical use cases include batch processing of barcode images, extracting metadata, and integrating barcode data into workflows. Developers often need to iterate over BarCodeResult collections to retrieve code type, decoded text, and positional information.
// Prompt: Iterate over BarCodeResult collection to log each barcode's type, text, and region.
// Tags: barcode symbology, generation, recognition, logging, aspose.barcode, barcoderesult, region

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate multiple barcode images, read them back,
/// and log each barcode's type, decoded text, and region information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them,
    /// and writes details to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodesDemo");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // --------------------------------------------------------------------
        // Define sample barcodes (symbology and associated text)
        // --------------------------------------------------------------------
        var samples = new (BaseEncodeType Encode, string Text)[]
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345")
        };

        // --------------------------------------------------------------------
        // Generate barcode images and save them as PNG files
        // --------------------------------------------------------------------
        foreach (var (encode, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encode.TypeName}_{Guid.NewGuid()}.png");
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Optional: set simple visual parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Read all generated images and log barcode details
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(tempFolder, "*.png");
        foreach (string imageFile in imageFiles)
        {
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            using (var reader = new BarCodeReader(imageFile, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                // Iterate over each detected barcode result
                foreach (BarCodeResult result in results)
                {
                    // Extract region rectangle for positional information
                    var rect = result.Region.Rectangle;

                    // Log file name, barcode type, decoded text, and region coordinates
                    Console.WriteLine($"File: {Path.GetFileName(imageFile)}");
                    Console.WriteLine($"  Type: {result.CodeType}");
                    Console.WriteLine($"  Text: {result.CodeText}");
                    Console.WriteLine($"  Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Cleanup (optional): delete temporary files and folder
        // --------------------------------------------------------------------
        // foreach (string file in imageFiles) File.Delete(file);
        // Directory.Delete(tempFolder);
    }
}