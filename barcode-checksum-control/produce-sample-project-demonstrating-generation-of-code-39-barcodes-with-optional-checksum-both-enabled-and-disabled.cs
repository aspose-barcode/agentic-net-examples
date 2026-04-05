using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Text to encode in the Code 39 barcode
        string codeText = "CODE39";

        // Generate a Code 39 barcode with checksum enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Enable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            // Save the image
            generator.Save("code39_checksum_enabled.png");
        }

        // Generate a Code 39 barcode with checksum disabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Disable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
            // Save the image
            generator.Save("code39_checksum_disabled.png");
        }

        Console.WriteLine("Code 39 barcodes generated with and without checksum.");
    }
}