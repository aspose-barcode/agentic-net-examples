using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Ensure the human‑readable code text is shown below the bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            // Align the code text to the left
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Configure the caption that appears below the barcode
            generator.Parameters.CaptionBelow.Text = "Sample Caption";
            generator.Parameters.CaptionBelow.Visible = true;
            // Align the caption to the left
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Left;

            // Optional: set some visual properties
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }
    }
}