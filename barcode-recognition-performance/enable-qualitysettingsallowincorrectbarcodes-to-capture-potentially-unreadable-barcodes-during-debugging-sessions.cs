using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image, reading it with relaxed quality settings,
/// and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, displays detection details, and deletes the temporary image.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path for the generated barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a sample barcode image using Code128 encoding and the text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Save the generated barcode image to the temporary file
            generator.Save(imagePath);
        }

        // Verify that the image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader for all supported decode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Allow detection of barcodes that may be partially unreadable or of low quality
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output detection details to the console
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }

        // Attempt to delete the temporary barcode image; ignore any errors that occur
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Cleanup errors are intentionally ignored
        }
    }
}