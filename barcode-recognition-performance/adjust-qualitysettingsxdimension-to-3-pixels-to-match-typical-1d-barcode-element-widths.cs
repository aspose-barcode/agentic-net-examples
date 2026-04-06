using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded
            generator.CodeText = "1234567890";

            // Adjust XDimension to 3 pixels (typical width for 1D barcode elements)
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode image to a file
            generator.Save("barcode.png");
        }
    }
}