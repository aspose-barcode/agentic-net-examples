// Title: Generate DataMatrix barcode with caption and save as BMP
// Description: Demonstrates how to create a DataMatrix barcode, add a caption with a specific font size measured in document units, and save the image as a BMP file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as captions, font units, and image formats. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce a printable barcode image. Developers often need to customize captions, set precise font sizing, and export barcodes to various raster formats for integration into documents, labels, or UI components.
// Prompt: Set FontUnit to Document, define caption font size, and produce DataMatrix barcode saved as BMP file.
// Tags: datamatrix, barcode, caption, fontunit, bmp, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a DataMatrix barcode with a caption,
/// configures the caption font size using document units, and saves the result as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies caption settings,
    /// and writes the output file to the current directory.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output BMP file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataMatrix.bmp");

        // Initialize the barcode generator for DataMatrix symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Enable the caption above the barcode and set its text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "DataMatrix Sample";

            // Set the caption font size using Document units (1/300 inch). 12f corresponds to 12 document units.
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f;

            // Save the generated barcode as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}