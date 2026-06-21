using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with a large XDimension and then recognizing it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, verifies its creation, reads the barcode, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Define a temporary file path for the generated barcode image.
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "largeBarcode.png");

        // --------------------------------------------------------------------
        // 2. Generate a barcode with a very large XDimension (>10 pixels).
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LARGE_XDIMENSION"))
        {
            // Disable automatic sizing to allow explicit dimension settings.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set XDimension to 12 points (approximately 12 pixels, which is >10).
            generator.Parameters.Barcode.XDimension.Point = 12f;

            // Set a reasonable bar height for visibility.
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Save the generated barcode image to the temporary file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 3. Verify that the barcode image file was successfully created.
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // 4. Recognize the barcode using Large XDimension mode.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            // Configure the reader to use Large XDimension detection.
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }

        // --------------------------------------------------------------------
        // 5. Clean up the temporary barcode image file.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Suppress any exceptions that occur during file deletion.
        }
    }
}