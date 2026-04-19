using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set custom colors
            generator.Parameters.Barcode.BarColor = Color.Blue;          // Bars color
            generator.Parameters.BackColor = Color.Yellow;              // Background color
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green; // Human‑readable text color

            // Configure caption above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Above Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Purple;

            // Configure caption below the barcode
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Below Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Orange;

            // Save the barcode image
            generator.Save("custom_colors.png");
        }
    }
}