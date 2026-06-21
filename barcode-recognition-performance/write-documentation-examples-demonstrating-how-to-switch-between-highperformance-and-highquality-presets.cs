using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode and reading it with different quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and reads it using HighPerformance and HighQuality settings.
    /// </summary>
    static void Main()
    {
        // -------------------------------------------------
        // Generate a simple Code128 barcode and save it to a file.
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the generated barcode image as PNG.
            generator.Save("code128.png");
        }

        // -------------------------------------------------
        // Read the barcode using the HighPerformance preset.
        // -------------------------------------------------
        using (var reader = new BarCodeReader("code128.png", DecodeType.Code128))
        {
            // Apply the HighPerformance quality settings to speed up processing.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Iterate through all detected barcodes (should be one) and output the decoded text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"HighPerformance - Detected Text: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Read the same barcode using the HighQuality preset.
        // -------------------------------------------------
        using (var reader = new BarCodeReader("code128.png", DecodeType.Code128))
        {
            // Apply the HighQuality quality settings for maximum accuracy.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all detected barcodes and output the decoded text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"HighQuality - Detected Text: {result.CodeText}");
            }
        }
    }
}