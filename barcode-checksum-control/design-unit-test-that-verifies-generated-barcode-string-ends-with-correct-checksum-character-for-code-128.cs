using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample data to encode
        const string data = "ABC123";

        // Generate Code128 barcode with checksum enabled and visible
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, data))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save barcode to a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Recognize the barcode from the stream
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // The checksum character is provided in the extended parameters
                        string checksumChar = result.Extended.OneD.CheckSum;

                        // Combine the original code text with the checksum character
                        string fullCode = result.CodeText + checksumChar;

                        // Verify that the full code ends with the checksum character
                        bool endsWithChecksum = fullCode.EndsWith(checksumChar, StringComparison.Ordinal);

                        Console.WriteLine(endsWithChecksum ? "Test Passed" : "Test Failed");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"Checksum: {checksumChar}");
                        Console.WriteLine($"Full Code: {fullCode}");
                    }
                }
            }
        }
    }
}