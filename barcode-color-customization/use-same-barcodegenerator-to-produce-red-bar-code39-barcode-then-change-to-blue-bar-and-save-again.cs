using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a Code39 barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Set the bar (foreground) color to red
            generator.Parameters.Barcode.BarColor = Color.Red;
            // Save the red barcode image
            generator.Save("code39_red.png");

            // Change the bar color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;
            // Save the blue barcode image
            generator.Save("code39_blue.png");
        }
    }
}