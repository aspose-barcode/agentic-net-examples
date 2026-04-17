using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = "qr.png";

        // Ensure any existing file is removed
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Desired QR version (e.g., Version 5)
        QRVersion desiredVersion = QRVersion.Version05;

        // Create QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode
            generator.CodeText = "Aspose QR Code Example";

            // Set QR version explicitly
            generator.Parameters.Barcode.QR.Version = desiredVersion;

            // Optional: set error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code image
            generator.Save(outputPath);
        }

        // Verify the generated QR code version using BarCodeReader
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            bool versionMatched = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the recognized QR version
                QRVersion recognizedVersion = result.Extended.QR.Version;

                Console.WriteLine($"Recognized QR Version: {recognizedVersion}");
                Console.WriteLine($"Desired QR Version   : {desiredVersion}");

                if (recognizedVersion == desiredVersion)
                {
                    versionMatched = true;
                    Console.WriteLine("The generated QR code complies with the specified version.");
                }
                else
                {
                    Console.WriteLine("The generated QR code does NOT match the specified version.");
                }
            }

            if (!versionMatched)
            {
                Console.WriteLine("No QR code was recognized or version mismatch occurred.");
            }
        }
    }
}