using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and reading of a low‑contrast Code128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a low‑contrast barcode, saves it to a memory stream,
    /// and reads it back with different deconvolution settings.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        string codeText = "LowContrastTest";

        // Create a barcode generator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure bar and background colors to be similar gray tones (low contrast)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(100, 100, 100); // dark gray
            generator.Parameters.BackColor = Color.FromArgb(200, 200, 200);      // light gray

            // Save the generated barcode image into a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Read the barcode with deconvolution enabled (slow mode – more aggressive restoration)
                Console.WriteLine("=== Reading with Deconvolution = Slow (more aggressive) ===");
                ReadAndReport(ms, DeconvolutionMode.Slow);

                // Reset stream position for the second read operation
                ms.Position = 0;

                // Read the barcode with deconvolution disabled (fast mode – minimal restoration)
                Console.WriteLine("=== Reading with Deconvolution = Fast (minimal) ===");
                ReadAndReport(ms, DeconvolutionMode.Fast);
            }
        }
    }

    // Reads a barcode image from a stream using the specified deconvolution mode and reports results.
    static void ReadAndReport(Stream imageStream, DeconvolutionMode deconvMode)
    {
        // Initialize a barcode reader for Code128 format
        using (var reader = new BarCodeReader(imageStream, DecodeType.Code128))
        {
            // Use high‑quality settings suitable for low‑contrast images
            reader.QualitySettings = QualitySettings.HighQuality;
            reader.QualitySettings.Deconvolution = deconvMode;          // Set deconvolution mode
            reader.QualitySettings.AllowIncorrectBarcodes = true;      // Permit reading despite minor errors

            // Iterate through all detected barcodes and output their details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine($"Detected Angle: {result.Region.Angle}");
                Console.WriteLine();
            }
        }
    }
}