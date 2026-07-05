// Title: Verify MinimalXDimension default behavior
// Description: Demonstrates checking that MinimalXDimension is zero when UseMinimalXDimension is disabled in barcode quality settings.
// Prompt: Create unit tests ensuring MinimalXDimension defaults to zero when UseMinimalXDimension is false.
// Tags: barcode, symbology, code128, qualitysettings, minimalxdimension, unit-test, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, reads it back,
/// and verifies that MinimalXDimension defaults to zero when the
/// XDimension mode is not set to UseMinimalXDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it,
    /// and checks the MinimalXDimension default value.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and keep it in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Use default settings (AutoSizeMode.None, default XDimension).
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Create a reader for the generated image.
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Ensure the XDimension mode is NOT UseMinimalXDimension.
                    if (reader.QualitySettings.XDimension == XDimensionMode.UseMinimalXDimension)
                    {
                        Console.WriteLine("FAILED: XDimension mode is unexpectedly UseMinimalXDimension.");
                        return;
                    }

                    // Verify that MinimalXDimension defaults to zero.
                    float minimal = reader.QualitySettings.MinimalXDimension;
                    if (Math.Abs(minimal) < 0.0001f)
                    {
                        Console.WriteLine("PASSED: MinimalXDimension defaults to zero when UseMinimalXDimension is false.");
                    }
                    else
                    {
                        Console.WriteLine($"FAILED: MinimalXDimension is {minimal}, expected 0.");
                    }
                }
            }
        }
    }
}