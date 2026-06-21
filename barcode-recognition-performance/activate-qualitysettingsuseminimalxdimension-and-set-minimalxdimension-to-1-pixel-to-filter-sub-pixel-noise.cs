using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it in memory,
/// and then reading it back using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a memory stream, and reads it back.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and store it in a memory stream
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode image as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning before reading
            ms.Position = 0;

            // Load the barcode image from the memory stream into a bitmap
            using (var bitmap = new Bitmap(ms))
            {
                // Initialize a barcode reader that can decode all supported types
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Enable minimal XDimension mode and set the minimal XDimension threshold to 1 pixel
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = 1f;

                    // Read all barcodes from the image and output their type and text
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}