using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a PDF417 barcode with a top caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PDF417 barcode image with a caption above it and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "pdf417_top_caption.png";

        // Create a BarcodeGenerator instance for PDF417 encoding with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Enable the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;

            // Set the caption text.
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Optional: Align the caption to the center and set its font size.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}