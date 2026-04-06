using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string imagePath = "barcode.png";

        // Generate a simple Code128 barcode and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            generator.Save(imagePath);
        }

        // Read the barcode with QualitySettings configured to filter sub‑pixel noise
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Activate UseMinimalXDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Set minimal X dimension to 1 pixel
            reader.QualitySettings.MinimalXDimension = 1;

            // Iterate through detected barcodes and output the code text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
            }
        }
    }
}