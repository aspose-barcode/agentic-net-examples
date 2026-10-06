// Title: Create MaxiCode barcode with custom aspect ratio
// Description: Demonstrates generating a MaxiCode barcode and adjusting its width‑to‑height proportion using the AspectRatio property.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology configuration. It shows how to use the BarcodeGenerator class together with EncodeTypes, set barcode parameters such as XDimension and MaxiCode specific properties, and save the result as an image. Developers working with shipping labels, logistics, or inventory systems often need to customize MaxiCode appearance for scanner compatibility.
// Prompt: Create a MaxiCode barcode with aspect ratio 1.5 to adjust width‑to‑height proportion.
// Tags: maxicode, aspectratio, barcode generation, image output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a MaxiCode barcode and saves it as a PNG image with a custom aspect ratio.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its dimensions, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "MaxiCodeAspectRatio1.5.png");

        // Initialize the barcode generator for MaxiCode with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the module size (X dimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10;

            // Adjust the width‑to‑height proportion of the MaxiCode.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.5f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}