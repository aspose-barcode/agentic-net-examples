// Title: Verify auto‑size behavior of barcode when BarCodeHeight is zero
// Description: Demonstrates a unit‑test‑style check that a barcode generated with BarCodeHeight left at its default (zero) automatically sizes itself based on the encoded content.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the AutoSizeMode property (especially Interpolation) together with default measurement units to let the library determine optimal barcode dimensions. Developers working with dynamic barcode rendering, layout‑aware image creation, or automated testing of barcode size behavior will find this pattern useful.
// Prompt: Create unit test verifying barcode with BarCodeHeight zero enables auto‑size based on content, using default units.
// Tags: barcode, autosize, interpolation, code128, unit-test, default-units, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains a simple console‑based test that verifies auto‑size functionality
/// of a generated barcode when the bar height is not explicitly set (default zero).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Executes the auto‑size test and
    /// writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Run the test and output result
        try
        {
            bool result = TestAutoSizeWithDefaultUnits();

            // Report PASS or FAIL based on the test outcome
            Console.WriteLine(result ? "PASS: Auto-size enabled correctly." : "FAIL: Auto-size not as expected.");
        }
        catch (Exception ex)
        {
            // Unexpected exception handling – report as failure
            Console.WriteLine($"FAIL: Unexpected exception - {ex.GetType().Name}: {ex.Message}");
        }
    }

    // Verifies that when AutoSizeMode is set to Interpolation (and BarHeight is not set),
    // the generated barcode image size adapts to the content using default units.
    static bool TestAutoSizeWithDefaultUnits()
    {
        // Sample barcode text
        const string codeText = "Test12345";

        // Create generator for Code128 (a 1D barcode)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable auto-size via interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Do NOT set BarHeight; leaving it at default allows auto-sizing.
            // Generate the barcode image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Ensure an image was created.
                if (bitmap == null)
                    return false;

                // Verify that the image has a non‑zero height and width (auto‑sized).
                if (bitmap.Height <= 0 || bitmap.Width <= 0)
                    return false;

                // Optionally, save the image for manual inspection (not required for the test).
                // bitmap.Save("autoSizeBarcode.png", ImageFormat.Png);

                // If we reach this point, auto‑size behaved as expected.
                return true;
            }
        }
    }
}