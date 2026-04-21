using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 (any symbology can be used)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Enable auto‑size based on content.
            // According to Aspose.BarCode rules, setting BarHeight to 0 is invalid.
            // Use AutoSizeMode.Interpolation and do not set BarHeight.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the generated barcode image to a file (default PNG format)
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated successfully.");
    }
}