using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1Composite.png");

        // Prepare code text with '|' delimiter separating 1D and 2D parts
        string linearPart = "(01)01234567890128"; // valid GTIN-14
        string twoDPart = "(10)ABCD0123";
        string codeText = $"{linearPart}|{twoDPart}";

        // Generate GS1 Composite barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Read back the barcode and display separated parts
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Decoded GS1 Composite barcode:");
                Console.WriteLine($"1D Type: {result.Extended.GS1CompositeBar.OneDType}");
                Console.WriteLine($"1D CodeText: {result.Extended.GS1CompositeBar.OneDCodeText}");
                Console.WriteLine($"2D Type: {result.Extended.GS1CompositeBar.TwoDType}");
                Console.WriteLine($"2D CodeText: {result.Extended.GS1CompositeBar.TwoDCodeText}");
            }
        }
    }
}