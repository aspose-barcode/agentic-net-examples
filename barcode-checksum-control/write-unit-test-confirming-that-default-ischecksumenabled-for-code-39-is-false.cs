using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Code 39 barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Retrieve the default checksum setting.
            var checksumSetting = generator.Parameters.Barcode.IsChecksumEnabled;

            // Verify that the default is Disable (EnableChecksum.No).
            if (checksumSetting == EnableChecksum.No)
            {
                Console.WriteLine("Test Passed: Default IsChecksumEnabled for Code39 is No.");
            }
            else
            {
                Console.WriteLine($"Test Failed: Expected EnableChecksum.No, but got {checksumSetting}.");
            }
        }
    }
}