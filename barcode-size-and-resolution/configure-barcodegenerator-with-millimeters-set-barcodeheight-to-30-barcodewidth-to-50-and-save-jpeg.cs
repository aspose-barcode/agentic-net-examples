// Title: Generate Code128 Barcode with Millimeter Dimensions and Save as JPEG
// Description: Demonstrates configuring Aspose.BarCode's BarcodeGenerator to use millimeter units, set specific height and width, and save the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create barcodes with precise physical dimensions using the BarcodeGenerator class. Typical use cases include printing barcodes on labels or packaging where exact size specifications are required. Developers often need to set image size units, adjust dimensions, and export to common image formats.
// Prompt: Configure BarcodeGenerator with Millimeters, set BarCodeHeight to 30, BarCodeWidth to 50, and save JPEG.
// Tags: code128, barcode generation, image size, millimeters, jpeg, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, sets its size using millimeter units,
/// and saves the image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Define the text to encode in the barcode
            generator.CodeText = "123456";

            // Set the barcode image height to 30 millimeters
            generator.Parameters.ImageHeight.Millimeters = 30f;

            // Set the barcode image width to 50 millimeters
            generator.Parameters.ImageWidth.Millimeters = 50f;

            // Save the generated barcode as a JPEG image file
            generator.Save("barcode.jpg");
        }

        // Inform the user that the barcode image has been saved
        Console.WriteLine("Barcode image saved as barcode.jpg");
    }
}