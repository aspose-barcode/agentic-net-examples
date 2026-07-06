// Title: Toggle CodeText Visibility and Verify Image Height Change
// Description: Demonstrates how to hide and show the human‑readable text of a Code128 barcode and checks that the image height changes accordingly.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode appearance customization. It uses BarcodeGenerator, CodeTextParameters, and CodeLocation to control the visibility of the main codetext. Developers often need to toggle human‑readable text for different output requirements, such as compact images or printable labels. The snippet shows typical steps for setting parameters, generating images, and performing a simple validation.
// Prompt: Write unit tests that verify toggling CodetextParameters.Visible correctly shows and hides the main barcode text.
// Tags: code128, codetextvisibility, bitmap, aspose.barcode, generation, unit-test, debug.assert

using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that toggles the visibility of the barcode's main codetext
/// and verifies the resulting image height changes as expected.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images—one with
    /// visible text and one without—and asserts that the image without text
    /// is shorter, confirming the visibility toggle works.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with sample codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set the default location of the human‑readable text to appear below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Generate the first image where the text is visible.
            using (Bitmap imageWithText = generator.GenerateBarCodeImage())
            {
                int heightWithText = imageWithText.Height; // Record height with text.

                // Hide the human‑readable text by moving it to the 'None' location.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Generate the second image where the text is hidden.
                using (Bitmap imageWithoutText = generator.GenerateBarCodeImage())
                {
                    int heightWithoutText = imageWithoutText.Height; // Record height without text.

                    // Determine whether the image without text is shorter.
                    bool isHeightReduced = heightWithoutText < heightWithText;

                    // Assert the height reduction and output the result.
                    Debug.Assert(isHeightReduced, "Image height should be reduced when CodeText is hidden.");
                    Console.WriteLine($"Height with text: {heightWithText}");
                    Console.WriteLine($"Height without text: {heightWithoutText}");
                    Console.WriteLine(isHeightReduced
                        ? "Test passed: Text visibility toggling correctly changes image size."
                        : "Test failed: Image size did not change as expected.");
                }
            }
        }
    }
}