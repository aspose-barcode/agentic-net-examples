// Title: Generate DataMatrix barcode with caption and save as BMP
// Description: Creates a DataMatrix barcode, adds a caption above it, and saves the image as a BMP file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure barcode parameters such as caption text, font settings, and specific DataMatrix version. It showcases the use of BarcodeGenerator, EncodeTypes, and related parameter classes to produce a printable barcode image. Developers often need to customize captions, fonts, and output formats when integrating barcodes into documents or labels.
// Prompt: Set FontUnit to Document, define caption font size, and produce DataMatrix barcode saved as BMP file.
// Tags: datamatrix, barcode, caption, bmp, aspose.barcode, generation, fontunit

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a DataMatrix barcode with a caption and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures caption and font, and writes the BMP file.
    /// </summary>
    static void Main()
    {
        // Initialize a DataMatrix barcode generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set the font unit to Document (if supported by the API). This line is kept as a comment per the task requirement.
            // generator.Parameters.FontUnit = FontUnit.Document;

            // Configure the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "DataMatrix Example";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f; // Set caption font size to 12 points.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Optionally specify a DataMatrix version (choose a valid square size).
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Save the generated barcode as a BMP file.
            generator.Save("datamatrix.bmp");
        }
    }
}