using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string logFile = "checksum_error_log.txt";

        // Attempt to generate an EAN13 barcode with checksum disabled.
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
            {
                // Disable checksum for a barcode that requires it.
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;

                // Save the barcode image (if generation succeeds).
                generator.Save("ean13_no_checksum.png");
                Console.WriteLine("Barcode generated successfully.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception message to a file.
            File.AppendAllText(logFile, $"[{DateTime.Now}] {ex.Message}{Environment.NewLine}");
            Console.WriteLine($"Exception caught and logged to {logFile}");
        }
    }
}