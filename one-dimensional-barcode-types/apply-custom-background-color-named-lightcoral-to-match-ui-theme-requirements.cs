using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "Sample123";

            // Apply custom background color LightCoral
            generator.Parameters.BackColor = Color.LightCoral;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated with LightCoral background.");
    }
}