using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
class Program
{
    static void Main()
    {
        // Generate a Code128 barcode with an X-dimension of 1 pixel (ultra‑fine)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the smallest bar width to 1 pixel
            generator.Parameters.Barcode.XDimension.Point = 1f;
            // Save the barcode image
            generator.Save("ultrafine.png");
        }

        // Read the barcode using recognition settings tuned for 1‑pixel wide bars
        using (var reader = new BarCodeReader("ultrafine.png", DecodeType.Code128))
        {
            // Configure XDimension mode to detect small (1‑pixel) elements
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Perform recognition and output the result
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected CodeText: " + result.CodeText);
            }
        }
    }
}