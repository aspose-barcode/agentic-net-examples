// Title: Generate MaxiCode barcode with transparent background
// Description: Demonstrates how to create a MaxiCode barcode using Aspose.BarCode.ComplexBarcode with a transparent background, suitable for overlaying on UI components.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode2 and related message classes to produce high‑density 2‑D barcodes. Typical scenarios include shipping labels, parcel tracking, and UI overlays where a transparent background is required. Developers often need to configure barcode parameters such as colors, size, and encoding options, and then save the result in common image formats.
// Prompt: Configure ComplexBarcodeGenerator to output a barcode image with transparent background for UI component overlay.
// Tags: maxicode, transparent background, complex barcode, barcode generation, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds a MaxiCode codetext, configures the generator for a transparent background,
    /// and saves the barcode as a PNG image.
    /// </summary>
    static void Main()
    {
        // Prepare complex codetext for MaxiCode mode 2 (includes a structured second message)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build the structured second message (address lines, state, and year)
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;

        // Attach the second message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Create the barcode generator and set the background to transparent
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Parameters.BackColor = Color.Transparent;

            // Define the output file path (PNG format preserves transparency)
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeTransparent.png");

            // Save the generated barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}