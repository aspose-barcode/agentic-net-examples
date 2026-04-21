using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample HIBC LIC Code128 barcode text (primary data only)
        const string codeText = "A12345";

        // Create a barcode generator for HIBC LIC Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCCode128LIC, codeText))
        {
            // Set foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the barcode image as PNG
            generator.Save("hibc_lic.png");
        }

        Console.WriteLine("HIBC LIC barcode generated: hibc_lic.png");
    }
}