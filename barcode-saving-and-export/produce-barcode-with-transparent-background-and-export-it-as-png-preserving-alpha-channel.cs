using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeTransparentBackground
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set background to transparent
                generator.Parameters.BackColor = Color.Transparent;

                // Save the barcode as PNG (PNG supports alpha channel)
                generator.Save("barcode_transparent.png");
            }
        }
    }
}