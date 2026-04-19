using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Choose a symbology that does not support checksum (e.g., QR code)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test"))
        {
            try
            {
                // Attempt to enable checksum – this should fail for QR
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Trigger generation (saving forces processing)
                generator.Save("output.png");
                Console.WriteLine("Barcode generated successfully (unexpected).");
            }
            catch (Exception ex)
            {
                // Expected path: an exception indicating checksum is not supported
                Console.WriteLine("Expected exception caught:");
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
        }
    }
}