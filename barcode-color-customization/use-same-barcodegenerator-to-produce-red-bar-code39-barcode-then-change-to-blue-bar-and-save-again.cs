using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a Code39 barcode generator with initial text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Set the bar color to red and save the image
            generator.Parameters.Barcode.BarColor = Color.Red;
            generator.Save("code39_red.png");

            // Change the bar color to blue and save the image again
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Save("code39_blue.png");
        }
    }
}