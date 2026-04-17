using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string outputPath = "code128.png";

        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABCD1234"))
            {
                // Attempt to disable checksum on Code128 (checksum is mandatory)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // Show checksum in human‑readable text (will have no effect if disabled)
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred while disabling checksum for Code128:");
            Console.WriteLine(ex.Message);
        }
    }
}