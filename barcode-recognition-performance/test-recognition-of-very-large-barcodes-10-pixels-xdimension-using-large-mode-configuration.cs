using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
class Program
{
    static void Main()
    {
        // Create a barcode with a large XDimension (>10 pixels)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LARGE_XDIMENSION"))
        {
            // Set XDimension to 12 pixels
            generator.Parameters.Barcode.XDimension.Point = 12f;
            // Optionally increase bar height for better visibility
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            // Save the generated barcode image
            generator.Save("largeBarcode.png");
        }

        // Read the barcode using Large XDimension mode
        using (var reader = new BarCodeReader("largeBarcode.png", DecodeType.Code128))
        {
            // Configure recognition to use Large mode
            reader.QualitySettings.XDimension = XDimensionMode.Large;
            // Optionally set a high-performance preset
            reader.QualitySettings = QualitySettings.HighPerformance;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }
    }
}