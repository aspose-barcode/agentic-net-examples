using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Entry point for the QR code extraction utility.
/// Scans a folder for image files, extracts QR code text, and writes results to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO to hold extracted QR code data.
    /// </summary>
    private class PaymentInfo
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Main method processes command‑line arguments, reads images, extracts QR codes, and writes JSON output.
    /// </summary>
    /// <param name="args">
    /// args[0] – optional input folder path (default: "QRCodes").
    /// args[1] – optional output JSON file path (default: "paymentDetails.json").
    /// </param>
    static void Main(string[] args)
    {
        // Resolve input folder: use first argument if provided, otherwise default.
        string inputFolder = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? args[0]
            : "QRCodes";

        // Resolve output JSON file path: use second argument if provided, otherwise default.
        string outputJsonPath = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
            ? args[1]
            : "paymentDetails.json";

        // Verify that the input folder exists before proceeding.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Define supported image file extensions.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        var paymentList = new List<PaymentInfo>();

        // Enumerate all files in the input folder.
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            // Skip files that do not have a supported image extension.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Defensive check: ensure the file still exists.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            try
            {
                // Load the image using Aspose.Drawing.Bitmap (IDisposable).
                using (var bitmap = new Bitmap(filePath))
                {
                    // Initialize a barcode reader configured for QR codes only.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.QR))
                    {
                        // Iterate over all detected QR codes in the image.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Add a new record containing the file name and extracted QR code text.
                            paymentList.Add(new PaymentInfo
                            {
                                FileName = Path.GetFileName(filePath),
                                CodeText = result.CodeText
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing the current file.
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        // Serialize the collected payment information to a formatted JSON string.
        try
        {
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(paymentList, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Successfully wrote {paymentList.Count} record(s) to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during JSON serialization or file writing.
            Console.WriteLine($"Failed to write JSON output: {ex.Message}");
        }
    }
}