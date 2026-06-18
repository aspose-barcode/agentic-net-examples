using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code39 barcodes with and without checksum using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images: one with checksum enabled and one with checksum disabled.
    /// </summary>
    static void Main()
    {
        // Sample text to encode in the Code39 barcode.
        const string codeText = "CODE39";

        // ------------------------------------------------------------
        // Generate a Code39 barcode with checksum enabled.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Ensure the checksum digit is displayed in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode image to a PNG file.
            generator.Save("code39_checksum_enabled.png");

            // Inform the user that the barcode has been created.
            Console.WriteLine("Generated barcode with checksum enabled: code39_checksum_enabled.png");
        }

        // ------------------------------------------------------------
        // Generate a Code39 barcode with checksum disabled.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Disable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Save the generated barcode image to a PNG file.
            generator.Save("code39_checksum_disabled.png");

            // Inform the user that the barcode has been created.
            Console.WriteLine("Generated barcode with checksum disabled: code39_checksum_disabled.png");
        }
    }
}