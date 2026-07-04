// Title: Resetting Barcode Background Color
// Description: Demonstrates how to change a barcode's background color to gray, save it, then reset the background to the default white and save again.
// Prompt: Reset the barcode background to default white after previously setting it to gray.
// Tags: barcode symbology, background reset, png output, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to modify and then reset a barcode's background color.
/// </summary>
class Program
{
    /// <summary>
    /// Generates two barcode images: one with a gray background and another with the default white background.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for a Code128 barcode with the value "12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set the background color to gray and save the first image
            generator.Parameters.BackColor = Color.Gray;
            generator.Save("barcode_gray.png");

            // Reset the background color to the default white and save the second image
            generator.Parameters.BackColor = Color.White;
            generator.Save("barcode_white.png");
        }

        // Inform the user that the barcode images have been created
        Console.WriteLine("Barcodes generated: barcode_gray.png (gray background), barcode_white.png (white background).");
    }
}