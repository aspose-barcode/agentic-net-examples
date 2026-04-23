using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the barcode image to a PNG file
            generator.Save("barcode.png");
        }
    }
}