using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, then reading it with an
/// intentionally low minimal XDimension setting to observe false positives.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates a barcode, reads it with a low MinimalXDimension, and reports any false positives.
    /// </summary>
    static void Main()
    {
        const string expectedCode = "1234567890"; // The barcode value we expect to read
        int falsePositives = 0; // Counter for mismatched read results

        // Generate the barcode image in a memory stream (no file I/O)
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the expected text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, expectedCode))
            {
                // Set a reasonable XDimension (module width) for generation
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading
            ms.Position = 0;

            // Initialize a barcode reader for Code128 using the same memory stream
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                // Enable the use of MinimalXDimension mode during recognition
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // Set an unrealistically low minimal XDimension (e.g., 0.5 points) to provoke false positives
                reader.QualitySettings.MinimalXDimension = 0.5f;

                // Iterate over all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Compare the read text with the expected value; count mismatches
                    if (!string.Equals(result.CodeText, expectedCode, StringComparison.Ordinal))
                    {
                        falsePositives++;
                    }
                }
            }
        }

        // Output the number of false positives observed with the low MinimalXDimension setting
        Console.WriteLine($"False positives with too low MinimalXDimension: {falsePositives}");
    }
}