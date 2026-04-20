using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Sample GTIN-14 value (14 digits)
        string gtin14 = "01234567890123";

        // Create the barcode generator for ITF-14
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, gtin14))
        {
            // Set a frame border around the barcode
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

            // Apply custom frame thickness (5 points)
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f;

            // Optional: set barcode colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as PNG
            generator.Save("itf14.png");
        }
    }
}