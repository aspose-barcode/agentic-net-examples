using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        try
        {
            // Create a barcode generator for Codabar (which does not support checksum)
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123B"))
            {
                // Enable checksum – this should cause an exception because Codabar has no checksum support
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Attempt to generate and save the barcode (triggers validation)
                generator.Save("codabar.png");

                Console.WriteLine("Barcode generated successfully (unexpected).");
            }
        }
        catch (Exception ex)
        {
            // Expected path: an exception indicating checksum cannot be enabled for this symbology
            Console.WriteLine($"Expected exception caught: {ex.Message}");
        }
    }
}