using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background color to transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG image with an alpha channel
            generator.Save("transparent_barcode.png");
        }
    }
}