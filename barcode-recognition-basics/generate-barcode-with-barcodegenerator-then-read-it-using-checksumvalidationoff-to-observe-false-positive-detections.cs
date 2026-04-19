using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define the barcode image file path in the current directory
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "ean13.png");

        // Create an EAN13 barcode with a valid checksum (last digit is correct)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Save the generated barcode image
            generator.Save(filePath);
        }

        // Verify that the image was saved before attempting to read it
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode with checksum validation turned OFF (false positive detection)
        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.EAN13))
        {
            // Disable checksum validation
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== ChecksumValidation.Off ===");
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                // Extended data contains the value without checksum and the checksum itself
                Console.WriteLine("BarCode Value (without checksum): " + result.Extended.OneD.Value);
                Console.WriteLine("BarCode Checksum: " + result.Extended.OneD.CheckSum);
            }
        }

        // Optional: read the same barcode with checksum validation ON for comparison
        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.EAN13))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== ChecksumValidation.On ===");
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                Console.WriteLine("BarCode Value (without checksum): " + result.Extended.OneD.Value);
                Console.WriteLine("BarCode Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}