// Title: Demonstrate AutoSizeMode.Nearest barcode dimension verification
// Description: Generates barcodes with specified point dimensions and checks that the resulting image does not exceed those dimensions.
// Prompt: Write unit tests that compare expected and actual image dimensions after applying AutoSizeMode.Nearest with given parameters.
// Tags: barcode, autosizemode, nearest, dimensions, unit-test, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the entry point and test runner for barcode dimension verification using AutoSizeMode.Nearest.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point that runs sample barcode dimension tests.
    /// </summary>
    static void Main()
    {
        // Test case 1: ImageWidth = 200pt, ImageHeight = 100pt, AutoSizeMode = Nearest
        RunTest(EncodeTypes.Code128, "Test123", 200f, 100f);

        // Test case 2: ImageWidth = 150pt, ImageHeight = 150pt, AutoSizeMode = Nearest
        RunTest(EncodeTypes.QR, "https://example.com", 150f, 150f);
    }

    /// <summary>
    /// Generates a barcode with the specified parameters, then validates that the actual image dimensions
    /// do not exceed the requested point dimensions when AutoSizeMode.Nearest is applied.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="widthPt">Desired maximum image width in points.</param>
    /// <param name="heightPt">Desired maximum image height in points.</param>
    static void RunTest(BaseEncodeType encodeType, string codeText, float widthPt, float heightPt)
    {
        // Initialize the barcode generator with the chosen symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure AutoSizeMode to Nearest and set target dimensions in points.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Point = widthPt;
            generator.Parameters.ImageHeight.Point = heightPt;

            // Generate the barcode image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Actual dimensions of the generated bitmap (in pixels).
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Verify that the actual dimensions do not exceed the requested dimensions.
                // AutoSizeMode.Nearest may reduce the size to the nearest lower possible value.
                bool widthOk = actualWidth <= widthPt;
                bool heightOk = actualHeight <= heightPt;

                // Output the test results to the console.
                Console.WriteLine($"Test for {encodeType.TypeName} with CodeText \"{codeText}\":");
                Console.WriteLine($"  Expected max width: {widthPt}pt, actual width: {actualWidth}px");
                Console.WriteLine($"  Expected max height: {heightPt}pt, actual height: {actualHeight}px");
                Console.WriteLine($"  Width check: {(widthOk ? "PASS" : "FAIL")}");
                Console.WriteLine($"  Height check: {(heightOk ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }
        }
    }
}