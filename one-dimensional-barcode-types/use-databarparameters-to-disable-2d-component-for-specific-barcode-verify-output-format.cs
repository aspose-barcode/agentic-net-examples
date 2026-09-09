// Title: Generate DataBar Expanded Barcode with 2D Component Disabled and Verify PNG Output
// Description: The example creates a DataBar Expanded barcode, disables its 2‑D composite component, saves it as a PNG file, and confirms the image format by inspecting the file signature.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, illustrating how to configure DataBarParameters (e.g., disabling the 2D composite component) when creating barcodes. Typical use cases include customizing barcode symbologies for retail or logistics applications where only the linear portion is required. Developers often work with BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce and validate barcode images.
// Prompt: Use DataBarParameters to disable 2D component for specific barcode, verify output format.
// Tags: databar, barcode, disable 2d component, png output, aspose.barcode, generation, verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a DataBar Expanded barcode with the 2‑D composite component disabled,
/// save it as PNG, and verify the output format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output files.
        string outputDir = Path.Combine(Path.GetTempPath(), "DataBarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the PNG image to be saved.
        string pngPath = Path.Combine(outputDir, "Databar_No2DComponent.png");

        // Generate a DataBar Expanded barcode and disable its 2D composite component.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpanded, "(01)12345678901231"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Disable the 2D component of the DataBar barcode.
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the barcode image as a PNG file.
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved file is a PNG by checking its signature bytes.
        if (File.Exists(pngPath))
        {
            byte[] signature = new byte[8];
            using (FileStream fs = new FileStream(pngPath, FileMode.Open, FileAccess.Read))
            {
                fs.Read(signature, 0, signature.Length);
            }

            // PNG signature: 89 50 4E 47 0D 0A 1A 0A
            bool isPng = signature.Length == 8 &&
                         signature[0] == 0x89 && signature[1] == 0x50 &&
                         signature[2] == 0x4E && signature[3] == 0x47 &&
                         signature[4] == 0x0D && signature[5] == 0x0A &&
                         signature[6] == 0x1A && signature[7] == 0x0A;

            Console.WriteLine(isPng
                ? $"Output format verified: PNG ({pngPath})"
                : $"Output format verification failed for file: {pngPath}");
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}