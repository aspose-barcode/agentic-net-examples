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
        // Define file name for the generated QR code
        string qrFile = "qr.png";

        // Create QR code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR error correction level to high
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define image size (300x300 points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the QR code image to file
            generator.Save(qrFile);
        }

        // Verify that the file was created
        if (!File.Exists(qrFile))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // Read and recognize the QR code from the saved image
        using (var reader = new BarCodeReader(qrFile, DecodeType.QR))
        {
            // Use normal quality settings
            reader.QualitySettings = QualitySettings.NormalQuality;

            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine("Detected Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Decoded Text: " + result.CodeText);
                Console.WriteLine("Confidence: " + result.Confidence);
                // Check if confidence indicates strong recognition
                if (result.Confidence == BarCodeConfidence.Strong)
                {
                    Console.WriteLine("QR code read successfully with strong confidence.");
                }
                else
                {
                    Console.WriteLine("QR code read with lower confidence.");
                }
            }

            if (!found)
            {
                Console.WriteLine("No QR code detected in the image.");
            }
        }

        // Clean up generated file (optional)
        try
        {
            File.Delete(qrFile);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}