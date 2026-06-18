using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and handling errors when disabling checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, attempts to disable checksum (which causes an exception),
    /// and logs any exception to a file.
    /// </summary>
    static void Main()
    {
        // Define file paths for the output image and error log.
        string imagePath = "code128.png";
        string logPath = "error.log";

        // Initialize a BarcodeGenerator for Code128 with the data "123456".
        // Note: Checksum is mandatory for Code128; disabling it will trigger an exception.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Attempt to disable the checksum (expected to cause an exception on Save).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            try
            {
                // Save the generated barcode image to the specified path.
                generator.Save(imagePath);
                Console.WriteLine("Barcode generated successfully.");
            }
            catch (Exception ex)
            {
                // Build a log entry with timestamp and exception message.
                string logEntry = $"{DateTime.Now:u} - {ex.Message}{Environment.NewLine}";
                // Append the log entry to the error log file.
                File.AppendAllText(logPath, logEntry);
                Console.WriteLine("Exception occurred and was logged to file.");
            }
        }
    }
}