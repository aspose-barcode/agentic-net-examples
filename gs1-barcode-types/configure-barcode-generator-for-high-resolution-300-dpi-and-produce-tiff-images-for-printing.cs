using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set high resolution (300 DPI)
            generator.Parameters.Resolution = 300f;

            // Optionally set colors (black bars on white background)
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a CMYK TIFF suitable for printing
            generator.Save("barcode.tif");
        }

        Console.WriteLine("Barcode generated and saved as barcode.tif at 300 DPI.");
    }
}