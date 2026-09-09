// Title: Generate MaxiCode barcode with custom margins
// Description: Demonstrates creating a MaxiCode barcode image with a 10‑pixel margin on all sides and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure barcode parameters such as padding, module size, and image format using the BarcodeGenerator class. Typical use cases include generating shipping labels, inventory tags, or any application requiring MaxiCode symbology. Developers often need to adjust margins and dimensions to fit layout constraints or improve visual separation in printed or digital media.
// Prompt: Generate a MaxiCode barcode with a custom margin of 10 pixels on all sides for better visual separation.
// Tags: maxicode, barcode generation, margin, png, aspose.barcode, encode types, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with custom margins and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies a 10‑pixel padding on all sides, sets the module size, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary file path for the output PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCode_" + Guid.NewGuid().ToString("N") + ".png");

        // Initialize the barcode generator for MaxiCode symbology with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample"))
        {
            // Apply a custom margin of 10 pixels on each side
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Optional: define the module (dot) size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}