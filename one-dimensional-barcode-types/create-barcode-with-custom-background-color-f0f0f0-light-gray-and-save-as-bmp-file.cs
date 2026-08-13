// Title: Generate a Code128 barcode with a custom light‑gray background and save as BMP
// Description: Demonstrates how to create a Code128 barcode, apply a custom background color (#F0F0F0), and export it as a BMP image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image format settings. Developers commonly need to customize barcode appearance (colors, fonts) and output formats for integration into documents, labels, or UI components. The snippet shows typical steps for setting parameters and saving the result.
// Prompt: Create a barcode with custom background color #F0F0F0 (light gray) and save as a BMP file.
// Tags: code128, barcode generation, bmp, background color, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom background color
/// and saves it as a BMP file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated BMP image.
        string outputPath = "barcode.bmp";

        // Initialize the barcode generator with Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that the barcode will encode.
            generator.CodeText = "123456";

            // Apply a custom light‑gray background color (#F0F0F0).
            generator.Parameters.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

            // Save the generated barcode image in BMP format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}