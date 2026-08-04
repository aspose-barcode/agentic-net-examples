// Title: Mailmark Barcode Decoding with Failure Logging
// Description: Demonstrates generating Mailmark barcodes, attempting to decode them, and logging any decoding failures including the image path and exception details.
// Category-Description: Shows how to use Aspose.BarCode's ComplexBarcodeGenerator to create Mailmark symbols and BarCodeReader to decode them. Typical use cases include batch processing of Mailmark images, error handling, and troubleshooting when decoding fails. Developers often need to capture raw image paths and exception information for diagnostics, which this example logs to a text file.
// Prompt: Implement logging of decoding failures, capturing raw image path and exception details for Mailmark troubleshooting.
// Tags: mailmark, barcode, decoding, logging, aspose.barcode, complexbarcode, barcodereader, image, exception handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Mailmark barcodes, attempts to decode them,
/// and logs any decoding failures for troubleshooting purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, creates a non‑barcode image,
    /// scans all PNG files in the working folder, and logs success or failure of each decode attempt.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare working folder and log file
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folderPath);

        string logPath = Path.Combine(folderPath, "decode_log.txt");
        if (!File.Exists(logPath))
        {
            File.WriteAllText(logPath, $"Decode Log - {DateTime.Now}{Environment.NewLine}");
        }

        // --------------------------------------------------------------------
        // Generate sample Mailmark barcode images (2 records)
        // --------------------------------------------------------------------
        for (int i = 0; i < 2; i++)
        {
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state Mailmark
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,          // vary ItemID to keep records unique
                DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
            };

            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                string imagePath = Path.Combine(folderPath, $"mailmark_{i + 1}.png");
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Create a non‑barcode image to force a decoding failure
        // --------------------------------------------------------------------
        string nonBarcodePath = Path.Combine(folderPath, "nonbarcode.png");
        using (var bitmap = new Bitmap(200, 200))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
            }
            bitmap.Save(nonBarcodePath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Scan folder for PNG images and attempt to decode each one
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imageFile in imageFiles)
        {
            // Defensive check: ensure the file still exists
            if (!File.Exists(imageFile))
            {
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(imageFile, DecodeType.Mailmark))
                {
                    var results = reader.ReadBarCodes();
                    bool anyFound = false;

                    foreach (var result in results)
                    {
                        anyFound = true;
                        Console.WriteLine($"SUCCESS: {Path.GetFileName(imageFile)} -> {result.CodeText}");
                    }

                    if (!anyFound)
                    {
                        string msg = $"No Mailmark barcode detected in '{imageFile}'.";
                        Console.WriteLine($"INFO: {msg}");
                        File.AppendAllText(logPath, $"{DateTime.Now}: {msg}{Environment.NewLine}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log decoding failure with image path and exception details
                string error = $"Failed to decode '{imageFile}'. Exception: {ex.GetType().Name} - {ex.Message}";
                Console.WriteLine($"ERROR: {error}");
                File.AppendAllText(logPath, $"{DateTime.Now}: {error}{Environment.NewLine}");
            }
        }

        Console.WriteLine("Decoding process completed. See log file for details.");
    }
}