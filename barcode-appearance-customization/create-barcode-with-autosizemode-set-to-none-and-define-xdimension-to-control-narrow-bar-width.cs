// Title: Generate Code128 barcode with custom XDimension and disabled AutoSize
// Description: Demonstrates creating a Code128 barcode, turning off automatic sizing, and setting the narrow bar width via XDimension.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode dimensions using the BarcodeGenerator, EncodeTypes, and AutoSizeMode classes. Developers often need to produce barcodes with precise module widths for printing or scanning requirements, and this snippet shows the typical steps for customizing size parameters before saving the image.
// Prompt: Create a barcode with AutoSizeMode set to None and define XDimension to control narrow bar width.
// Tags: code128, autosizemode, xdimension, barcode generation, png output, aspnet.barcode, generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Program demonstrating barcode generation with custom sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, disables auto sizing, sets XDimension, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology (any 1D type could be used)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode
            generator.CodeText = "1234567890";

            // Turn off automatic size calculation so we can define dimensions manually
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Specify the narrow bar width (XDimension) in points; 2 points per module in this case
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}