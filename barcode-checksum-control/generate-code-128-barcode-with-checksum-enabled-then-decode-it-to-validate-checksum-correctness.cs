using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "code128.png";
        const string codeText = "1234567890";

        // Generate Code128 barcode with checksum enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = codeText;
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Optional: show checksum in human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            generator.Save(filePath);
            Console.WriteLine($"Barcode saved to {filePath}");
        }

        // Decode the barcode and validate checksum
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText : {result.CodeText}");
                Console.WriteLine($"Value (without checksum) : {result.Extended.OneD.Value}");
                Console.WriteLine($"Checksum from barcode : {result.Extended.OneD.CheckSum}");
            }
        }
    }
}