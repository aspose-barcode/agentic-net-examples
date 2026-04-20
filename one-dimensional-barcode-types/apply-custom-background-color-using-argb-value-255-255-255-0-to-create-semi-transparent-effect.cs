using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the code text to encode
            generator.CodeText = "123ABC";

            // Apply a semi‑transparent background color (ARGB: 255,255,255,0)
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255, 0);

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}