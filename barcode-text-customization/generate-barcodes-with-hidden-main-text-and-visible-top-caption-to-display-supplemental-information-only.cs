// Title: Generate barcode with hidden main text and visible top caption
// Description: Demonstrates how to create a Code128 barcode where the encoded data is hidden while a supplemental caption is displayed above the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionAbove settings. Developers often need to hide the primary barcode data and show additional information such as product details, batch numbers, or instructions above the barcode. The snippet shows typical configuration steps for visual customization and output format selection.
// Prompt: Generate barcodes with hidden main text and visible top caption to display supplemental information only.
// Tags: code128, hidden text, top caption, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Example program that generates a Code128 barcode with hidden main text and a visible top caption.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Creates the output directory, configures the barcode generator,
        /// hides the main barcode text, adds a top caption, and saves the result as a PNG image.
        /// </summary>
        static void Main()
        {
            // Determine and create the output folder if it does not exist
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Full path for the generated barcode image
            string outputPath = Path.Combine(outputFolder, "barcode.png");

            // Text to encode (will be hidden) and the caption to display above the barcode
            string hiddenCodeText = "1234567890";
            string topCaption = "Supplemental Information";

            // Initialize the barcode generator with Code128 symbology and the hidden text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, hiddenCodeText))
            {
                // Hide the main barcode text so only the caption is visible
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Configure the top caption (visible above the barcode)
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Text = topCaption;
                generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
                generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
                generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Blue;

                // Set barcode and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image in PNG format
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}