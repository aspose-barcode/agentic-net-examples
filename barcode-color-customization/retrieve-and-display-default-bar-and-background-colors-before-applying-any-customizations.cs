using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator with a sample symbology (Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Retrieve default background and bar colors
            Color defaultBackColor = generator.Parameters.BackColor;
            Color defaultBarColor = generator.Parameters.Barcode.BarColor;

            // Display the default colors
            Console.WriteLine($"Default Background Color: {defaultBackColor}");
            Console.WriteLine($"Default Bar Color: {defaultBarColor}");
        }
    }
}