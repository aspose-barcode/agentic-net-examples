// Title: Generate GS1 Code 128 barcode with custom size and save as JPEG
// Description: Demonstrates how to create a GS1 Code 128 barcode, set its image dimensions to 200 × 100 pixels, and export it as a JPEG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control barcode appearance. Typical scenarios include creating barcodes for product labeling, inventory systems, and e‑commerce platforms where specific image sizes and formats are required. Developers often need to adjust dimensions, scaling, and output formats to integrate barcodes into existing graphics pipelines.
// Prompt: Set barcode size to 200 × 100 pixels for a GS1 Code 128 barcode and export as JPEG.
// Tags: gs1 code128, barcode size, jpeg export, aspose.barcode, image generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// customizes its image size, and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a barcode, configures size, and writes the output image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the GS1 Code 128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128))
        {
            // Assign a valid GS1 Code 128 codetext (GTIN‑14 example)
            generator.CodeText = "(01)12345678901231";

            // Set the desired canvas dimensions: 200 × 100 pixels
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Enable interpolation to scale the barcode to fit the specified canvas
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the generated barcode as a JPEG image
            generator.Save("gs1code128.jpg");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated and saved as gs1code128.jpg");
    }
}