// Title: Batch barcode generation and recognition with StripFNC disabled
// Description: This example creates multiple GS1‑Code128 barcode images that contain FNC symbols, then reads them back while preserving those symbols.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition in a batch workflow. It uses BarcodeGenerator to encode GS1 data, BarCodeReader with DecodeType.Code128 to decode, and BarcodeSettings.StripFNC to control FNC handling. Typical scenarios include processing large sets of GS1 barcodes where FNC characters must remain intact, such as inventory or logistics applications. Developers often need to generate barcodes, store them as images, and later read them without losing embedded control characters.
// Prompt: Create a batch process that reads multiple images with StripFNC false to keep FNC symbols.
// Tags: barcode, gs1code128, stripfnc, batch-processing, generation, recognition, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of GS1‑Code128 barcodes containing FNC characters
/// and subsequent recognition while preserving those characters (StripFNC = false).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, then reads each image back
    /// with FNC symbols retained.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for generated barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Sample GS1 data strings that include FNC (Function) characters
        // --------------------------------------------------------------------
        string[] sampleTexts = new[]
        {
            "(02)04006664241007(37)1(400)7019590754",
            "(01)12345678901231(10)ABC123",
            "(01)98765432109876(21)XYZ789"
        };

        // --------------------------------------------------------------------
        // Generate a PNG barcode image for each sample text using GS1Code128
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sampleTexts[i]))
            {
                generator.Save(filePath);
            }
        }

        // --------------------------------------------------------------------
        // Locate all generated PNG files for batch processing
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode images found to process.");
            return;
        }

        // --------------------------------------------------------------------
        // Read each image, ensuring FNC characters are NOT stripped (StripFNC = false)
        // --------------------------------------------------------------------
        foreach (string imageFile in imageFiles)
        {
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            using (var reader = new BarCodeReader(imageFile, DecodeType.Code128))
            {
                // Disable automatic removal of FNC symbols
                reader.BarcodeSettings.StripFNC = false;

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in {Path.GetFileName(imageFile)}.");
                    continue;
                }

                Console.WriteLine($"Barcodes in {Path.GetFileName(imageFile)}:");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                }
            }
        }
    }
}