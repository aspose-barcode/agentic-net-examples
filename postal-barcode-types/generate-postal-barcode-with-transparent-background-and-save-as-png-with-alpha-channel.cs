using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for a postal barcode (Postnet) with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set the background to transparent so the PNG will contain an alpha channel.
            generator.Parameters.BackColor = Color.Transparent;

            // Set the bar (foreground) color if desired (default is black).
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image as PNG, preserving the transparent background.
            generator.Save("postal.png");
        }
    }
}