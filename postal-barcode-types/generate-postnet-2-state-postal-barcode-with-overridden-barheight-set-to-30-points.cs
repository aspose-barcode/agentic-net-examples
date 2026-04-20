using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Postnet barcode generator with sample ZIP code
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Ensure AutoSizeMode is None so BarHeight is applied
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Override the bar height to 30 points
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the generated barcode image
            generator.Save("postnet.png");
        }

        Console.WriteLine("Postnet barcode generated: postnet.png");
    }
}