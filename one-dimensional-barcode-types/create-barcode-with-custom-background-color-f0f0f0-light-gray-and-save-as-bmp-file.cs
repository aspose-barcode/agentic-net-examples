// Title: Create Code128 barcode with custom light gray background and save as BMP
// Description: Demonstrates how to generate a Code128 barcode, apply a custom background color, and save the result as a BMP image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating barcodes for product labeling, inventory tracking, or packaging, where developers need to customize visual aspects such as background color and output format.
// Prompt: Create a barcode with custom background color #F0F0F0 (light gray) and save as a BMP file.
// Tags: code128, background color, bmp, aspose.barcodes, aspose.drawing, barcode generation

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom light gray background
/// and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "barcode.bmp";

        // Initialize the BarcodeGenerator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the background color to light gray (#F0F0F0).
            generator.Parameters.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

            // Save the generated barcode image in BMP format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output a confirmation message indicating where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}