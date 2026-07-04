// Title: Code128 Barcode Generation with Custom Colors and Caption
// Description: Demonstrates creating a Code128 barcode with blue bars, white background, and a red caption, then saving it as a PNG file.
// Prompt: Generate a Code128 barcode with blue bars, white background, and red caption, saving to PNG.
// Tags: barcode, code128, generation, png, color, caption, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    /// <summary>
    /// Sample program that generates a Code128 barcode with custom colors and a caption.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Creates a barcode, customizes its appearance, and saves it as a PNG image.
        /// </summary>
        static void Main()
        {
            // Initialize a barcode generator for the Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the data to be encoded in the barcode
                generator.CodeText = "1234567890";

                // Apply a blue color to the barcode bars
                generator.Parameters.Barcode.BarColor = Color.Blue;

                // Set the background of the image to white
                generator.Parameters.BackColor = Color.White;

                // Enable and configure a red caption displayed above the barcode
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Text = "Sample Caption";
                generator.Parameters.CaptionAbove.TextColor = Color.Red;
                generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
                generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

                // Save the generated barcode as a PNG file in the current directory
                generator.Save("code128.png");
            }
        }
    }
}