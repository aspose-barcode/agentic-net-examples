// Title: Disable 2D Composite Component in DataBar Limited Barcode and Verify PNG Output
// Description: Demonstrates how to generate a DataBar Limited barcode with the 2‑D composite component disabled, save it as a PNG file, and verify the image format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar symbologies. It shows how to configure DataBarParameters, such as disabling the 2D composite component, and how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce and validate barcode images. Developers working with retail or GS1 barcodes often need to customize DataBar settings and confirm output formats.
// Prompt: Use DataBarParameters to disable 2D component for specific barcode, verify output format.
// Tags: databar, databarlimited, disable-2d-component, png, verification, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates disabling the 2D composite component of a DataBar Limited barcode,
/// saving it as a PNG image, and verifying the file format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and checks the PNG signature.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "databar.png";

        // Remove any existing file to ensure a clean run.
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a DataBar Limited barcode generator with a valid GTIN-like text.
        // The format "(01)08888888888888" complies with GS1 requirements for DataBar Limited.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DatabarLimited, "(01)08888888888888"))
        {
            // Access DataBar-specific parameters.
            // Disable the 2D composite component (default is false; set explicitly for clarity).
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Optionally adjust other visual parameters, such as the X-dimension.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and that it has the expected PNG format.
        if (File.Exists(outputPath))
        {
            // Read the first 8 bytes of the file to check the PNG signature.
            byte[] header = new byte[8];
            using (FileStream fs = new FileStream(outputPath, FileMode.Open, FileAccess.Read))
            {
                fs.Read(header, 0, header.Length);
            }

            // PNG files start with the following byte sequence: 89 50 4E 47 0D 0A 1A 0A.
            bool isPng = header.Length == 8 &&
                         header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E &&
                         header[3] == 0x47 && header[4] == 0x0D && header[5] == 0x0A &&
                         header[6] == 0x1A && header[7] == 0x0A;

            Console.WriteLine(isPng
                ? $"Barcode saved successfully as PNG: {outputPath}"
                : $"Barcode saved, but file format verification failed: {outputPath}");
        }
        else
        {
            Console.WriteLine("Failed to create the barcode image.");
        }
    }
}