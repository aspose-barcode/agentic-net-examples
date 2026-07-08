// Title: Apply custom background color to ITF14 barcode and export as JPEG
// Description: Demonstrates setting a custom background color for an ITF14 barcode before rendering and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual properties such as background and bar colors using the BarcodeGenerator class. Typical use cases include branding, UI integration, and printing where specific color schemes are required. Developers often need to adjust colors, sizes, and output formats for various barcode symbologies.
// Prompt: Apply custom background color to ITF barcodes before rendering, export JPEG.
// Tags: barcode, itf14, background color, jpeg, aspose.barcode, generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom background color to an ITF14 barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, sets visual parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Create an ITF14 barcode generator with sample numeric data
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "123456789012"))
        {
            // Set a custom background color (light gray)
            generator.Parameters.BackColor = Color.LightGray;

            // Optionally customize bar color (default is black)
            // generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a JPEG image file
            generator.Save("itf_barcode.jpg");
        }
    }
}