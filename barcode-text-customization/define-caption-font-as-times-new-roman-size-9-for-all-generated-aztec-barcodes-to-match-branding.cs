// Title: Generate Aztec Barcodes with Custom Caption Font
// Description: Demonstrates how to generate Aztec barcodes and set the caption font to Times New Roman, size 9, for branding purposes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating barcodes with customized captions for product labeling, inventory tracking, and marketing materials. Developers often need to adjust caption appearance to match corporate branding guidelines.
// Prompt: Define caption font as Times New Roman, size 9, for all generated Aztec barcodes to match branding.
// Tags: aztec, barcode, caption, font, times new roman, size 9, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates Aztec barcodes with customized caption fonts.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates three Aztec barcodes, each with captions
    /// using Times New Roman font at 9 points, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a temporary output directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "AztecDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Generate three barcode images with different sample texts.
        for (int i = 1; i <= 3; i++)
        {
            // Define the barcode text and the target file path.
            string codeText = $"AztecSample{i}";
            string filePath = Path.Combine(outputDir, $"Aztec_{i}.png");

            // Initialize the barcode generator for Aztec symbology.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, codeText))
            {
                // Set caption font to Times New Roman, size 9 points for both above and below captions.
                generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
                generator.Parameters.CaptionAbove.Font.Size.Point = 9f;
                generator.Parameters.CaptionBelow.Font.FamilyName = "Times New Roman";
                generator.Parameters.CaptionBelow.Font.Size.Point = 9f;

                // Make captions visible and assign sample text.
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Text = $"Above {i}";
                generator.Parameters.CaptionBelow.Visible = true;
                generator.Parameters.CaptionBelow.Text = $"Below {i}";

                // Save the generated barcode as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated file.
            Console.WriteLine($"Generated: {filePath}");
        }

        // Indicate that the barcode generation process has completed.
        Console.WriteLine("Aztec barcode generation completed.");
    }
}