// Title: Generate Code 16K barcode with aspect ratio nine and save as PNG
// Description: Demonstrates creating a Code 16K barcode, setting its aspect ratio to nine, and saving it as a PNG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure symbology‑specific parameters such as aspect ratio for Code 16K. It showcases the use of the BarcodeGenerator class together with EncodeTypes and barcode parameter objects, a common task for developers needing to produce high‑density linear barcodes for inventory, shipping, or packaging applications.
// Prompt: Generate a Code 16K barcode with aspect ratio nine and save as PNG image.
// Tags: code16k, barcode, generation, png, aspose.barcode, aspectratio

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a Code 16K barcode with a custom aspect ratio
/// and saves it as a PNG image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code 16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
        {
            // Define the data to encode in the barcode
            generator.CodeText = "1234567890";

            // Configure the Code 16K specific parameter: set aspect ratio to 9
            generator.Parameters.Barcode.Code16K.AspectRatio = 9f;

            // Save the generated barcode as a PNG image in the current directory
            generator.Save("code16k.png");
        }
    }
}