using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes, saving them as images,
/// recognizing them, and exporting recognition checkpoints using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, saves them, reads them back,
    /// and writes XML checkpoints for each detection.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated images and checkpoint files.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define a collection of sample barcodes to generate.
        // Each tuple contains the barcode symbology type and the text to encode.
        var samples = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "ABC123456"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.EAN13, "5901234123457")
        };

        // Iterate over each sample, generate the barcode image, and then recognize it.
        for (int i = 0; i < samples.Length; i++)
        {
            // Deconstruct the tuple into symbology and text.
            var (symbology, codeText) = samples[i];

            // Build the file path for the barcode image.
            string imagePath = Path.Combine(outputDir, $"barcode_{i}.png");

            // -------------------------
            // Generate barcode image
            // -------------------------
            using (BarcodeGenerator generator = new BarcodeGenerator(symbology, codeText))
            {
                // Save the generated barcode as a PNG file.
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // -------------------------
            // Recognize barcode and export checkpoint after each detection
            // -------------------------
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes detected in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Process each detection result.
                for (int j = 0; j < results.Length; j++)
                {
                    BarCodeResult result = results[j];

                    // Output detection details to the console.
                    Console.WriteLine($"Image {i}, Detection {j}: Type={result.CodeTypeName}, Text={result.CodeText}");

                    // Export the reader's internal state to an XML file as a checkpoint.
                    string checkpointPath = Path.Combine(outputDir, $"checkpoint_{i}_{j}.xml");
                    reader.ExportToXml(checkpointPath);
                }
            }
        }

        // Indicate that processing has finished.
        Console.WriteLine("Processing completed.");
    }
}