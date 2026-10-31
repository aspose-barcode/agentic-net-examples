using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare a valid MaxiCode codetext for Mode2
        string gs = "\u001d";
        string rs = "\u001e";
        string eot = "\u0004";
        string codetext = $"[)>{rs}01{gs}B1050{gs}056{gs}001{gs}ADDITIONAL DATA{eot}";

        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codetext))
        {
            // Set basic parameters
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Set MaxiCode specific parameters
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 0.5f;
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Auto;

            // Log parameters
            Console.WriteLine("MaxiCode Generation Parameters:");
            Console.WriteLine($"Mode: {generator.Parameters.Barcode.MaxiCode.Mode}");
            Console.WriteLine($"Aspect Ratio: {generator.Parameters.Barcode.MaxiCode.AspectRatio}");
            Console.WriteLine($"Encode Mode: {generator.Parameters.Barcode.MaxiCode.EncodeMode}");

            // Generate and save the barcode
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}