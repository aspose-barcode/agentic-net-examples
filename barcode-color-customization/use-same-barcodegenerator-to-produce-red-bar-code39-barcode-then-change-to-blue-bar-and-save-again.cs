// Title: Generate Code39 barcodes with red and blue bars using Aspose.BarCode
// Description: Demonstrates how to create a Code39 barcode, set its bar color to red, save it, then change the color to blue and save again.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include customizing barcode appearance for branding or visual distinction. Developers often need to modify barcode colors and export images in various formats, and this snippet illustrates those common operations.
// Prompt: Use the same BarcodeGenerator to produce a red‑bar Code39 barcode, then change to blue‑bar and save again.
// Tags: code39, barcode generation, color customization, png output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code39 barcodes with different bar colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates a red barcode, changes to blue, and saves both images.
    /// </summary>
    static void Main()
    {
        // Determine the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Define the text to encode in the barcode
        string codeText = "123ABC";

        // Initialize the barcode generator with Code39 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Set the bar color to red and save the image
            generator.Parameters.Barcode.BarColor = Color.Red;
            string redPath = Path.Combine(outputDir, "Code39_Red.png");
            generator.Save(redPath, BarCodeImageFormat.Png);

            // Change the bar color to blue and save the second image
            generator.Parameters.Barcode.BarColor = Color.Blue;
            string bluePath = Path.Combine(outputDir, "Code39_Blue.png");
            generator.Save(bluePath, BarCodeImageFormat.Png);
        }

        // Output a simple confirmation message
        Console.WriteLine("Barcodes generated.");
    }
}