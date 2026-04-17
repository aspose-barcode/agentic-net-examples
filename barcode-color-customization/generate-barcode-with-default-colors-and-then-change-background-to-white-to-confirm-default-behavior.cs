using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a barcode with default colors (black bars on white background)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save("barcode_default.png");
        }

        // Generate a barcode and explicitly set the background color to white
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.BackColor = Color.White; // Explicitly set background to white
            generator.Save("barcode_white.png");
        }
    }
}