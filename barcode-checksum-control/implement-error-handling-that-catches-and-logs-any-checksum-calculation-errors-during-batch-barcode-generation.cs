// Title: Batch Barcode Generation with Checksum Error Handling
// Description: Demonstrates generating multiple barcodes in a batch while handling checksum calculation errors and logging them.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set checksum options, and manage errors during bulk barcode creation. Typical use cases include automated label production, inventory systems, and batch processing where different symbologies and checksum requirements are involved. Developers often need to catch exceptions from invalid data or checksum failures and log them for later review.
// Prompt: Implement error handling that catches and logs any checksum calculation errors during batch barcode generation.
// Tags: barcode, checksum, batch, generation, error-handling, aspose.barcode, code128, code39, qr, interleaved2of5, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a batch of barcodes with varying symbologies and checksum settings,
/// handling and logging any errors that occur during generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary output folder,
    /// defines batch data, generates barcodes, and logs any checksum errors.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Path to the error log file within the output folder
        string logPath = Path.Combine(outputFolder, "error_log.txt");

        // Sample batch data: each entry contains the symbology, the code text, and the checksum setting
        var batchData = new List<(BaseEncodeType EncodeType, string CodeText, EnableChecksum ChecksumSetting)>
        {
            // Code128 requires checksum; this will succeed
            (EncodeTypes.Code128, "ABC123", EnableChecksum.Yes),

            // Code39 with checksum enabled but an invalid code text for checksum (contains unsupported character)
            (EncodeTypes.Code39, "INVALID*CHAR", EnableChecksum.Yes),

            // Code39 with checksum disabled (should succeed)
            (EncodeTypes.Code39, "VALID123", EnableChecksum.No),

            // QR code (checksum not applicable, setting ignored)
            (EncodeTypes.QR, "https://example.com", EnableChecksum.Yes),

            // Interleaved2of5 with checksum enabled but odd number of digits (may cause checksum error)
            (EncodeTypes.Interleaved2of5, "12345", EnableChecksum.Yes)
        };

        // Process each barcode definition in the batch
        foreach (var (encodeType, codeText, checksumSetting) in batchData)
        {
            // Build a safe file name for the output image
            string fileName = $"{encodeType}_{codeText}.png".Replace("*", "_").Replace(":", "_");
            string filePath = Path.Combine(outputFolder, fileName);

            try
            {
                // Initialize the barcode generator with the specified symbology and text
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Apply the checksum setting for the current barcode
                    generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

                    // Generate the barcode image and save it as PNG
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        bitmap.Save(filePath, ImageFormat.Png);
                    }

                    Console.WriteLine($"Generated barcode: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Compose an error message that includes the symbology and text
                string message = $"Error generating barcode for symbology {encodeType}, text '{codeText}': {ex.Message}";
                Console.WriteLine(message);

                // Append the error details with a timestamp to the log file
                File.AppendAllText(logPath, $"{DateTime.Now:u} - {message}{Environment.NewLine}");
            }
        }

        // Inform the user that batch processing is complete and where to find results
        Console.WriteLine($"Batch processing completed. Output folder: {outputFolder}");
        Console.WriteLine($"If any errors occurred, see log file: {logPath}");
    }
}