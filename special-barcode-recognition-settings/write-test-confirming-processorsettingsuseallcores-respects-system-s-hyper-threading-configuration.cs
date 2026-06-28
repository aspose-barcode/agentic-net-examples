using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition while configuring processor settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, saves it to a temporary PNG file,
    /// then reads it back using different processor settings.
    /// </summary>
    static void Main()
    {
        // Define path for a temporary PNG file to store the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "test_barcode.png");

        // Generate a simple Code128 barcode with checksum enabled and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Enable checksum for Code128 (required by the API).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Save the barcode image to the temporary file.
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Retrieve and display the number of logical processors (including hyper‑threads).
        int logicalProcessors = Environment.ProcessorCount;
        Console.WriteLine($"Logical processors (including hyper‑threads): {logicalProcessors}");

        // ------------------------------------------------------------
        // Test barcode reading with UseAllCores = true
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        Console.WriteLine($"ProcessorSettings.UseAllCores set to: {BarCodeReader.ProcessorSettings.UseAllCores}");

        // Read the barcode using all available cores.
        using (var readerAll = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            foreach (var result in readerAll.ReadBarCodes())
            {
                Console.WriteLine("[UseAllCores=true] Detected barcode: " + result.CodeText);
            }
        }

        // ------------------------------------------------------------
        // Test barcode reading with UseAllCores = false and limited cores
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        // Limit the number of cores to half of the logical processors, rounded up, but at least 1.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, logicalProcessors / 2);
        Console.WriteLine($"ProcessorSettings.UseAllCores set to: {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"ProcessorSettings.UseOnlyThisCoresCount set to: {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");

        // Read the barcode using the limited core count.
        using (var readerLimited = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            foreach (var result in readerLimited.ReadBarCodes())
            {
                Console.WriteLine("[UseAllCores=false] Detected barcode: " + result.CodeText);
            }
        }

        // ------------------------------------------------------------
        // Clean up: delete the temporary barcode image file
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to delete temporary file: " + ex.Message);
        }
    }
}