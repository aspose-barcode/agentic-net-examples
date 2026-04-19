using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image containing ultra‑fine barcodes
        string imagePath = "sample.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a barcode reader for the desired symbologies (example: Code128 and QR)
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128, DecodeType.QR))
        {
            // Configure recognition to treat the minimal element size as 1 pixel
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f; // 1 pixel

            // Read and output all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine();
            }
        }
    }
}