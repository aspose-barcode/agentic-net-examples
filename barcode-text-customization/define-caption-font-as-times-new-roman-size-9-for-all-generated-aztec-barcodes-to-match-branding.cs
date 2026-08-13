// Title: Generate Aztec Barcode with Times New Roman Caption Font
// Description: Creates an Aztec barcode image and sets the caption font to Times New Roman, size 9, demonstrating how to customize barcode captions using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and the Parameters.Caption* properties to customize barcode appearance. Typical scenarios include branding, adding descriptive text above or below barcodes, and exporting to common image formats. Developers often need to adjust fonts, sizes, and caption content to meet visual guidelines.
// Prompt: Define caption font as Times New Roman, size 9, for all generated Aztec barcodes to match branding.
// Tags: aztec, barcode, caption, font, times new roman, size9, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate an Aztec barcode and apply a custom caption font using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates an Aztec barcode, sets caption fonts, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputFile = "aztec.png";

        // Initialize the barcode generator for the Aztec symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "Sample"))
        {
            // Configure the caption font for the text displayed above the barcode.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 9f;

            // Configure the caption font for the text displayed below the barcode.
            generator.Parameters.CaptionBelow.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionBelow.Font.Size.Point = 9f;

            // Optional: set custom caption texts.
            generator.Parameters.CaptionAbove.Text = "Aztec Barcode";
            generator.Parameters.CaptionBelow.Text = "Sample";

            // Save the generated barcode image to the specified file.
            generator.Save(outputFile);
        }

        // Output the full path of the saved barcode image for user reference.
        Console.WriteLine($"Aztec barcode saved to: {Path.GetFullPath(outputFile)}");
    }
}