using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare a Code128 barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABCD1234"))
        {
            // Disable checksum generation for Code128.
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;

            try
            {
                // Attempt to generate the barcode. Expect an exception because
                // Code128 requires a checksum and disabling it is invalid.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // If no exception was thrown, the test failed.
                Console.WriteLine("Test Failed: No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Expected path: an exception should be thrown.
                Console.WriteLine("Test Passed: Caught expected exception.");
                Console.WriteLine("Exception Type: " + ex.GetType().FullName);
                Console.WriteLine("Message: " + ex.Message);
            }
        }
    }
}