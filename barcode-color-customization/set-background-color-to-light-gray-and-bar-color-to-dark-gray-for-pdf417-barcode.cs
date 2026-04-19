using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a PDF417 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample Text"))
        {
            // Set the background color to light gray
            generator.Parameters.BackColor = Color.LightGray;
            // Set the bar (foreground) color to dark gray
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Save the barcode image to a PNG file
            generator.Save("pdf417_gray.png");
        }
    }
}