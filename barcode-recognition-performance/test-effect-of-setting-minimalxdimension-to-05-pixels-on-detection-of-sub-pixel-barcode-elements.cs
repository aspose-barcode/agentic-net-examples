using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode with a very small XDimension (0.5 pixels)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123456";
            // Set XDimension to 0.5 pixels (sub‑pixel width)
            generator.Parameters.Barcode.XDimension.Point = 0.5f;
            // Save the generated image
            generator.Save("subpixel_barcode.png");
        }

        // Read the barcode using MinimalXDimension = 0.5 pixels
        using (var reader = new BarCodeReader("subpixel_barcode.png", DecodeType.Code128))
        {
            // Configure recognition to use the MinimalXDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 0.5f;

            // Attempt to read barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Symbology: {result.CodeTypeName}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }
    }
}