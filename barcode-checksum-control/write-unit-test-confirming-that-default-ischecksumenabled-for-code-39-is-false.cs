using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code39 without modifying any settings
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39))
        {
            // Retrieve the default checksum setting
            EnableChecksum checksumSetting = generator.Parameters.Barcode.IsChecksumEnabled;

            // Verify that the default is Disable (No)
            if (checksumSetting == EnableChecksum.No)
            {
                Console.WriteLine("Test Passed: Default IsChecksumEnabled for Code39 is No.");
            }
            else
            {
                Console.WriteLine($"Test Failed: Expected No, but got {checksumSetting}.");
            }
        }
    }
}