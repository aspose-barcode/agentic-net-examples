using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace ChecksumHelperDemo
{
    // Helper class to manage checksum settings of a BarcodeGenerator
    public static class ChecksumHelper
    {
        // Enables checksum calculation for the provided generator
        public static void EnableChecksum(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
        }

        // Disables checksum calculation for the provided generator
        public static void DisableChecksum(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
        }

        // Returns true if checksum is enabled (Yes), false otherwise
        public static bool IsChecksumEnabled(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return generator.Parameters.Barcode.IsChecksumEnabled == Aspose.BarCode.Generation.EnableChecksum.Yes;
        }
    }

    class Program
    {
        static void Main()
        {
            const string outputPath = "code39_checksum.png";

            // Create a barcode generator for Code39 (supports checksum)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
            {
                // Enable checksum using the helper
                ChecksumHelper.EnableChecksum(generator);

                // Optionally show the checksum digit in the human‑readable text
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the barcode image
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to {outputPath}");
                Console.WriteLine($"Checksum enabled: {ChecksumHelper.IsChecksumEnabled(generator)}");
            }

            // Decode the barcode and validate checksum
            using (var reader = new BarCodeReader(outputPath, DecodeType.Code39))
            {
                // Enable checksum validation during recognition
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    Console.WriteLine($"Decoded Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"Decoded Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}