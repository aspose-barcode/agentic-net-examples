// Title: Limit barcode reading to a maximum of three per image
// Description: Demonstrates generating Code128 barcodes, reading them, and restricting processing to three barcodes per image to reduce overhead.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and BarCodeResult for accessing decoded data. Typical scenarios include batch processing of images where limiting the number of decoded symbols improves performance, a common requirement for developers handling large volumes of scanned documents.
// Prompt: Set maximum number of barcodes per image to three to limit processing overhead.
// Tags: code128, barcode-generation, barcode-recognition, png, barcodereader, barcodegenerator, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Code128 barcodes, reads them back,
/// and limits the number of barcodes processed per image to three.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them with a limit of three per image,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode texts
        var texts = new List<string> { "ABC123", "DEF456", "GHI789" };
        var imagePaths = new List<string>();

        // Generate one barcode image per text
        foreach (string text in texts)
        {
            string imagePath = Path.Combine(tempFolder, $"barcode_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set X-dimension to control barcode size
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save the barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(imagePath);
        }

        // Read barcodes from each image, processing at most three per image
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                Console.WriteLine($"Reading barcodes from: {Path.GetFileName(path)}");
                int count = 0;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (count >= 3)
                        break; // Stop after three barcodes

                    Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                    count++;
                }
                Console.WriteLine($"  Barcodes processed: {count}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in imagePaths)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}