using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with custom colors and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Generates a barcode image, saves it, and attempts to read it back.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "custom_color_barcode.png";

        // ------------------------------------------------------------
        // Generate a barcode with custom colors (dark bars on light background)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "DarkOnLight"))
        {
            // Set the color of the dark bars.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color (light area).
            generator.Parameters.BackColor = Color.White;

            // Increase resolution to improve detection accuracy.
            generator.Parameters.Resolution = 300f;

            // Add padding to avoid clipping of the barcode at the image edges.
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image file was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the generated barcode using a custom recognition configuration.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            // Optimize detection for dark bars on a light background.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

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
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Confidence: {result.Confidence}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }
    }
}