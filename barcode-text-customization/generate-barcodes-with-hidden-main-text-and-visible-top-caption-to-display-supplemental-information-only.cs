// Title: Generate barcode with hidden main text and visible top caption
// Description: Demonstrates how to create a Code128 barcode where the primary human‑readable text is hidden and a supplemental caption is shown above the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionAbove settings. Developers often need to hide the default barcode text while adding custom labels such as order numbers, dates, or other metadata above the barcode. The snippet shows typical configuration steps for text location, caption visibility, alignment, font, and colors, useful for creating clean, information‑rich barcode images.
// Prompt: Generate barcodes with hidden main text and visible top caption to display supplemental information only.
// Tags: code128, barcode generation, hidden text, top caption, aspnet, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with hidden main text and a visible top caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Ensure the target directory exists; create it if necessary
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a BarcodeGenerator for Code128 with the desired code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Hide the default human‑readable barcode text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the caption that appears above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Order ID: 98765";
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Black;

            // Optional: set barcode and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}