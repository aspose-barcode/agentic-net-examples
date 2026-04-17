using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image that contains barcodes.
        string imagePath = "sample.png";

        // Verify that the image file exists.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader with a set of common decode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128, DecodeType.Code39, DecodeType.QR, DecodeType.DataMatrix, DecodeType.Pdf417, DecodeType.Aztec))
        {
            // Configure quality settings for detecting large barcodes in high‑resolution scans.
            // Use the Large XDimension mode and set a minimal XDimension of 6 pixels.
            reader.QualitySettings.XDimension = XDimensionMode.Large;
            reader.QualitySettings.MinimalXDimension = 6;

            // Read all barcodes from the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}