// Title: Scheduled barcode reader state export to XML for audit logging
// Description: Demonstrates how to generate barcodes, read them, and periodically export the reader's internal state to an XML log file for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode operations category covering barcode generation, recognition, and state export. It showcases the use of BarcodeGenerator, BarCodeReader, and the ExportToXml method—common tasks for developers who need to create barcodes, decode them, and maintain an audit trail of processing activities. Ideal for scenarios such as batch processing, scheduled jobs, and compliance logging where tracking the reader's state is required.
// Prompt: Create a scheduled job that periodically exports reader state to XML for audit logging of processed barcodes.
// Tags: barcode symbology, generation, recognition, export, xml, audit, scheduled job, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates temporary barcodes, reads them, and periodically
/// exports the reader's state to an XML audit log.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, simulates a scheduled job that reads
    /// each barcode and appends the reader's XML state to a log file.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images.
        string barcodeFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(barcodeFolder);

        // Sample data to encode into barcodes.
        string[] sampleTexts = new[] { "ABC123", "XYZ789", "CODE456" };
        string[] barcodeFiles = new string[sampleTexts.Length];

        // Generate barcode images using Code128 symbology.
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(barcodeFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles[i] = filePath;
        }

        // Path for the XML audit log file.
        string logFile = Path.Combine(barcodeFolder, "AuditLog.xml");

        // Simulated scheduled job: run a few cycles (no infinite loop, no sleep).
        for (int cycle = 1; cycle <= 3; cycle++)
        {
            Console.WriteLine($"Cycle {cycle} - processing barcodes...");

            // Process each generated barcode file.
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                // Create a reader capable of decoding all supported barcode types.
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Perform barcode recognition (results are not used here).
                    var results = reader.ReadBarCodes();

                    // Export the reader's internal state to XML via a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        reader.ExportToXml(ms);
                        string xmlState = Encoding.UTF8.GetString(ms.ToArray());

                        // Append the XML state to the audit log with a simple wrapper.
                        string entry = $"<!-- Cycle {cycle}, File: {Path.GetFileName(file)} -->{Environment.NewLine}{xmlState}{Environment.NewLine}";
                        File.AppendAllText(logFile, entry);
                    }
                }
            }
        }

        Console.WriteLine($"Audit log written to: {logFile}");

        // Cleanup temporary barcode images (optional).
        foreach (string file in barcodeFiles)
        {
            if (File.Exists(file))
                File.Delete(file);
        }

        // Note: the temporary folder itself is left for inspection of the log file.
    }
}