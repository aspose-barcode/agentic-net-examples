using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Code39 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC-123"))
        {
            // Increase the XDimension to make each bar wider.
            // This reduces visual density, which is helpful for print media.
            // XDimension is a Unit; set its value in points.
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // Adjust as needed

            // Optionally, increase bar height for better readability
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the barcode image as PNG
            generator.Save("code39.png");
        }

        Console.WriteLine("Barcode generated and saved as code39.png");
    }
}