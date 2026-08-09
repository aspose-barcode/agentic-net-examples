// Title: Generate MaxiCode Mode 6 barcode with transparent background and save to memory stream
// Description: Demonstrates how to create a MaxiCode Mode 6 barcode, set a transparent background, and write the PNG image to a MemoryStream using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeStandardCodetext, and image formatting options. Developers often need to generate high‑density 2‑D barcodes for logistics and apply custom visual settings like transparent backgrounds before streaming the result.
// Prompt: Create a MaxiCode Mode 6 barcode, apply a transparent background, and write the file to a memory stream.
// Tags: maxicode, mode6, transparent background, memory stream, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 6 barcode with a transparent background and saving it to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies visual settings, and outputs the image size.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode standard codetext for Mode 6
        var maxiCode = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode6,
            Message = "Sample message"
        };

        // Create a memory stream to hold the generated image
        using (var ms = new MemoryStream())
        {
            // Initialize the complex barcode generator with the MaxiCode settings
            using (var generator = new ComplexBarcodeGenerator(maxiCode))
            {
                // Apply a transparent background to the barcode image
                generator.Parameters.BackColor = Color.Transparent;

                // Save the barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Output the size of the generated image (for demonstration purposes)
            Console.WriteLine($"Generated barcode image size: {ms.Length} bytes");
        }
    }
}