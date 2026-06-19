using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode image, reading it, and evaluating the reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a temporary barcode, reads it, logs quality information, and cleans up.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // 1. Generate a temporary barcode image and save it to the system temp folder.
        // --------------------------------------------------------------------
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the barcode as a PNG file.
            generator.Save(tempImagePath);
        }

        // Verify that the image was created successfully.
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // 2. Read the barcode from the generated image and evaluate its ReadingQuality.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate over all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                double readingQuality = result.ReadingQuality;

                // Output the decoded text and its quality metric.
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {readingQuality}");

                // Values 1‑99 indicate moderate quality; log a warning for this range.
                if (readingQuality >= 1 && readingQuality <= 99)
                {
                    Console.WriteLine($"Warning: ReadingQuality {readingQuality} is moderate.");
                }
            }
        }

        // --------------------------------------------------------------------
        // 3. Clean up the temporary barcode image file.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}