// Title: Generate DataMatrix barcode with caption and custom font size saved as BMP
// Description: Demonstrates creating a DataMatrix barcode, adding a visible caption, setting the caption font size using Document units, and saving the image as a BMP file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DataMatrix. It covers typical tasks such as configuring caption visibility, text, and font sizing—common requirements for product labeling, packaging, and inventory tracking where readable human‑readable text accompanies machine‑readable barcodes.
// Prompt: Set FontUnit to Document, define caption font size, and produce DataMatrix barcode saved as BMP file.
// Tags: datamatrix, barcode, caption, fontunit, bmp, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode with a caption,
/// sets the caption font size using Document units, and saves the result as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures caption properties, and saves the image.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output BMP file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataMatrix.bmp");

        // Create a BarcodeGenerator for the DataMatrix symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Make the caption appear above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;

            // Set the caption text that will be displayed.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Define the caption font size using Document units (points relative to the document).
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f;

            // Save the generated barcode as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}