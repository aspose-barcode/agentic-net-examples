// Title: Generate QR Code with Fixed Dimensions
// Description: Demonstrates creating a QR Code barcode, disabling automatic sizing, and enforcing specific image width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode image size using the BarcodeGenerator class. Developers often need to produce barcodes with exact dimensions for layout constraints, printing templates, or UI components. The snippet shows usage of EncodeTypes, AutoSizeMode, and dimension properties to achieve consistent output.
// Prompt: Generate QR Code barcode and disable automatic size to enforce fixed dimensions.
// Tags: qr code, fixed size, autosizemode, image dimensions, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode with fixed image dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, disables auto‑sizing, sets explicit width and height, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text/content
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Turn off automatic size calculation to allow manual dimension control
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Define the exact image width and height (in points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Optionally adjust the module (dot) size to better fit the fixed canvas
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Persist the generated barcode to a PNG file
            generator.Save("qr_fixed.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("QR Code generated and saved as 'qr_fixed.png'.");
    }
}