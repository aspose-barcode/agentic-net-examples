using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator with Code128 symbology and some sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set background color to gray
            generator.Parameters.BackColor = Color.Gray;
            // Save the barcode with gray background
            generator.Save("barcode_gray.png");

            // Reset background color to default white
            generator.Parameters.BackColor = Color.White;
            // Save the barcode with white background
            generator.Save("barcode_white.png");
        }
    }
}