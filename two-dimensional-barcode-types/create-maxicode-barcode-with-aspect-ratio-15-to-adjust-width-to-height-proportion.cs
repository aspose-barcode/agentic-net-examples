// Title: Generate a MaxiCode barcode with custom aspect ratio
// Description: Demonstrates creating a MaxiCode barcode and adjusting its width‑to‑height proportion using the AspectRatio property.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology-specific settings. It showcases the BarcodeGenerator class and its Parameters for MaxiCode, a common need when developers must control barcode dimensions for packaging or scanning requirements. Typical use cases include customizing aspect ratios for optimal scanner performance.
// Prompt: Create a MaxiCode barcode with aspect ratio 1.5 to adjust width‑to‑height proportion.
// Tags: maxicode, barcode generation, aspect ratio, png, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program class containing the entry point for generating a MaxiCode barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a MaxiCode barcode with an aspect ratio of 1.5 and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for MaxiCode with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Adjust the barcode's aspect ratio (height/width) to 1.5.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.5f;

            // Save the generated barcode image as a PNG file.
            generator.Save("maxicode.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("MaxiCode barcode generated: maxicode.png");
    }
}