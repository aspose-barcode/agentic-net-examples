// Title: Scheduled Barcode Reader State Export to XML for Audit Logging
// Description: Demonstrates generating sample barcodes, reading them, and exporting the reader's internal state to XML files for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related settings to process barcodes and serialize the reader state. Typical use cases include scheduled jobs that need to log detailed processing information for compliance or debugging. Developers often need to generate barcodes, decode them, and persist reader diagnostics, which this sample illustrates.
// Prompt: Create a scheduled job that periodically exports reader state to XML for audit logging of processed barcodes.
// Tags: barcode generation, barcode recognition, xml export, audit logging, scheduled job, aspose.barcode, code128, qr, pdf417

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates sample barcodes, reads them, and exports the reader state to XML for audit logging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, processes them, and logs audit information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images and audit files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, output file name)
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.png")
        };

        // Generate barcode images and save them to the temporary folder
        foreach (var sample in samples)
        {
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            {
                // Set X-dimension for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                string imagePath = Path.Combine(tempFolder, sample.fileName);
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Prepare audit log file with a header line
        string auditLogPath = Path.Combine(tempFolder, "audit_log.txt");
        File.WriteAllText(auditLogPath, $"Audit Log Started: {DateTime.UtcNow:u}{Environment.NewLine}");

        // Process each generated barcode image
        for (int i = 0; i < samples.Count; i++)
        {
            string imagePath = Path.Combine(tempFolder, samples[i].fileName);
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Image not found: {imagePath}");
                continue;
            }

            // Initialize the barcode reader for all supported types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Configure reader settings
                reader.BarcodeSettings.StripFNC = true;
                reader.QualitySettings.XDimension = XDimensionMode.Small;
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output read results to console
                Console.WriteLine($"Processing {samples[i].fileName}: {results.Length} barcode(s) found.");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }

                // Export the internal reader state to an XML file for audit purposes
                string xmlPath = Path.Combine(tempFolder, $"reader_state_{i + 1}.xml");
                reader.ExportToXml(xmlPath);

                // Append a log entry indicating successful processing and export
                string logEntry = $"[{DateTime.UtcNow:u}] Processed {samples[i].fileName}, exported state to {Path.GetFileName(xmlPath)}{Environment.NewLine}";
                File.AppendAllText(auditLogPath, logEntry);
            }
        }

        // Inform the user where the audit log is saved
        Console.WriteLine("Processing completed. Audit log saved to:");
        Console.WriteLine(auditLogPath);
    }
}