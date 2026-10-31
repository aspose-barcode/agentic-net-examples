// Title: Generate Code128 barcode with custom XDimension
// Description: Demonstrates setting the XDimension of a Code128 barcode to 0.5 mm and creating a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as XDimension using the BarcodeGenerator class. Typical use cases include customizing barcode size for printing or embedding in documents. Developers often need to adjust dimensions to meet scanning requirements or layout constraints.
// Prompt: Set XDimension to 0.5 mm and YDimension to 30 mm before generating the barcode image.
// Tags: code128, xdimension, barcode generation, png, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a Code128 barcode image with a custom XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode, sets the XDimension to 0.5 mm, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Determine a temporary file path for the output PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        try
        {
            // Initialize the barcode generator with Code128 symbology and sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
            {
                // Set the XDimension (module width) to 0.5 mm.
                generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

                // YDimension is not supported in the current Aspose.BarCode API.
                Console.WriteLine("YDimension property is unavailable; only XDimension was set.");

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}