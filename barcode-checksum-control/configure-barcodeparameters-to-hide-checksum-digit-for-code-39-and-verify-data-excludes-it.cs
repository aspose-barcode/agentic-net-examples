using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string codeText = "ABC123";
        const string fileName = "code39.png";

        // Generate Code39 barcode without checksum (checksum digit hidden)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Disable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
            generator.Save(fileName);
        }

        // Read the generated barcode and verify that the value excludes checksum
        using (var reader = new BarCodeReader(fileName, DecodeType.Code39))
        {
            // Disable checksum validation to avoid false failures
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Value without checksum: {result.Extended.OneD.Value}");

                // Verify that the value matches the original code text (no checksum present)
                if (result.Extended.OneD.Value == codeText)
                {
                    Console.WriteLine("Verification passed: checksum digit is not present.");
                }
                else
                {
                    Console.WriteLine("Verification failed: checksum digit detected.");
                }
            }
        }
    }
}