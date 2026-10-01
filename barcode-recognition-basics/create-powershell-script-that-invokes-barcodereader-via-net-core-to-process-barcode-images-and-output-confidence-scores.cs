// Title: Generate and Read Barcode with Confidence Scores using Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode image, saving it as PNG, then reading it with Aspose.BarCode to output the decoded text, symbology, and reading quality (confidence score).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for encoding barcodes and BarCodeReader for decoding them, including quality settings to improve confidence scores. Developers working with barcode automation, inventory systems, or document processing often need to generate barcodes and later verify them programmatically; this snippet provides a concise reference for those common tasks.
// Prompt: Create a PowerShell script that invokes BarCodeReader via .NET Core to process barcode images and output confidence scores.
// Tags: barcode generation, barcode recognition, confidence score, code128, aspose.barcode, .net core

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and reading using Aspose.BarCode, outputting confidence scores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it, and prints decoding results with quality scores.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary folder for sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode parameters
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Generate a barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Barcode image not found: {barcodePath}");
            return;
        }

        // Read the barcode and output confidence scores
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Optional: set quality settings for higher accuracy
            reader.QualitySettings = QualitySettings.HighQuality;

            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text       : {result.CodeText}");
                    Console.WriteLine($"Symbology       : {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality : {result.ReadingQuality}");
                    Console.WriteLine(new string('-', 30));
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}