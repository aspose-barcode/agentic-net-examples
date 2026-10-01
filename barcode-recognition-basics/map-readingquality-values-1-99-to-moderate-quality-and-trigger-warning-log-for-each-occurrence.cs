// Title: Barcode Generation, Reading, and Quality Evaluation Demo
// Description: This example generates a Code128 barcode, saves it as a PNG image, reads it back, and evaluates the ReadingQuality property to identify moderate-quality barcodes.
// Category-Description: Demonstrates core Aspose.BarCode operations—barcode generation with BarcodeGenerator, image saving with BarCodeImageFormat, and barcode recognition using BarCodeReader and BarCodeResult. Typical use cases include creating barcodes for inventory, scanning them, and assessing scan quality to trigger alerts. Developers often need to combine these APIs to automate barcode workflows and monitor read reliability.
// Prompt: Map ReadingQuality values 1‑99 to moderate quality and trigger a warning log for each occurrence.
// Tags: barcode symbology, generation, recognition, readingquality, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a barcode, read it, and evaluate its reading quality using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a Code128 barcode, saves it, reads it back, and logs quality information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and evaluate its ReadingQuality
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                double qualityDouble = result.ReadingQuality;
                int quality = (int)qualityDouble;

                // Map quality 1‑99 to moderate and log a warning; otherwise log informational message
                if (quality >= 1 && quality <= 99)
                {
                    Console.WriteLine($"Warning: Barcode '{result.CodeText}' has moderate quality (ReadingQuality = {quality}).");
                }
                else
                {
                    Console.WriteLine($"Info: Barcode '{result.CodeText}' has quality {quality} (outside moderate range).");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect demo execution
        }
    }
}