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
        // Geographic coordinates to embed
        double latitude = 37.7749;
        double longitude = -122.4194;
        string geoUri = $"geo:{latitude},{longitude}";

        // Output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "GeoQR.png");

        // Generate QR Code with the geo URI
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, geoUri))
        {
            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");

        // Verify by reading the generated QR Code
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the QR code image.");
            return;
        }

        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
        }
    }
}