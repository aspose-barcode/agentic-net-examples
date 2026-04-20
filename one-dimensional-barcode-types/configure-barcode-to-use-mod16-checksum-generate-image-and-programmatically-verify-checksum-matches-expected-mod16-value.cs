using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define barcode data (start/stop characters A and B for Codabar)
        string startStop = "AB";
        string data = "123456";
        string fullCodeText = startStop[0] + data + startStop[1];

        // Create Codabar barcode generator with Mod16 checksum
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, fullCodeText))
        {
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            // Set Mod16 checksum mode
            generator.Parameters.Barcode.Codabar.ChecksumMode = Aspose.BarCode.Generation.CodabarChecksumMode.Mod16;
            // Show checksum in human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save barcode image
            string imagePath = "codabar_mod16.png";
            generator.Save(imagePath);
            Console.WriteLine($"Barcode image saved to {Path.GetFullPath(imagePath)}");
        }

        // Verify the checksum using barcode reader
        if (!File.Exists("codabar_mod16.png"))
        {
            Console.WriteLine("Generated image not found.");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader("codabar_mod16.png", DecodeType.Codabar))
        {
            // Ensure checksum validation is performed
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string recognizedCode = result.CodeText;
                string recognizedChecksum = result.Extended.OneD.CheckSum;
                Console.WriteLine($"Recognized CodeText: {recognizedCode}");
                Console.WriteLine($"Recognized Checksum: {recognizedChecksum}");

                // Compute expected Mod16 checksum for the data part
                string expectedChecksum = ComputeMod16Checksum(data);
                Console.WriteLine($"Expected Checksum: {expectedChecksum}");

                if (string.Equals(recognizedChecksum, expectedChecksum, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Checksum verification succeeded.");
                }
                else
                {
                    Console.WriteLine("Checksum verification failed.");
                }
            }
        }
    }

    // Computes Mod16 checksum character for Codabar data (excluding start/stop)
    private static string ComputeMod16Checksum(string data)
    {
        // Mapping of characters to values for Codabar
        Dictionary<char, int> charToValue = new Dictionary<char, int>
        {
            { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 },
            { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { '-', 10 }, { '$', 11 }, { ':', 12 }, { '/', 13 }, { '.', 14 }, { '+', 15 }
        };

        // Reverse mapping from value to character
        char[] valueToChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '$', ':', '/', '.', '+' };

        int sum = 0;
        foreach (char c in data)
        {
            if (!charToValue.TryGetValue(c, out int val))
                throw new ArgumentException($"Invalid character '{c}' for Codabar checksum calculation.");
            sum += val;
        }

        int checksumValue = sum % 16;
        return valueToChar[checksumValue].ToString();
    }
}