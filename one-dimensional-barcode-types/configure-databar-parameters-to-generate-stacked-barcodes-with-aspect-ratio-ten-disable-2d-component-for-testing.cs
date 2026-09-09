// Title: Generate Stacked DataBar Omni-Directional Barcode with Custom Aspect Ratio
// Description: Demonstrates how to configure Aspose.BarCode to create a stacked DataBar Omni‑Directional barcode, set the aspect ratio to ten, and disable the 2D composite component.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DatabarStackedOmniDirectional. It shows how to adjust DataBar parameters such as XDimension, AspectRatio, and the Is2DCompositeComponent flag. Developers working with retail or GS1 barcodes often need to customize these settings to meet scanning requirements and layout constraints.
// Prompt: Configure DataBar parameters to generate stacked barcodes with aspect ratio ten, disable 2D component for testing.
// Tags: databar, stacked, aspectratio, disable-2d-component, barcode-generation, aspnet, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a stacked DataBar Omni‑Directional barcode,
/// customizes its visual parameters, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the generated image.
        string tempDir = Path.Combine(Path.GetTempPath(), "DataBarStackedExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full file path for the PNG output.
        string outputPath = Path.Combine(tempDir, "DatabarStackedOmniDirectional.png");

        // Initialize the barcode generator with the stacked Omni‑Directional DataBar symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStackedOmniDirectional, "(01)12345678901231"))
        {
            // Set the X‑dimension (module width) to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure the DataBar aspect ratio to 10 (wide bars relative to height).
            generator.Parameters.Barcode.DataBar.AspectRatio = 10f;

            // Disable the optional 2D composite component for testing purposes.
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}