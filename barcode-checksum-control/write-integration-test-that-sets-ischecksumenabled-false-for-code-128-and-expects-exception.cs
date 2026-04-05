using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        try
        {
            // Create a Code128 generator with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                // Disable checksum generation
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;

                // Attempt to generate the barcode image – an exception is expected
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save to a temporary file (won't be reached if exception occurs)
                    bitmap.Save("code128.png");
                }
            }

            // If we reach this point, no exception was thrown – test failed
            Console.WriteLine("Test failed: No exception was thrown.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception should be caught
            Console.WriteLine($"Test passed: Caught expected exception: {ex.GetType().Name} - {ex.Message}");
        }
    }
}