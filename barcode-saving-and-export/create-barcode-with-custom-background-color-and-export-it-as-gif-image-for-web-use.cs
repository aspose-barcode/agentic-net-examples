using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a custom background color (e.g., light gray)
            generator.Parameters.BackColor = Color.FromArgb(240, 240, 240);

            // Optionally, set the foreground (bars) color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a GIF image suitable for web use
            generator.Save("barcode.gif");
        }
    }
}