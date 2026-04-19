using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a Code128 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = Color.DarkGreen;

            // Save the barcode as a BMP file
            generator.Save("code128.bmp");
        }
    }
}