using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Optionally show the checksum digit in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode as a JPEG image
            generator.Save("code128.jpg");
        }
    }
}