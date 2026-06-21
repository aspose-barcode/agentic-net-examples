using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, saves it to a memory stream,
    /// and performs two recognition attempts with different quality settings.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode with a specific XDimension (module size)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set module width to 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image to a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // -------------------------------------------------------------
                // 1. Read the barcode with default recognition settings
                // -------------------------------------------------------------
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Perform barcode detection
                    var results = reader.ReadBarCodes();
                    Console.WriteLine($"Default detection count: {results.Length}");
                    // Output each detected barcode's type and text
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected type: {result.CodeTypeName}, text: {result.CodeText}");
                    }
                }

                // Reset stream position for the second read
                ms.Position = 0;

                // -------------------------------------------------------------
                // 2. Read the same image with MinimalXDimension set higher than XDimension
                // -------------------------------------------------------------
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // MinimalXDimension is a recognition threshold; setting it higher than the
                    // actual XDimension (2) should cause the barcode to be ignored.
                    reader.QualitySettings.MinimalXDimension = 3f; // higher than generator's XDimension

                    // Perform barcode detection with modified quality settings
                    var results = reader.ReadBarCodes();
                    Console.WriteLine($"Detection with MinimalXDimension=3: {results.Length}");
                    // Output each detected barcode's type and text
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected type: {result.CodeTypeName}, text: {result.CodeText}");
                    }
                }
            }
        }
    }
}