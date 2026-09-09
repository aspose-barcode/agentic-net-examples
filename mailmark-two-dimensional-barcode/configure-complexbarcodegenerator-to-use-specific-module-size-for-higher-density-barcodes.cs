// Title: Generate a MaxiCode barcode with custom module size for higher density
// Description: Demonstrates configuring a ComplexBarcodeGenerator to set a specific X-dimension (module size) and resolution, producing a high‑density MaxiCode barcode saved as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to use ComplexBarcodeGenerator with MaxiCodeCodetextMode3. It shows key API classes such as ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and MaxiCodeStandardSecondMessage, typical for creating custom‑styled barcodes, adjusting module size, resolution, and colors. Developers often need these techniques when generating dense barcodes for logistics or tracking applications.
// Prompt: Configure ComplexBarcodeGenerator to use a specific module size for higher density barcodes.
// Tags: barcode, complexbarcode, maxicode, module size, resolution, png, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a custom module size and resolution using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary output folder, configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine a unique temporary directory for output
        string outputDir = Path.Combine(Path.GetTempPath(), "ComplexBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "maxicode.png");

        // Prepare the secondary message for MaxiCode
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Higher density barcode"
        };

        // Set up MaxiCode codetext with postal code, country, service category, and secondary message
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Generate the barcode with custom module size and resolution
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Increase module size (X-dimension) for higher density
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Increase image resolution
            generator.Parameters.Resolution = 300f;

            // Set barcode color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine("Barcode generated at: " + outputPath);
    }
}