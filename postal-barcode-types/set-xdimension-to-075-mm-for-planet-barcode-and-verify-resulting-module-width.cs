// Title: Set XDimension for Planet barcode and verify module width
// Description: Demonstrates how to set the XDimension (module width) of a Planet barcode to 0.75 mm using Aspose.BarCode and confirms the setting. The generated barcode image is saved to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode parameter customization. It shows how to work with the BarcodeGenerator class, modify barcode dimensions via the Parameters.Barcode.XDimension property, and save the result in PNG format. Developers often need to adjust module width for printing accuracy, scanning reliability, or meeting specific standards, making this pattern common in barcode creation workflows.
// Prompt: Set XDimension to 0.75 mm for a Planet barcode and verify resulting module width.
// Tags: planet barcode, xdimension, module width, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting the XDimension of a Planet barcode and saving the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Planet barcode with a custom XDimension and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full path for the output image file
        string imagePath = Path.Combine(outputDir, "PlanetBarcode.png");

        // Create a barcode generator for the Planet symbology with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set the XDimension (module width) to 0.75 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.75f;
            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
            // Output the set XDimension value for verification
            Console.WriteLine($"XDimension set to {generator.Parameters.Barcode.XDimension.Millimeters} mm");
            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {imagePath}");
        }
    }
}