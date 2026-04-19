using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file path for the generated barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "code128.png");

        // Generate a Code128 barcode with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Save the barcode image
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with checksum validation enabled
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine("[Checksum ON] BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("[Checksum ON] BarCode CodeText: " + result.CodeText);
            }

            if (!anyResult)
            {
                Console.WriteLine("[Checksum ON] No barcode detected or checksum validation failed.");
            }
        }

        // Read the barcode with checksum validation disabled for comparison
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Disable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine("[Checksum OFF] BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("[Checksum OFF] BarCode CodeText: " + result.CodeText);
            }

            if (!anyResult)
            {
                Console.WriteLine("[Checksum OFF] No barcode detected.");
            }
        }
    }
}