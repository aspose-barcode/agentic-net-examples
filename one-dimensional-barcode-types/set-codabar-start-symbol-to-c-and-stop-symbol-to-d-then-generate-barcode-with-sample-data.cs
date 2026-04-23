using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Codabar symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set start symbol to 'C' and stop symbol to 'D'
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Sample data to encode (without start/stop symbols)
            generator.CodeText = "123456";

            // Save the generated barcode image to a file
            generator.Save("codabar.png");
        }

        Console.WriteLine("Codabar barcode generated: codabar.png");
    }
}