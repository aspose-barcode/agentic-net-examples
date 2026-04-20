using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Simple model for JSON output
    private class PaymentInfo
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
    }

    static void Main(string[] args)
    {
        // Determine input folder (argument or default)
        string inputFolder = args.Length > 0 ? args[0] : "InputQRCodes";
        // Ensure the folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Seed a sample QR code so the example can run end‑to‑end
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "PAYMENT:12345"))
            {
                // Set a moderate error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Save(samplePath);
            }
        }

        // Prepare list to hold extracted payment details
        var payments = new List<PaymentInfo>();

        // Get image files (png and jpg) from the folder
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Simple filter for common image extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                continue;

            // Verify the file still exists
            if (!File.Exists(filePath))
                continue;

            try
            {
                // Use BarCodeReader to decode QR codes
                using (var reader = new BarCodeReader(filePath, DecodeType.QR))
                {
                    // Read all barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        payments.Add(new PaymentInfo
                        {
                            FileName = Path.GetFileName(filePath),
                            CodeText = result.CodeText
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // If a file cannot be processed, write a warning to console and continue
                Console.WriteLine($"Warning: Could not process '{filePath}'. {ex.Message}");
            }
        }

        // Serialize the collected payment info to JSON
        string jsonOutput = JsonSerializer.Serialize(payments, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to output file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "payment_details.json");
        File.WriteAllText(outputPath, jsonOutput);

        // Inform the user where the result is stored
        Console.WriteLine($"Payment details written to: {outputPath}");
    }
}