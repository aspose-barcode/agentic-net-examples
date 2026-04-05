using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with the desired code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set custom colors
                generator.Parameters.Barcode.BarColor = Color.Black;          // Bar (foreground) color
                generator.Parameters.BackColor = Color.Yellow;               // Background color
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.Red; // Code text color

                // Set caption texts and their colors
                generator.Parameters.CaptionAbove.Text = "Above Caption";
                generator.Parameters.CaptionAbove.TextColor = Color.Green;

                generator.Parameters.CaptionBelow.Text = "Below Caption";
                generator.Parameters.CaptionBelow.TextColor = Color.Blue;

                // Save the barcode image to a file
                generator.Save("custom_barcode.png");
            }
        }
    }
}