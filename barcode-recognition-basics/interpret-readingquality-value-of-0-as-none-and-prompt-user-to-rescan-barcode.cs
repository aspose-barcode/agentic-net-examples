// Title: Demonstrate ReadingQuality Evaluation and Rescan Prompt for Code128 Barcode
// Description: Generates a Code128 barcode, reads it, checks the ReadingQuality value, and prompts the user to rescan when quality is none (0).
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings to decode them. Developers often need to assess reading quality to decide whether a barcode scan is reliable, and this pattern is common in inventory, logistics, and point‑of‑sale applications.
// Prompt: Interpret a ReadingQuality value of 0 as none and prompt the user to rescan the barcode.
// Tags: barcode symbology, generation, recognition, readingquality, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates a Code128 barcode, reads it, and evaluates the reading quality.
/// If the quality is reported as zero, the user is prompted to rescan the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode from the image and evaluate its reading quality.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply a standard quality preset (optional but demonstrates API usage).
            reader.QualitySettings = QualitySettings.NormalQuality;

            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                // No barcode detected – advise the user to scan again.
                Console.WriteLine("No barcode detected. Please rescan the barcode.");
            }
            else
            {
                // Process each detected barcode.
                foreach (BarCodeResult result in results)
                {
                    double quality = result.ReadingQuality;

                    if (quality == 0)
                    {
                        // Quality of zero indicates "none" – prompt for a rescan.
                        Console.WriteLine("Reading quality is none. Please rescan the barcode.");
                    }
                    else
                    {
                        // Output detailed information for a successful read.
                        Console.WriteLine($"Reading quality: {quality}");
                        Console.WriteLine($"Code Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome.
        }
    }
}