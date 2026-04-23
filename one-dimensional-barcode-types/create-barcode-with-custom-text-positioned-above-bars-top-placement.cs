using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 (any 1D symbology can be used)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded
            generator.CodeText = "123456";

            // Configure the caption that will appear above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Optionally hide the default human‑readable code text below the bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated successfully.");
    }
}