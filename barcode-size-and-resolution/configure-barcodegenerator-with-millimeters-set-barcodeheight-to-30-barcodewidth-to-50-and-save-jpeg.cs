// Title: Generate Code128 Barcode with Millimeter Dimensions and Save as JPEG
// Description: Demonstrates configuring Aspose.BarCode's BarcodeGenerator to use millimeter units, set specific height and width, and export the barcode as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control image size using the Parameters.ImageHeight and ImageWidth properties with millimeter units. Developers commonly use these APIs to produce barcodes with precise physical dimensions for printing on labels, packaging, or documents. The key classes shown are BarcodeGenerator, EncodeTypes, and the Parameters sub‑objects, which are essential for customizing barcode appearance.
// Prompt: Configure BarcodeGenerator with Millimeters, set BarCodeHeight to 30, BarCodeWidth to 50, and save JPEG.
// Tags: code128, barcode generation, jpeg, millimeters, aspose.barcode, barcodegenerator, parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, sets its size in millimeters, and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the barcode image height to 30 millimeters
            generator.Parameters.ImageHeight.Millimeters = 30f;

            // Configure the barcode image width to 50 millimeters
            generator.Parameters.ImageWidth.Millimeters = 50f;

            // Save the generated barcode as a JPEG image file named "barcode.jpg"
            generator.Save("barcode.jpg");
        }
    }
}