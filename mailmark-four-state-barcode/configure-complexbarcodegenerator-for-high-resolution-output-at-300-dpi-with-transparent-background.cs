// Title: Generate MaxiCode barcode with high‑resolution transparent PNG
// Description: Demonstrates creating a MaxiCode barcode using Aspose.BarCode's ComplexBarcodeGenerator, setting 300 DPI resolution and a transparent background, and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode3 and related message classes to produce advanced 2‑D barcodes. Typical scenarios include logistics, parcel tracking, and inventory systems where MaxiCode is required. Developers often need to control image resolution, background transparency, and output format, which this sample illustrates.
// Prompt: Configure ComplexBarcodeGenerator for high‑resolution output at 300 DPI with transparent background.
// Tags: maxicode, complex barcode, high resolution, transparent background, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode with high resolution and a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MaxiCode barcode, configures image settings, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "ComplexBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "maxicode.png");

        // Create the second message part required for MaxiCode mode 3
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample Message"
        };

        // Build the MaxiCode codetext with required fields (postal code, country, service category, and second message)
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Generate the barcode, set high‑resolution (300 DPI) and transparent background, then save as PNG
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Parameters.Resolution = 300f; // 300 DPI for high‑quality output
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent; // Transparent background
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}