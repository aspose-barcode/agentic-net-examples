using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.svg");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Optional: set module size
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"QR Code saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving SVG: {ex.Message}");
            }
        }
    }
}