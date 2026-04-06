using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "code11.png";

        // Generate a Code 11 barcode with checksum enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(filePath);
        }

        // Read the barcode with checksum validation turned on
        using (var reader = new BarCodeReader(filePath, DecodeType.Code11))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                Console.WriteLine("BarCode Value: " + result.Extended.OneD.Value);
                Console.WriteLine("BarCode Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}