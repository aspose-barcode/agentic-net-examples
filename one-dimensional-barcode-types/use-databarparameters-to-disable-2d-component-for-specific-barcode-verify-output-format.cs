// Title: Disable 2D Component in DataBar Expanded Barcode and Verify PNG Output
// Description: Demonstrates how to generate a DataBar Expanded barcode with the 2‑dimensional composite component turned off, then checks that the resulting file is a PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar symbologies and barcode parameter customization. It showcases the use of BarcodeGenerator, EncodeTypes, and DataBarParameters to modify barcode features such as disabling the 2D composite component. Developers often need to tailor barcode appearance for specific standards or scanning requirements, and this snippet illustrates typical steps: configure parameters, generate the image, and validate the output format.
// Prompt: Use DataBarParameters to disable 2D component for specific barcode, verify output format.
// Tags: databar, disable 2d component, png output, barcode generation, aspose.barcode, encode types, databarp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataBar Expanded barcode with the 2D composite component disabled,
/// saves it as a PNG file, and verifies the output format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, generates the barcode,
    /// saves it, and checks that the file exists and has a PNG extension.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string filePath = Path.Combine(outputDir, "Databar2DComponentDisabled.png");

        // Generate DataBar Expanded barcode with the 2D component disabled
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpanded, "(01)12345678901231"))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Disable the 2D composite component of the DataBar barcode
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and has a PNG extension
        bool exists = File.Exists(filePath);
        bool isPng = exists && string.Equals(Path.GetExtension(filePath), ".png", StringComparison.OrdinalIgnoreCase);

        // Output verification results
        Console.WriteLine($"Barcode file created: {exists}");
        Console.WriteLine($"File extension is PNG: {isPng}");
    }
}