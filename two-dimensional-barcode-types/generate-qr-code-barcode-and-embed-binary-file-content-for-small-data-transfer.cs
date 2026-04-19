using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Determine binary data source: file path from arguments or sample bytes
            byte[] binaryData;
            if (args.Length > 0 && File.Exists(args[0]))
            {
                binaryData = File.ReadAllBytes(args[0]);
            }
            else
            {
                // Sample binary payload for demonstration
                binaryData = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
            }

            string outputFile = "qr_binary.png";

            // Create QR Code generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Use high error correction level for better data recovery
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Embed binary data directly into the QR code
                generator.SetCodeText(binaryData);

                // Hide human‑readable text (optional)
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Generate the barcode image and save it
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(outputFile, ImageFormat.Png);
                }
            }

            Console.WriteLine($"QR code saved to {outputFile}");
        }
    }
}