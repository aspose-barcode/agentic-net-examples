using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace ChecksumControlDemo
{
    public static class ChecksumHelper
    {
        public static void SetChecksumEnabled(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
        }

        public static void SetChecksumDisabled(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
        }

        public static bool IsChecksumEnabled(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return generator.Parameters.Barcode.IsChecksumEnabled == Aspose.BarCode.Generation.EnableChecksum.Yes;
        }

        public static void EnableChecksumValidation(BarCodeReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
        }

        public static void DisableChecksumValidation(BarCodeReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
        }

        public static bool IsChecksumValidationEnabled(BarCodeReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            return reader.BarcodeSettings.ChecksumValidation == ChecksumValidation.On;
        }
    }

    class Program
    {
        static void Main()
        {
            const string filePath = "barcode.png";
            const string codeText = "123456";

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                ChecksumHelper.SetChecksumEnabled(generator);
                Console.WriteLine($"Checksum enabled for generation: {ChecksumHelper.IsChecksumEnabled(generator)}");
                generator.Save(filePath);
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                ChecksumHelper.EnableChecksumValidation(reader);
                Console.WriteLine($"Checksum validation enabled for recognition: {ChecksumHelper.IsChecksumValidationEnabled(reader)}");

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                    Console.WriteLine($"Extracted Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}