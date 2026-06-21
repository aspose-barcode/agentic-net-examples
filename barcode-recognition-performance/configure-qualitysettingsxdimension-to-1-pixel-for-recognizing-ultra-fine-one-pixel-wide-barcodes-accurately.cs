using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a minimal XDimension,
/// saving it to a memory stream, and then recognizing it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a stream, and reads it back.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the XDimension to 1 point (approximately 1 pixel at 72 DPI)
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Disable auto-sizing so the XDimension and BarHeight values are respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the barcode bar height to 30 points
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the generated barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader to decode Code128 from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Configure the reader to handle ultra‑fine XDimension settings
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = 1f; // Expect 1‑pixel wide bars

                    // Iterate through all detected barcodes and output their details
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}