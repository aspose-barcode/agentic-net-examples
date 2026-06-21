using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a memory stream,
/// and then reading it back to verify the MinimalXDimension setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, and checks the MinimalXDimension value.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Prepare a memory stream to hold the generated barcode image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Initialize a barcode reader to recognize all supported types from the stream
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Set XDimension mode to Normal (disables UseMinimalXDimension)
                    reader.QualitySettings.XDimension = XDimensionMode.Normal;

                    // Retrieve the MinimalXDimension value (should be zero in Normal mode)
                    float minimalX = reader.QualitySettings.MinimalXDimension;
                    // Determine if the retrieved value is effectively zero
                    bool testPassed = Math.Abs(minimalX) < 0.0001f;

                    // Output the test result to the console
                    Console.WriteLine(testPassed
                        ? "PASS: MinimalXDimension is zero when UseMinimalXDimension is false."
                        : $"FAIL: MinimalXDimension is {minimalX}, expected zero.");
                }
            }
        }
    }
}