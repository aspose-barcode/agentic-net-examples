using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the foreground (bar) color to lime (#00FF00)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 255, 0);

            // Increase resolution for higher quality output
            generator.Parameters.Resolution = 300;

            // Save the barcode as a TIFF image
            generator.Save("barcode.tif");
        }
    }
}