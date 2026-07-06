// Title: Set GS1 Code 128 barcode size and export as JPEG
// Description: Demonstrates how to configure a GS1 Code 128 barcode's dimensions to 200 × 100 pixels and save it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating image size manipulation using the BarcodeGenerator class. Developers often need to control barcode dimensions for UI layout, printing, or integration into documents; the API provides AutoSizeMode and image size properties to achieve precise pixel sizing.
// Prompt: Set barcode size to 200 × 100 pixels for a GS1 Code 128 barcode and export as JPEG.
// Tags: gs1code128, barcode size, jpeg export, aspnet, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting a GS1 Code 128 barcode's size to 200 × 100 pixels and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures size, and saves the image.
    /// </summary>
    static void Main()
    {
        // Create a GS1 Code 128 barcode generator with sample GS1 data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Enable automatic sizing mode that respects explicit image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired image width to 200 pixels
            generator.Parameters.ImageWidth.Point = 200f;

            // Set the desired image height to 100 pixels
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated barcode as a JPEG image file
            generator.Save("gs1code128.jpg");
        }
    }
}