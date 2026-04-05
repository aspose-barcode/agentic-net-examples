using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Paths for generated images
        string noChecksumPath = Path.Combine(Directory.GetCurrentDirectory(), "code128_no_checksum.png");
        string defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "code128_default.png");

        // Attempt to generate Code128 with checksum disabled (should throw)
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABCD1234"))
            {
                // Disable checksum – Code128 requires a checksum, so an exception is expected
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
                generator.Save(noChecksumPath);
                Console.WriteLine("Generated barcode without checksum (unexpected).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception while disabling checksum for Code128: " + ex.Message);
        }

        // Generate Code128 with default settings (checksum enabled)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABCD1234"))
        {
            // Ensure checksum is shown in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(defaultPath);
            Console.WriteLine("Generated barcode with checksum enabled: " + defaultPath);
        }

        // Read back the generated barcode to display its value and checksum
        using (var reader = new BarCodeReader(defaultPath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Read Code128:");
                Console.WriteLine("  CodeText: " + result.CodeText);
                Console.WriteLine("  Value (without checksum): " + result.Extended.OneD.Value);
                Console.WriteLine("  Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}