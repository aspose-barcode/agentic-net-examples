// Title: Custom Foreground Color for MaxiCode Mode 2 Barcode
// Description: Demonstrates how to generate a MaxiCode Mode 2 barcode with a custom foreground (bar) color using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCodeMode classes to create a MaxiCode barcode, adjust visual properties such as bar color and module size, and save the result as an image. Developers working with 2D barcodes often need to customize appearance for branding or readability, making this pattern a common starting point.
// Prompt: Apply a custom foreground color to a MaxiCode Mode 2 barcode using the generator's ForeColor property.
// Tags: maxicode, foreground color, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with a custom foreground color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode2.png");

        // Define control characters used in the MaxiCode data format
        string gs = "\u001d";   // Group Separator
        string rs = "\u001e";   // Record Separator
        string eot = "\u0004";  // End of Transmission

        // Build the codetext according to MaxiCode Mode 2 specifications
        string codetext = $"[)>{rs}01{gs}B1050{gs}056{gs}001{gs}CUSTOM FOREGROUND{eot}";

        // Create a barcode generator for MaxiCode with the prepared codetext
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.MaxiCode, codetext))
        {
            // Set the MaxiCode mode to Mode 2 (structured carrier message)
            gen.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Apply a custom foreground (bar) color – red in this case
            gen.Parameters.Barcode.BarColor = Color.Red;

            // Optionally increase the module (pixel) size for better visibility
            gen.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated barcode as a PNG image
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}