// Title: Create MaxiCode barcode with custom aspect ratio
// Description: Demonstrates generating a MaxiCode barcode, setting its aspect ratio to 1.5, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology‑specific settings. It illustrates the use of BarcodeGenerator, EncodeTypes, and MaxiCode parameters to control visual dimensions such as aspect ratio. Developers building logistics solutions, shipping labels, or inventory tags often need to fine‑tune MaxiCode size for scanner compatibility.
// Prompt: Create a MaxiCode barcode with aspect ratio 1.5 to adjust width‑to‑height proportion.
// Tags: maxicode, aspectratio, barcode generation, aspose.barcode, png, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode with a custom aspect ratio and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its aspect ratio, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the MaxiCode barcode.
        const string codeText = "Sample MaxiCode";

        // Initialize the barcode generator for the MaxiCode symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Adjust the height‑to‑width proportion to 1.5 for the generated barcode.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.5f;

            // Optional: set the image resolution (dots per inch) if higher quality is required.
            generator.Parameters.Resolution = 300;

            // Specify the output file path and save the barcode as a PNG image.
            const string outputPath = "maxicode_aspectratio.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved.
            Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        }
    }
}