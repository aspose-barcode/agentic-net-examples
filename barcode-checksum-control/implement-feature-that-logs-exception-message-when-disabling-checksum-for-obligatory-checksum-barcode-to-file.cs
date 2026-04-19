using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string outputImage = "ean13.png";
        // Path for the log file
        const string logFile = "checksum_error.log";

        // Create a barcode generator for a symbology that requires a checksum (EAN13)
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            try
            {
                // Attempt to disable the checksum (should raise an exception for mandatory checksum)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Save the barcode image (will not be reached if exception occurs)
                generator.Save(outputImage);
                Console.WriteLine($"Barcode saved to {outputImage}");
            }
            catch (Exception ex)
            {
                // Log the exception message to a file
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {ex.Message}{Environment.NewLine}";
                File.AppendAllText(logFile, logEntry);
                Console.WriteLine($"Exception logged to {logFile}");
            }
        }
    }
}