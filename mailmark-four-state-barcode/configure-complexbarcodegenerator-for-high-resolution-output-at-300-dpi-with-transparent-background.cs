// Title: Generate a high‑resolution transparent MaxiCode complex barcode
// Description: Demonstrates creating a MaxiCode complex barcode with 300 DPI resolution and a transparent background, then saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator along with MaxiCodeCodetextMode3 and related message classes to produce high‑quality barcodes. Developers often need to customize resolution, colors, and output formats when integrating barcodes into graphics‑intensive applications.
// Prompt: Configure ComplexBarcodeGenerator for high‑resolution output at 300 DPI with transparent background.
// Tags: maxicode, complexbarcode, highresolution, transparentbackground, png, complexbarcodegenerator, maxicodecodetextmode3, maxicodestandardsecondmessage

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode complex barcode with high resolution and transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "complex_barcode.png");

        // Prepare the secondary message part of the MaxiCode payload
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample"
        };

        // Build the main MaxiCode codetext with required fields
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Initialize the complex barcode generator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Configure high resolution (300 DPI) for crisp output
            generator.Parameters.Resolution = 300f;

            // Set the background to transparent so the barcode can be overlaid on any surface
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Complex barcode saved to: {outputPath}");
    }
}