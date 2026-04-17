using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeQrUnitTest
{
    class Program
    {
        static void Main()
        {
            // Define the QR code text and output file path
            string qrText = "https://www.example.com";
            string outputFile = Path.Combine(Environment.CurrentDirectory, "qr_test.png");

            // Generate QR code image
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = qrText;
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                // Save the generated QR code to a PNG file
                generator.Save(outputFile);
            }

            // Validate the generated QR code by reading it back
            if (!File.Exists(outputFile))
            {
                Console.WriteLine("Failed to generate QR code image.");
                return;
            }

            bool validationPassed = false;
            using (BarCodeReader reader = new BarCodeReader(outputFile, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result != null && result.CodeText == qrText)
                    {
                        validationPassed = true;
                        break;
                    }
                }
            }

            // Output validation result
            Console.WriteLine(validationPassed
                ? "QR code generation and validation succeeded."
                : "QR code validation failed.");
        }
    }
}