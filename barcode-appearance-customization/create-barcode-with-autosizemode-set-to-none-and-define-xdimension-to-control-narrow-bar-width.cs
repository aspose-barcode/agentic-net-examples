using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Code128 barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the narrow bar width (XDimension) to 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode image to a PNG file
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}