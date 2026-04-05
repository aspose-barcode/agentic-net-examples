using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set semi‑transparent bar color (alpha 128, blue)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 0, 255);
            // Optional: set background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a PNG image
            generator.Save("barcode.png");
        }
    }
}