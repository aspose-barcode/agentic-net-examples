// Title: Demonstrate toggling barcode codetext visibility and its effect on image height
// Description: This example generates a Code128 barcode with and without the human‑readable text, then compares the resulting image heights to show how hiding the codetext reduces the barcode size.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, illustrating how to control the visibility of the main barcode text using the CodeTextParameters.Location property. Developers working with barcode rendering often need to show or hide the codetext for layout or branding reasons; the example uses BarcodeGenerator, EncodeTypes, and the CodeLocation enum to toggle visibility and observe its impact on the generated image dimensions.
// Prompt: Write unit tests that verify toggling CodetextParameters.Visible correctly shows and hides the main barcode text.
// Tags: barcode, code128, codetextvisibility, imageheight, aspose.barcode, generation, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains a simple demonstration that toggles barcode codetext visibility
/// and checks the resulting image height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the visibility toggle test and
    /// reports any unexpected errors.
    /// </summary>
    static void Main()
    {
        try
        {
            // Run the test that compares image heights with and without codetext.
            RunVisibilityToggleTest();
        }
        catch (Exception ex)
        {
            // Output any unexpected exception details.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates barcode images with codetext shown and hidden, then compares their heights.
    /// </summary>
    static void RunVisibilityToggleTest()
    {
        const string codeText = "Test123";

        // Get image height when codetext is visible.
        int heightWithText = GetBarcodeImageHeight(codeText, showText: true);
        // Get image height when codetext is hidden.
        int heightWithoutText = GetBarcodeImageHeight(codeText, showText: false);

        Console.WriteLine($"Height with text: {heightWithText}");
        Console.WriteLine($"Height without text: {heightWithoutText}");

        // Verify that hiding the codetext reduces the image height.
        if (heightWithoutText < heightWithText)
        {
            Console.WriteLine("Test passed: hiding codetext reduces image height.");
        }
        else
        {
            Console.WriteLine("Test failed: image height did not decrease when codetext was hidden.");
        }
    }

    /// <summary>
    /// Generates a barcode image for the specified code and returns its height.
    /// </summary>
    /// <param name="code">The barcode text to encode.</param>
    /// <param name="showText">If true, the human‑readable text is displayed below the barcode; otherwise it is hidden.</param>
    /// <returns>The height of the generated bitmap in pixels.</returns>
    static int GetBarcodeImageHeight(string code, bool showText)
    {
        // Create a generator for Code128 with the provided code.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code))
        {
            // Configure codetext visibility based on the showText flag.
            if (showText)
            {
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            }
            else
            {
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            }

            // Generate the barcode image and return its height.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return bitmap.Height;
            }
        }
    }
}