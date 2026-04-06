using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Common barcode settings
        const string codeText = "123456";
        const float widthValue = 300f;   // example size
        const float heightValue = 150f;  // example size

        // Generate barcode with size in pixels
        using (var generatorPixels = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generatorPixels.CodeText = codeText;
            generatorPixels.Parameters.ImageWidth.Pixels = widthValue;
            generatorPixels.Parameters.ImageHeight.Pixels = heightValue;
            generatorPixels.Save("barcode_pixels.png");
        }

        // Generate barcode with size in millimeters
        using (var generatorMillimeters = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generatorMillimeters.CodeText = codeText;
            generatorMillimeters.Parameters.ImageWidth.Millimeters = widthValue;
            generatorMillimeters.Parameters.ImageHeight.Millimeters = heightValue;
            generatorMillimeters.Save("barcode_millimeters.png");
        }

        // Indicate completion
        Console.WriteLine("Barcodes generated: barcode_pixels.png (pixels), barcode_millimeters.png (millimeters).");
    }
}