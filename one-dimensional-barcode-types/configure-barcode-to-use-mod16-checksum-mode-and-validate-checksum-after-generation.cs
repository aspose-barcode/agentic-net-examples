using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "codabar.png";
        // Generate Codabar barcode with Mod16 checksum mode
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            // Set checksum algorithm to Mod16 (default, but set explicitly)
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;
            // Save the barcode image
            generator.Save(filePath);
        }

        // Read the barcode and validate checksum
        using (var reader = new BarCodeReader(filePath, DecodeType.Codabar))
        {
            // Ensure checksum validation is performed
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("CodeText: " + result.CodeText);
                Console.WriteLine("Value (without checksum): " + result.Extended.OneD.Value);
                Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}