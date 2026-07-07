// Title: Generate a dense MaxiCode barcode with custom module size
// Description: Demonstrates how to set the ModuleSize (XDimension) to 2 points for a denser MaxiCode, suitable for compact label printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and XDimension properties to control barcode density. Developers creating packaging, shipping labels, or inventory tags often need to adjust module size for space‑constrained layouts.
// Prompt: Set the generator's ModuleSize property to 2 to produce a denser MaxiCode image for compact labels.
// Tags: maxicode, module size, barcode generation, png, aspose.barcode, encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a custom module size for denser output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode barcode, sets XDimension to 2 points, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Sample codetext for MaxiCode
        const string codeText = "Sample MaxiCode";

        // Output file path
        string outputPath = "maxicode.png";

        // Create a MaxiCode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set module size (XDimension) to 2 points for a denser image
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}