// Title: Create Code128 barcode with specific bar height within image bounds
// Description: Demonstrates generating a Code128 barcode, setting the bar height to 40 mm, and configuring the image size so the bars stay inside the image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to control barcode dimensions using the BarcodeGenerator and its Parameters properties. It shows disabling auto‑size, setting BarHeight, and adjusting ImageWidth/ImageHeight to keep the barcode within the canvas—common tasks when creating printable barcodes for labels, receipts, or packaging.
// Prompt: Create a barcode, set Height to 40 mm, and ensure bars remain within image bounds.
// Tags: code128, barheight, image-bounds, png, barcodegenerator, parameters, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode image with a bar height of 40 mm,
/// ensuring the barcode fits within the defined image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures dimensions,
    /// and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing so the explicit BarHeight is applied.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the height of the barcode bars to 40 millimeters.
            generator.Parameters.Barcode.BarHeight.Millimeters = 40f;

            // Define image dimensions large enough to contain the bars and required padding.
            generator.Parameters.ImageWidth.Millimeters = 100f;
            generator.Parameters.ImageHeight.Millimeters = 50f;

            // Optional: set background to white and bars to black (default colors).
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file.
            generator.Save("barcode.png");
        }
    }
}