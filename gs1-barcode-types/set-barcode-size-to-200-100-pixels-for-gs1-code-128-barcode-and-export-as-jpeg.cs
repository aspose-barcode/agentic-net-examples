// Title: Generate GS1 Code 128 barcode with custom size and save as JPEG
// Description: Demonstrates how to create a GS1 Code 128 barcode, set its image dimensions to 200 × 100 pixels, and export it as a JPEG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include product labeling, inventory tracking, and any application requiring GS1-compliant barcodes with specific image sizing. Developers often need to control barcode dimensions and output formats for integration into web pages, print media, or mobile apps.
// Prompt: Set barcode size to 200 × 100 pixels for a GS1 Code 128 barcode and export as JPEG.
// Tags: gs1 code128, barcode generation, image size, jpeg export, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// configures a fixed image size, and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "gs1code128.jpg");

        // Create a barcode generator for GS1 Code 128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Configure the barcode image to have a fixed size of 200 × 100 pixels.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}