// Title: Generate GS1 Code 128 Barcode, Rotate 90°, Save as BMP
// Description: Demonstrates creating a GS1 Code 128 barcode, rotating the image 90 degrees clockwise, and saving it as a BMP file.
// Category-Description: This example is part of the Aspose.BarCode barcode generation collection, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128 to produce GS1‑compliant barcodes, apply image rotation, and export to bitmap formats. Developers often need to generate GS1 barcodes for product identification, adjust orientation for printing layouts, and save in various image types such as BMP, PNG, or JPEG.
// Prompt: Create a GS1 Code 128 barcode, rotate the image 90 degrees clockwise, and store as BMP.
// Tags: gs1,code128,barcode,generation,rotation,bmp,aspose.barcode

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode, rotating it, and saving as BMP.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies rotation, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code 128 codetext: AI (01) with a 14‑digit GTIN
        const string codeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 Code 128 with the specified codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set rotation angle to 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode image as a BMP file
            generator.Save("gs1code128.bmp");
        }
    }
}