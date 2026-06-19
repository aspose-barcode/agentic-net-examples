using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code, reading it back, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, reads it using Aspose.BarCode, displays details, and deletes the temporary file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a QR code image with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Save the barcode as a PNG file at the temporary location
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at: {imagePath}");
            return;
        }

        // Read barcodes from the image and output confidence scores
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output barcode type, text, confidence, and reading quality
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"BarCode Confidence: {result.Confidence}");
                Console.WriteLine($"BarCode ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}