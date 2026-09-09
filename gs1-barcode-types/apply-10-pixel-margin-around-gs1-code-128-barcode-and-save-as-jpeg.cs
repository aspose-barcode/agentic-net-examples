// Title: Apply 10‑pixel margin to GS1 Code 128 barcode and save as JPEG
// Description: Demonstrates how to generate a GS1 Code 128 barcode, add a uniform 10‑pixel padding around it, and export the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode padding parameters. Developers commonly need to customize barcode appearance, such as adding margins, before saving to common image formats like JPEG for web or print use.
// Prompt: Apply a 10‑pixel margin around a GS1 Code 128 barcode and save as JPEG.
// Tags: gs1code128, margin, jpeg, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Code 128 barcode with a 10‑pixel margin on all sides and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies padding, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file in the current working directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "gs1code128.jpg");

        // Initialize the barcode generator with GS1 Code 128 symbology and the sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Apply a uniform 10‑pixel margin (padding) on all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode as a JPEG image to the specified file path.
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}