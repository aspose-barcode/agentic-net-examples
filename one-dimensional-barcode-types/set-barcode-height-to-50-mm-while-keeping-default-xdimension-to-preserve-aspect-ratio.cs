using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

public class Program
{
    public static void Main()
    {
        // Create a barcode generator for Code128 (any 1D symbology can be used)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Set the bar height to 50 millimeters while keeping the default XDimension
            generator.Parameters.Barcode.BarHeight.Millimeters = 50f;

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}