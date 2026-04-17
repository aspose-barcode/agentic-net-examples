using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "barcode.png";

        // Create a barcode (EAN13) that includes a checksum
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(imagePath);
        }

        // Ensure the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read the barcode with checksum validation turned on for all symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Enable checksum validation (both obligatory and optional checksum symbologies)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Value (without checksum): {result.Extended.OneD.Value}");
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
            }
        }
    }
}