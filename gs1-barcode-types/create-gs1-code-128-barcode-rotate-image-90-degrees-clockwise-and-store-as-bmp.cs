// Title: Create and rotate a GS1 Code 128 barcode saved as BMP
// Description: Demonstrates generating a GS1 Code 128 barcode, rotating the image 90° clockwise, and saving it in BMP format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. It illustrates typical tasks such as setting rotation angles and exporting to bitmap images, which developers often need when integrating barcodes into documents or printing workflows.
// Prompt: Create a GS1 Code 128 barcode, rotate the image 90 degrees clockwise, and store as BMP.
// Tags: gs1, code128, barcode, generation, rotation, bmp, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode, rotates it, and saves as BMP.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define sample GS1 Code 128 data (Application Identifier 01 - GTIN)
        const string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 Code 128 symbology and the data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set rotation angle to 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode image as a BMP file
            generator.Save("gs1code128.bmp");
        }
    }
}