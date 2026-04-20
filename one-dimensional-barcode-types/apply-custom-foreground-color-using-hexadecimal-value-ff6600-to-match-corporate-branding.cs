using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "123ABC";

            // Apply custom foreground (bar) color #FF6600 (orange)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 255, 102, 0);

            // Save the generated barcode image
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated with custom color.");
    }
}