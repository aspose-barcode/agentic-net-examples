using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate an EAN13 barcode with a valid checksum.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save("barcode.png");
        }

        // Read the barcode with checksum validation disabled (false positives may occur).
        using (var reader = new BarCodeReader("barcode.png", DecodeType.EAN13))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("ChecksumValidation.Off - CodeText: " + result.CodeText);
                Console.WriteLine("ChecksumValidation.Off - Value (without checksum): " + result.Extended.OneD.Value);
                Console.WriteLine("ChecksumValidation.Off - Checksum: " + result.Extended.OneD.CheckSum);
            }
        }

        // Read the same barcode with checksum validation enabled for comparison.
        using (var reader = new BarCodeReader("barcode.png", DecodeType.EAN13))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("ChecksumValidation.On - CodeText: " + result.CodeText);
                Console.WriteLine("ChecksumValidation.On - Value (without checksum): " + result.Extended.OneD.Value);
                Console.WriteLine("ChecksumValidation.On - Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}