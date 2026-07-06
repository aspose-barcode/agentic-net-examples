// Title: Generate Code128 Barcode Image with Specified Size in Inches
// Description: Demonstrates how to create a Code128 barcode, set its dimensions in inches, and save it as a PNG file.
// Category-Description: Shows basic usage of Aspose.BarCode for barcode generation, covering the BarcodeGenerator class, EncodeTypes enumeration, and image parameter settings. Typical scenarios include creating printable barcodes with precise physical dimensions for labels, packaging, or inventory systems. Developers often need to control unit measurement, size, and output format when integrating barcode creation into .NET applications.
// Prompt: Instantiate BarcodeGenerator, set unit to Inches, specify width and height, and generate a PNG image.
// Tags: code128, barcode generation, inches, image size, png, aspose.barcode, barcodegenerator, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging; // retained for completeness, though not required for the Save overload

/// <summary>
/// Example program that generates a Code128 barcode with specific dimensions in inches
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures size in inches, and writes a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that will be encoded into the barcode
            generator.CodeText = "123456";

            // Define the image dimensions using inches (2 inches wide, 1 inch tall)
            generator.Parameters.ImageWidth.Inches = 2f;   // width in inches
            generator.Parameters.ImageHeight.Inches = 1f;  // height in inches

            // Save the generated barcode as a PNG file in the current directory
            generator.Save("barcode.png");
        }
    }
}