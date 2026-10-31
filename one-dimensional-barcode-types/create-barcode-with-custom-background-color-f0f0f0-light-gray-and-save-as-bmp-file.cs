// Title: Create a Code128 barcode with custom light gray background and save as BMP
// Description: Demonstrates how to generate a Code128 barcode, apply a custom background color (#F0F0F0), and save the result as a BMP image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating barcodes for product labeling, inventory tracking, and document automation where visual customization such as background color is required. Developers often need to adjust appearance settings and export to various image formats for integration into applications.
// Prompt: Create a barcode with custom background color #F0F0F0 (light gray) and save as a BMP file.
// Tags: code128, barcode generation, background color, bmp, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a custom light gray background and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its appearance, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "custom_background.bmp");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Apply a custom background color (light gray #F0F0F0).
            generator.Parameters.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

            // Save the generated barcode as a BMP image.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}