using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Initialize a barcode generator with a default symbology (Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Retrieve default background color (should be White)
            Color defaultBackColor = generator.Parameters.BackColor;
            // Retrieve default bar (foreground) color (should be Black)
            Color defaultBarColor = generator.Parameters.Barcode.BarColor;

            // Display the default colors
            Console.WriteLine($"Default Background Color: {defaultBackColor.Name} (ARGB: {defaultBackColor.ToArgb():X8})");
            Console.WriteLine($"Default Bar Color: {defaultBarColor.Name} (ARGB: {defaultBarColor.ToArgb():X8})");
        }
    }
}