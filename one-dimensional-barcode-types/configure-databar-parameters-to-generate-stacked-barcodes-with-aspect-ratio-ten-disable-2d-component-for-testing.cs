// Title: Generate Stacked DataBar Barcode with Custom Aspect Ratio
// Description: Demonstrates how to create a stacked DataBar Omni‑Directional barcode, set its aspect ratio to ten, and disable the 2D composite component.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and DataBar parameters. Developers often need to customize DataBar symbologies—such as adjusting aspect ratios, stacking options, or toggling 2D components—to meet specific printing or scanning requirements. The snippet illustrates typical steps: initializing the generator, configuring barcode properties, and saving the output image.
// Prompt: Configure DataBar parameters to generate stacked barcodes with aspect ratio ten, disable 2D component for testing.
// Tags: databar, stacked, aspectratio, disable-2d-component, barcode-generation, aspnet, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a stacked DataBar Omni‑Directional barcode
/// with a custom aspect ratio and without the 2D composite component.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory,
    /// configures the barcode generator, and saves the resulting PNG image.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "DatabarStackedAspectRatio10.png");

        // Initialize the barcode generator with the stacked Omni‑Directional DataBar symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStackedOmniDirectional, "(01)12345678901231"))
        {
            // Optional: set the module (X) size in pixels for finer control over image resolution
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Set the aspect ratio to 10, which stretches the stacked barcode vertically
            generator.Parameters.Barcode.DataBar.AspectRatio = 10;

            // Disable the 2D composite component (useful for testing or when only the linear part is required)
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the configured barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}