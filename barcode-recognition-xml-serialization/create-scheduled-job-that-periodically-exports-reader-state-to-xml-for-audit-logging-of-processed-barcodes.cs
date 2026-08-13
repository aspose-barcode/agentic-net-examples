// Title: Scheduled barcode processing with XML audit export
// Description: Demonstrates generating Code128 barcodes, reading them, and exporting the reader state to XML for audit logging.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and the ExportToXml method for persisting reader state. Developers often need such patterns for batch processing, scheduled jobs, and compliance auditing where a detailed record of barcode scans is required.
// Prompt: Create a scheduled job that periodically exports reader state to XML for audit logging of processed barcodes.
// Tags: code128, generation, recognition, xml, audit, export

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcodes, reads them, and exports the reader state to XML for audit purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them, logs results, and exports reader state.
    /// </summary>
    static void Main()
    {
        // Prepare directory for generated barcode images
        string imageDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(imageDir);

        // Initialize human‑readable audit log file
        string auditLogPath = Path.Combine(Directory.GetCurrentDirectory(), "audit.log");
        File.WriteAllText(auditLogPath, $"Audit Log - Started at {DateTime.Now}{Environment.NewLine}");

        // Sample data to encode into barcodes
        string[] sampleTexts = { "ABC123", "987XYZ", "Test001" };

        // Process each sample text
        foreach (string text in sampleTexts)
        {
            // Define paths for the barcode image and the corresponding XML export
            string imagePath = Path.Combine(imageDir, $"{text}.png");
            string xmlPath = Path.Combine(imageDir, $"{text}_reader.xml");

            // ---------- Barcode Generation ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Optional: customize generation parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Save the generated barcode as a PNG image
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Verify that the image file was created successfully
            if (!File.Exists(imagePath))
            {
                File.AppendAllText(auditLogPath, $"Image not found: {imagePath}{Environment.NewLine}");
                continue;
            }

            // ---------- Barcode Reading & Audit ----------
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Read all barcodes present in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Log each detection result to the audit file
                    string logEntry = $"[{DateTime.Now}] Image: {Path.GetFileName(imagePath)}, " +
                                      $"Type: {result.CodeType}, Text: {result.CodeText}";
                    File.AppendAllText(auditLogPath, logEntry + Environment.NewLine);
                }

                // Export the reader's internal state to XML for detailed audit logging
                try
                {
                    reader.ExportToXml(xmlPath);
                    File.AppendAllText(auditLogPath, $"Exported reader state to XML: {xmlPath}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    File.AppendAllText(auditLogPath, $"Failed to export XML for {imagePath}: {ex.Message}{Environment.NewLine}");
                }
            }
        }

        // Finalize audit log
        File.AppendAllText(auditLogPath, $"Audit Log - Completed at {DateTime.Now}{Environment.NewLine}");
    }
}