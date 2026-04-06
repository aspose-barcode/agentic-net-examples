using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a barcode image (EAN13 includes a checksum)
        string filePath = "barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(filePath);
        }

        // Read the barcode with checksum validation enabled for all symbologies
        using (var reader = new BarCodeReader(filePath, DecodeType.EAN13))
        {
            // Enable checksum validation (On) for both obligatory and optional checksum symbologies
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