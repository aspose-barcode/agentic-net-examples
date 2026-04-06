using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "code128.png";
        // Generate Code128 barcode with checksum enabled and displayed
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(filePath);
        }

        // Read the barcode and validate checksum
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("CodeText: " + result.CodeText);
                // For 1D barcodes the checksum is available via Extended.OneD.CheckSum
                string checksum = result.Extended.OneD.CheckSum;
                if (string.IsNullOrEmpty(checksum))
                {
                    Console.WriteLine("Checksum validation failed or not applicable.");
                }
                else
                {
                    Console.WriteLine("Checksum: " + checksum);
                    Console.WriteLine("Checksum validation succeeded.");
                }
            }
        }
    }
}