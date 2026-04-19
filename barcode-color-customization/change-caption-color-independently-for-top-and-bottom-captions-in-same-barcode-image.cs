using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Configure the top caption (above the barcode)
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Blue; // Independent color for top caption
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Configure the bottom caption (below the barcode)
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Green; // Independent color for bottom caption
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center;

            // Save the barcode image to a file
            generator.Save("barcode_with_captions.png");
        }

        Console.WriteLine("Barcode image generated successfully.");
    }
}