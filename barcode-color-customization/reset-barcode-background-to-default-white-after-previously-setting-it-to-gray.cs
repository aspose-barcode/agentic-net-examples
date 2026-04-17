using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 and set the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set background to gray and save the image
            generator.Parameters.BackColor = Color.Gray;
            generator.Save("barcode_gray.png");

            // Reset background to default white and save the new image
            generator.Parameters.BackColor = Color.White;
            generator.Save("barcode_white.png");
        }
    }
}