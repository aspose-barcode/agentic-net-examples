using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a stacked DataBar barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a stacked DataBar barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "databar_stacked.png");

        // Initialize a BarcodeGenerator for the DatabarStacked symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked))
        {
            // Set the GTIN code text required for DataBar symbologies.
            generator.CodeText = "(01)12345678901231";

            // Configure DataBar-specific parameters.
            // Set the aspect ratio (height divided by width) to 10.
            generator.Parameters.Barcode.DataBar.AspectRatio = 10f;

            // Disable the optional 2D composite component (not needed for this example).
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Stacked DataBar barcode saved to: {outputPath}");
    }
}