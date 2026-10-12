// Title: Apply custom foreground color to a MaxiCode Mode 2 barcode
// Description: Demonstrates how to generate a MaxiCode Mode 2 barcode with a custom blue foreground using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on visual customization of barcodes. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCodeMode classes to set symbology, mode, and color properties. Developers often need to adjust barcode appearance for branding or readability, and this snippet illustrates the typical steps for such customizations.
// Prompt: Apply a custom foreground color to a MaxiCode Mode 2 barcode using the generator's ForeColor property.
// Tags: maxicode, foreground-color, png, barcodegenerator, aspose.barcode

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
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Control characters used in the MaxiCode data string
        string gs = "\u001d";   // Group Separator
        string rs = "\u001e";   // Record Separator
        string eot = "\u0004";  // End of Transmission

        // Build the MaxiCode data payload according to the specification
        string codetext = $"[)>{rs}01{gs}B1050{gs}056{gs}001{gs}ADDITIONAL DATA{eot}";

        // Determine a temporary file path for the output PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCodeMode2.png");

        // Initialize the barcode generator with MaxiCode symbology and the prepared data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codetext))
        {
            // Set the MaxiCode mode to Mode 2 (structured carrier message)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Apply a custom foreground (bar) color – blue in this case
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Define the size of each module (pixel) for the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}