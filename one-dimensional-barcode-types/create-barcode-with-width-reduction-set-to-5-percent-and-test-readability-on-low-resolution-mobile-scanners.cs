using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode with bar‑width reduction,
/// saving it to a file, and then reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, verifies its creation, and attempts to read it.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a Code128 barcode generator with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Simulate a low‑resolution mobile scanner by setting a low DPI.
            generator.Parameters.Resolution = 72f; // 72 DPI

            // Apply a 5 % bar‑width reduction to the barcode.
            generator.Parameters.Barcode.BarWidthReduction.Point = 5f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Initialize a barcode reader for the generated image, supporting all barcode types.
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            // Use a high‑quality setting to improve recognition on low‑resolution images.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Perform the barcode recognition.
            var results = reader.ReadBarCodes();

            // Check if any barcodes were detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output details for each detected barcode.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Confidence: {result.Confidence}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}