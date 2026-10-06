// Title: Set XDimension for Planet barcode and verify module width
// Description: Demonstrates how to set the XDimension (module width) to 0.75 mm for a Planet barcode using Aspose.BarCode, then saves the image and outputs verification data.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating configuration of barcode parameters such as XDimension. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image saving with BarCodeImageFormat. Developers often need to adjust module size for printing accuracy, and this snippet provides a quick reference for setting and confirming XDimension values.
// Prompt: Set XDimension to 0.75 mm for a Planet barcode and verify resulting module width.
// Tags: planet, xdimension, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting XDimension for a Planet barcode and verifying the resulting module width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Planet barcode with a custom XDimension, saves it as PNG, and prints verification details.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string tempFolder = Path.Combine(Path.GetTempPath(), "PlanetXDim_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define full path for the generated PNG image
        string outputPath = Path.Combine(tempFolder, "planet.png");

        // Initialize the barcode generator for Planet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set the module width (XDimension) to 0.75 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.75f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Retrieve and display the XDimension that was set
            float setXDim = generator.Parameters.Barcode.XDimension.Millimeters;
            Console.WriteLine($"XDimension set to (mm): {setXDim}");

            // Load the saved image to verify its pixel dimensions
            using (var bitmap = new Bitmap(outputPath))
            {
                Console.WriteLine($"Generated image width (pixels): {bitmap.Width}");
            }
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}