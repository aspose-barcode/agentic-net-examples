using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcode images, reading them, and measuring CPU usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary directory for barcode images
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeSample");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // --------------------------------------------------------------------
        // Generate a small set of barcode images (5 items)
        // --------------------------------------------------------------------
        List<string> imagePaths = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            // Build the full file path for the current barcode image
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");

            // Create a barcode generator for Code128 with a unique text value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i}"))
            {
                // Enable checksum calculation for the barcode
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Save the generated barcode image to disk
                generator.Save(filePath);
            }

            // Store the path for later processing
            imagePaths.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Enable multi‑core processing for barcode reading
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // --------------------------------------------------------------------
        // Record CPU usage before processing
        // --------------------------------------------------------------------
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;

        // --------------------------------------------------------------------
        // Process each image and read barcodes
        // --------------------------------------------------------------------
        foreach (string path in imagePaths)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // Initialize a barcode reader for all supported types
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine(
                        $"Image: {Path.GetFileName(path)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Record CPU usage after processing
        // --------------------------------------------------------------------
        currentProcess.Refresh();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;

        // --------------------------------------------------------------------
        // Compute and display CPU time consumed
        // --------------------------------------------------------------------
        TimeSpan cpuUsed = cpuEnd - cpuStart;
        Console.WriteLine(
            $"CPU time used for processing {imagePaths.Count} images: {cpuUsed.TotalMilliseconds} ms");

        // --------------------------------------------------------------------
        // Clean up temporary files
        // --------------------------------------------------------------------
        foreach (string path in imagePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignore any deletion errors (e.g., file in use)
            }
        }

        // Attempt to delete the temporary directory
        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any deletion errors (e.g., directory not empty)
        }
    }
}