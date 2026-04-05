using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace BarcodeApiSimulation
{
    class Program
    {
        static void Main()
        {
            // Simulated request payload
            var request = new BarcodeRequest
            {
                CodeText = "123456789012",
                EncodeType = "Code128"
            };

            // Generate barcode image as PNG byte array
            byte[] pngData = GenerateBarcode(request);

            // Save the PNG to a file (for demonstration purposes)
            File.WriteAllBytes("barcode.png", pngData);
        }

        static byte[] GenerateBarcode(BarcodeRequest request)
        {
            // Resolve the encode type from the request string
            BaseEncodeType encodeType = ResolveEncodeType(request.EncodeType);

            // Create a barcode generator with the specified type and code text
            using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
            {
                // Enable checksum generation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Show checksum in the human‑readable text when supported
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the barcode image to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        static BaseEncodeType ResolveEncodeType(string type)
        {
            // Simple mapping from string to EncodeTypes; extend as needed
            switch (type?.Trim().ToUpperInvariant())
            {
                case "CODE128":
                    return EncodeTypes.Code128;
                case "EAN13":
                    return EncodeTypes.EAN13;
                case "QR":
                case "QR_CODE":
                    return EncodeTypes.QR;
                default:
                    throw new ArgumentException($"Unsupported encode type: {type}");
            }
        }
    }

    // DTO representing the incoming request payload
    class BarcodeRequest
    {
        public string CodeText { get; set; }
        public string EncodeType { get; set; }
    }
}