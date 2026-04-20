using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string outputPath = Path.Combine(Environment.CurrentDirectory, "codabar.png");

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "1234567890"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Default checksum mode for Codabar is Mod10; explicit setting omitted due to API version differences
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(outputPath);
        }

        if (File.Exists(outputPath))
        {
            using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.Codabar))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("CodeText: " + result.CodeText);
                    Console.WriteLine("Value (without checksum): " + result.Extended.OneD.Value);
                    Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
                }
            }
        }
        else
        {
            Console.WriteLine("Barcode image not found at: " + outputPath);
        }
    }
}