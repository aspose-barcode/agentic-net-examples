// Title: Generate DataBar Stacked barcode with custom aspect ratio and 2D component, save as PNG
// Description: Demonstrates how to create a DataBar Stacked barcode, set its aspect ratio to 12, enable the 2‑D composite component, and save the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DatabarStacked. It shows how to configure barcode parameters such as X‑dimension, aspect ratio, and 2D component flag—common tasks when customizing linear barcodes for retail or logistics applications. Developers often need to adjust these settings to meet specification requirements and then export the barcode to image formats like PNG or PDF.
// Prompt: Set DataBar stacked parameters aspect ratio twelve, enable 2D component, generate PDF output.
// Tags: databar, stacked, aspectratio, 2dcomponent, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataBar Stacked barcode with a custom aspect ratio,
/// enables the 2‑D composite component, and saves the barcode image to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the generated barcode image.
        string imagePath = Path.Combine(outputDir, "DataBarStacked.png");

        // Sample data to encode; includes an Application Identifier (01) for GTIN.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with the DataBar Stacked symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
        {
            // Set the module (X‑dimension) size in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure the aspect ratio of the DataBar barcode to 12.
            generator.Parameters.Barcode.DataBar.AspectRatio = 12f;

            // Enable the 2‑D composite component flag for the DataBar barcode.
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = true;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image generated at: {imagePath}");
    }
}