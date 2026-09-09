// Title: Generate Barcodes for All Symbologies with Checksum Variations and Logging
// Description: This example creates PNG barcodes for every supported symbology, demonstrates default, checksum‑enabled and checksum‑disabled generation, and logs any errors.
// Category-Description: Aspose.BarCode barcode generation examples – shows how to enumerate EncodeTypes, create BarcodeGenerator instances, toggle checksum using EnableChecksum, save images, and capture exceptions. Useful for developers needing batch barcode creation across all symbologies, handling default behavior and checksum settings.
// Prompt: Write a script that generates barcodes for all symbologies, toggles checksum per default behavior, and logs exceptions.
// Tags: barcode, symbology, generation, checksum, logging, aspose.barcode, example

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of barcodes for every supported symbology,
/// toggling checksum settings and logging successes or failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, iterates over all
    /// EncodeTypes, generates default, checksum‑enabled and checksum‑disabled
    /// barcodes, and writes a log file with the results.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for output files and a log file.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string logPath = Path.Combine(outputDir, "log.txt");

        // Local helper to write messages to console and append them to the log file.
        void Log(string message)
        {
            Console.WriteLine(message);
            File.AppendAllText(logPath, message + Environment.NewLine);
        }

        // Retrieve all public static fields of EncodeTypes (each represents a symbology).
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            string symName = field.Name;
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string codeText = "1234567890";

            // -----------------------------------------------------------------
            // 1. Default generation (no explicit checksum setting)
            // -----------------------------------------------------------------
            string defaultPath = Path.Combine(outputDir, $"{symName}_Default.png");
            try
            {
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Save(defaultPath, BarCodeImageFormat.Png);
                }
                Log($"Generated default barcode for {symName}");
            }
            catch (Exception ex)
            {
                Log($"Error generating default barcode for {symName}: {ex.Message}");
            }

            // -----------------------------------------------------------------
            // 2. Generation with checksum explicitly enabled
            // -----------------------------------------------------------------
            string checksumYesPath = Path.Combine(outputDir, $"{symName}_ChecksumYes.png");
            try
            {
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                    generator.Save(checksumYesPath, BarCodeImageFormat.Png);
                }
                Log($"Generated checksum-enabled barcode for {symName}");
            }
            catch (Exception ex)
            {
                Log($"Error generating checksum-enabled barcode for {symName}: {ex.Message}");
            }

            // -----------------------------------------------------------------
            // 3. Generation with checksum explicitly disabled
            // -----------------------------------------------------------------
            string checksumNoPath = Path.Combine(outputDir, $"{symName}_ChecksumNo.png");
            try
            {
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                    generator.Save(checksumNoPath, BarCodeImageFormat.Png);
                }
                Log($"Generated checksum-disabled barcode for {symName}");
            }
            catch (Exception ex)
            {
                Log($"Error generating checksum-disabled barcode for {symName}: {ex.Message}");
            }
        }

        // Final log entry indicating completion and location of generated files.
        Log($"Barcode generation completed. Files are located at: {outputDir}");
    }
}