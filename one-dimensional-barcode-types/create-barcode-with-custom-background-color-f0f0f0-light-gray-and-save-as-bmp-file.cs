// Title: Create Code128 barcode with custom background color and save as BMP
// Description: Demonstrates how to generate a Code128 barcode with a light gray background and save it as a BMP image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and drawing parameters to customize appearance. Typical use cases include creating branded barcodes with specific background colors for print or digital media. Developers often need to adjust visual properties like background and foreground colors before exporting to various image formats.
// Prompt: Create a barcode with custom background color #F0F0F0 (light gray) and save as a BMP file.
// Tags: code128, barcode generation, bmp, background color, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
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
        // Initialize a BarcodeGenerator for Code128 symbology with sample data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the background color to light gray (#F0F0F0)
            generator.Parameters.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

            // Save the generated barcode image as a BMP file named "barcode.bmp"
            generator.Save("barcode.bmp");
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine("Barcode saved as barcode.bmp");
    }
}