using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Determine whether to force checksum visibility.
        bool forceChecksumVisibility = false;
        foreach (string arg in args)
        {
            if (arg.Equals("--show-checksum", StringComparison.OrdinalIgnoreCase))
            {
                forceChecksumVisibility = true;
                break;
            }
        }

        // Create a barcode generator for Code128.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode.
            generator.CodeText = "123456";

            // Ensure checksum is calculated (optional for Code128, but set for completeness).
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

            // Force checksum digit to appear in the human‑readable text if the flag is set.
            if (forceChecksumVisibility)
            {
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            }

            // Save the barcode image.
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated successfully. Checksum visibility: " + forceChecksumVisibility);
    }
}