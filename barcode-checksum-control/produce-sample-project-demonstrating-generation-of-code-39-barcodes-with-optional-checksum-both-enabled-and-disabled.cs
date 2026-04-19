using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample Code 39 text
        string codeText = "CODE39";

        // Generate Code 39 barcode with checksum enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Enable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Show the checksum digit in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save("code39_checksum_enabled.png");
            Console.WriteLine("Generated Code 39 with checksum enabled: code39_checksum_enabled.png");
        }

        // Generate Code 39 barcode with checksum disabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Disable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            generator.Save("code39_checksum_disabled.png");
            Console.WriteLine("Generated Code 39 with checksum disabled: code39_checksum_disabled.png");
        }
    }
}