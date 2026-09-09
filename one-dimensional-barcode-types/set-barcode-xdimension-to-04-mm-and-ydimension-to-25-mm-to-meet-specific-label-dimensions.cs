// Title: Set XDimension and YDimension for a Code128 barcode
// Description: Demonstrates how to configure the XDimension (module width) to 0.4 mm and the image height (YDimension) to 25 mm for a Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode dimensions with the BarcodeGenerator class. It covers setting module width via XDimension, adjusting image height through AutoSizeMode and ImageHeight, and saving the result in PNG format. Developers creating labels, packaging, or inventory tags often need precise size control to meet printing specifications.
// Prompt: Set barcode XDimension to 0.4 mm and YDimension to 25 mm to meet specific label dimensions.
// Tags: barcode, xdimension, ydimension, code128, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with custom XDimension and YDimension settings,
/// then saves it as a PNG image to a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode dimensions, colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Define the barcode content and symbology
        string codeText = "Sample123";

        // Initialize the barcode generator with Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the module width (XDimension) to 0.4 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.4f;

            // Configure image height (YDimension) to 25 mm using interpolation auto-size mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageHeight.Millimeters = 25f;

            // Optional: define foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated barcode image
        Console.WriteLine("Barcode generated at: " + outputPath);
    }
}