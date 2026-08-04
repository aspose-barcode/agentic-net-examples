// Title: Barcode decoding with error handling and logging
// Description: Demonstrates generating a Code128 barcode, reading it, checking for decoding validity, and logging results or errors to a file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image and BarCodeReader to decode it, while handling failures by inspecting the decoded result and recording details. Developers often need to validate decoded text, handle missing or unreadable barcodes, and log outcomes for diagnostics.
// Prompt: Handle decoding failures by checking BarCodeReader.IsCodeTextValid and recording error details to a log file.
// Tags: barcode, code128, decoding, error-handling, logging, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, decoding, and error logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image, attempts to decode it, and writes success or error information to a log file.
    /// </summary>
    static void Main()
    {
        // Paths for the generated barcode image and the log file
        string imagePath = "barcode.png";
        string logPath = "decode_log.txt";

        // Ensure previous log is cleared
        if (File.Exists(logPath))
        {
            File.Delete(logPath);
        }

        // Step 1: Generate a sample barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Optional: set visual parameters for better readability
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode to a file
            generator.Save(imagePath);
        }

        // Step 2: Verify the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            File.AppendAllText(logPath, $"Error: Barcode image not found at '{imagePath}'.{Environment.NewLine}");
            return;
        }

        // Step 3: Read the barcode and handle decoding failures
        try
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();

                // No barcodes detected
                if (results.Length == 0)
                {
                    File.AppendAllText(logPath, "Error: No barcode detected in the image." + Environment.NewLine);
                }
                else
                {
                    // Process each detected barcode
                    foreach (var result in results)
                    {
                        // BarCodeResult does not expose IsCodeTextValid; treat non‑empty CodeText as valid
                        bool isValid = !string.IsNullOrEmpty(result.CodeText);
                        if (isValid)
                        {
                            File.AppendAllText(logPath,
                                $"Decoded successfully: Type={result.CodeTypeName}, Text={result.CodeText}{Environment.NewLine}");
                        }
                        else
                        {
                            File.AppendAllText(logPath,
                                $"Decoding failure: Barcode detected but CodeText is empty or null.{Environment.NewLine}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected exceptions during reading
            File.AppendAllText(logPath,
                $"Exception during barcode reading: {ex.GetType().Name} - {ex.Message}{Environment.NewLine}");
        }

        // Output log location to console for quick verification
        Console.WriteLine($"Decoding process completed. Log written to '{logPath}'.");
    }
}