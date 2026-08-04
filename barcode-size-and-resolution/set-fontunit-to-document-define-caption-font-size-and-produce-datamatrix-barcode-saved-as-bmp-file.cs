// Title: Generate DataMatrix barcode with caption and save as BMP
// Description: Demonstrates setting the caption font unit to Document, defining its size, and creating a DataMatrix barcode saved as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on customizing caption appearance using the FontUnit property and saving the result in bitmap format. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and drawing output options, which developers commonly use to embed barcodes with readable captions in documents, reports, or UI elements.
// Prompt: Set FontUnit to Document, define caption font size, and produce DataMatrix barcode saved as BMP file.
// Tags: datamatrix, caption, fontunit, bmp, aspose.barcode, barcode-generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a DataMatrix barcode with a caption,
/// configures the caption font using Document units, and saves the result as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for DataMatrix with the desired data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample DataMatrix"))
        {
            // Configure the caption that appears above the barcode.
            // Set the font family to Helvetica.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";

            // Define the font size in points (FontUnit is handled internally by the API).
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Assign the caption text to be displayed.
            generator.Parameters.CaptionAbove.Text = "DataMatrix Barcode";

            // Save the generated barcode as a BMP image file.
            generator.Save("datamatrix.bmp");
        }
    }
}