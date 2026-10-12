// Title: Barcode generation, reading, and quality evaluation example
// Description: Demonstrates creating a Code128 barcode image, reading it with Aspose.BarCode, and interpreting the ReadingQuality value to inform the user when the scan quality is none.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to generate barcodes for labeling, then read them back to verify data integrity and assess scan quality using BarCodeResult.ReadingQuality.
// Prompt: Interpret a ReadingQuality value of 0 as none and prompt the user to rescan the barcode.
// Tags: barcode symbology, generation, recognition, readingquality, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple demonstration of barcode generation, reading, and quality assessment using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, evaluates the reading quality, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a simple Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode from the generated image and evaluate its reading quality
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                if (result.ReadingQuality == 0)
                {
                    // ReadingQuality of 0 indicates no quality; prompt for a rescan
                    Console.WriteLine("Reading quality is none, please rescan the barcode.");
                }
                else
                {
                    Console.WriteLine($"Reading quality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}