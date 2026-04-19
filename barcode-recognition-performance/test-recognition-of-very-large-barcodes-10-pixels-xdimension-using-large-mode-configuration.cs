using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "largeBarcode.png";
        const string codeText = "123456789012";

        // Generate a Code128 barcode with a large XDimension (>10 pixels)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set XDimension to 12 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 12f;
            // Optionally set bar height
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;
            // Save the barcode image
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Recognize the barcode using Large XDimension mode
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure recognition to use Large mode for XDimension
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Read barcodes from the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
                Console.WriteLine($"Recognition Confidence: {result.Confidence}");
            }
        }
    }
}