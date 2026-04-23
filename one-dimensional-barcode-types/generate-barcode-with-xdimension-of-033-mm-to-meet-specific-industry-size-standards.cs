using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Code128 barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set XDimension to 0.33 millimeters (smallest bar/space width)
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Save the barcode image to a PNG file
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}