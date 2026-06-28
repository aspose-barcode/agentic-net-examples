using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a DataBar Omni-Directional barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it, and verifies the output file.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "databar.png");

        // Initialize a DataBar Omni-Directional barcode generator with sample GS1 code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, "(01)12345678901231"))
        {
            // Disable the optional 2D composite component for this barcode type.
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Enforce GS1 encoding rules for the DataBar barcode.
            generator.Parameters.Barcode.DataBar.AllowOnlyGS1Encoding = true;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Check whether the image file was successfully created.
        if (File.Exists(outputPath))
        {
            // Load the image to confirm it can be opened and retrieve its dimensions.
            using (var img = Image.FromFile(outputPath))
            {
                Console.WriteLine($"Barcode image saved successfully: {outputPath}");
                Console.WriteLine($"Image dimensions: {img.Width}x{img.Height}");
            }
        }
        else
        {
            // Inform the user that the barcode generation failed.
            Console.WriteLine("Failed to generate the barcode image.");
        }
    }
}