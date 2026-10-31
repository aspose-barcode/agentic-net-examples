// Title: Generate Code128 Barcode PNG with Inches Dimensions
// Description: Demonstrates how to create a Code128 barcode, set image size using inches, and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical scenarios include creating barcodes for product labels, shipping documents, or inventory systems where precise physical dimensions are required. Developers often need to control image size units, choose symbologies, and export to common image formats like PNG.
/// Prompt: Instantiate BarcodeGenerator, set unit to Inches, specify width and height, and generate a PNG image.
/// Tags: code128, barcode, generation, png, inches, aspose.barcode, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode image with dimensions specified in inches.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode, sets size in inches, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.png");

        // Initialize the generator with Code128 symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set image width to 2 inches.
            generator.Parameters.ImageWidth.Inches = 2f;

            // Set image height to 1 inch.
            generator.Parameters.ImageHeight.Inches = 1f;

            // Generate the barcode image and save it as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}