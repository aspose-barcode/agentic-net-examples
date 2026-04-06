using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
class Program
{
    static void Main()
    {
        // Generate a Code39 barcode and enable checksum generation
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Enable checksum for optional symbology
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            generator.Save("code39.png");
        }

        // Read the barcode with checksum validation turned on
        using (var reader = new BarCodeReader("code39.png", DecodeType.Code39))
        {
            // Configure checksum validation for recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("CodeText: " + result.CodeText);
                Console.WriteLine("Value: " + result.Extended.OneD.Value);
                Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}