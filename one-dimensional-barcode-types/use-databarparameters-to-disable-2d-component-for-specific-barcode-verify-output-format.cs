// Title: Generate DataBar Expanded Barcode with 2D Component Disabled
// Description: Demonstrates how to generate a DataBar Expanded barcode while disabling its 2D composite component and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataBarParameters configuration. It shows how to control the 2D composite component of DataBar barcodes, a common requirement when a pure linear representation is needed. Developers working with retail, logistics, or inventory systems often need to customize DataBar symbology for specific scanning environments, using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Use DataBarParameters to disable 2D component for specific barcode, verify output format.
// Tags: databar, barcode, generation, disable-2d-component, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataBar Expanded barcode with the 2D composite component disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates the barcode, saves it as PNG,
    /// and verifies the output file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo output
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataBarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated PNG image
        string disabledPath = Path.Combine(tempFolder, "Databar2DComponentDisabled.png");

        // Generate a DataBar Expanded barcode with the 2D component disabled
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.DatabarExpanded, "(01)12345678901231"))
        {
            // Set the X-dimension (module width) in pixels
            gen.Parameters.Barcode.XDimension.Pixels = 2f;

            // Disable the 2D composite component of the DataBar barcode
            gen.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the barcode image as PNG
            gen.Save(disabledPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and has the expected PNG extension
        if (File.Exists(disabledPath) && Path.GetExtension(disabledPath).Equals(".png", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Barcode generated and saved successfully:");
            Console.WriteLine(disabledPath);
        }
        else
        {
            Console.WriteLine("Failed to generate the barcode or incorrect output format.");
        }
    }
}