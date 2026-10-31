// Title: Generate Stacked DataBar Barcode with Custom Aspect Ratio
// Description: Demonstrates how to configure Aspose.BarCode to create a stacked DataBar barcode with an aspect ratio of ten and disable the 2D composite component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar symbology configuration. It shows usage of the BarcodeGenerator class, EncodeTypes enumeration, and DataBar parameters such as AspectRatio and Is2DCompositeComponent. Developers often need to customize DataBar settings for retail or inventory applications, adjusting visual dimensions and composite features to meet scanning requirements.
// Prompt: Configure DataBar parameters to generate stacked barcodes with aspect ratio ten, disable 2D component for testing.
// Tags: databar, stacked, aspectratio, disable-2d-component, barcode-generation, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a stacked DataBar barcode with a custom aspect ratio and disabled 2D component using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory inside the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full file path for the resulting PNG image
        string filePath = Path.Combine(outputDir, "DatabarStackedAspectRatio10.png");

        // Initialize the barcode generator with stacked DataBar symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
        {
            // Set the DataBar aspect ratio to 10
            generator.Parameters.Barcode.DataBar.AspectRatio = 10;
            // Disable the 2D composite component for testing purposes
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;
            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}