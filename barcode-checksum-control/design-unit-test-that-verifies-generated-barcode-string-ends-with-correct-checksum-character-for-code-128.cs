using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "code128.png";
        const string data = "ABC123";

        // Generate Code128 barcode with checksum enabled and displayed
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, data))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(filePath);
        }

        // Read the generated barcode and verify checksum character
        bool testPassed = false;
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Full code text includes checksum because ChecksumAlwaysShow = true
                string fullCodeText = result.CodeText;

                // Extract checksum character from extended parameters (OneD)
                string checksumChar = result.Extended.OneD.CheckSum;

                // Verify that the full code text ends with the checksum character
                if (!string.IsNullOrEmpty(fullCodeText) && !string.IsNullOrEmpty(checksumChar) &&
                    fullCodeText.EndsWith(checksumChar, StringComparison.Ordinal))
                {
                    testPassed = true;
                    Console.WriteLine("Test Passed: Barcode ends with correct checksum character.");
                }
                else
                {
                    Console.WriteLine("Test Failed: Checksum character mismatch.");
                    Console.WriteLine($"Full CodeText: {fullCodeText}");
                    Console.WriteLine($"Expected Checksum: {checksumChar}");
                }
            }
        }

        if (!testPassed)
        {
            Console.WriteLine("No barcode was read or verification failed.");
        }
    }
}