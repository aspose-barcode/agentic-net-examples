// Title: Profiling CPU impact of AllowIncorrectBarcodes during barcode scanning
// Description: Demonstrates how to measure the performance difference when the AllowIncorrectBarcodes setting is toggled while scanning multiple barcode types.
// Category-Description: This example belongs to the Aspose.BarCode scanning and performance profiling category. It showcases the use of BarCodeReader with QualitySettings, BarcodeGenerator for creating sample images, and Stopwatch for timing. Developers often need to evaluate how configuration options affect CPU load in high‑throughput scanning scenarios, making this a useful reference for performance testing.
// Prompt: Profile the impact of AllowIncorrectBarcodes on overall CPU load during continuous scanning.
// Tags: barcode, scanning, performance, allowincorrectbarcodes, aspose.barcode, csharp, profiling

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates profiling the CPU impact of the AllowIncorrectBarcodes setting during continuous barcode scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcodes, profiles scanning with different AllowIncorrectBarcodes settings, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeProfile_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode files (Code128, QR, DataMatrix, Aztec, Pdf417)
        var barcodeFiles = GenerateSampleBarcodes(tempFolder);

        // Number of scan repetitions for each configuration
        const int repetitions = 100;

        // Profile scanning with AllowIncorrectBarcodes set to false
        double timeFalse = ProfileScanning(barcodeFiles, false, repetitions);
        Console.WriteLine($"AllowIncorrectBarcodes = false, total time (ms): {timeFalse:F2}, avg per scan (ms): {timeFalse / (barcodeFiles.Count * repetitions):F4}");

        // Profile scanning with AllowIncorrectBarcodes set to true
        double timeTrue = ProfileScanning(barcodeFiles, true, repetitions);
        Console.WriteLine($"AllowIncorrectBarcodes = true,  total time (ms): {timeTrue:F2}, avg per scan (ms): {timeTrue / (barcodeFiles.Count * repetitions):F4}");

        // Clean up generated barcode files and temporary folder
        foreach (var file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Generates sample barcode images of various symbologies and returns their file paths.
    /// </summary>
    /// <param name="folder">The folder where barcode images will be saved.</param>
    /// <returns>List of file paths for the generated barcode images.</returns>
    static System.Collections.Generic.List<string> GenerateSampleBarcodes(string folder)
    {
        var files = new System.Collections.Generic.List<string>();

        // Code128 barcode
        string path1 = Path.Combine(folder, "code128.png");
        using (var gen = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            gen.Save(path1, BarCodeImageFormat.Png);
        }
        files.Add(path1);

        // QR code
        string path2 = Path.Combine(folder, "qr.png");
        using (var gen = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            gen.Save(path2, BarCodeImageFormat.Png);
        }
        files.Add(path2);

        // DataMatrix barcode
        string path3 = Path.Combine(folder, "datamatrix.png");
        using (var gen = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
        {
            gen.Save(path3, BarCodeImageFormat.Png);
        }
        files.Add(path3);

        // Aztec barcode
        string path4 = Path.Combine(folder, "aztec.png");
        using (var gen = new BarcodeGenerator(EncodeTypes.Aztec, "AZTEC"))
        {
            gen.Save(path4, BarCodeImageFormat.Png);
        }
        files.Add(path4);

        // Pdf417 barcode
        string path5 = Path.Combine(folder, "pdf417.png");
        using (var gen = new BarcodeGenerator(EncodeTypes.Pdf417, "PDF417DATA"))
        {
            gen.Save(path5, BarCodeImageFormat.Png);
        }
        files.Add(path5);

        return files;
    }

    /// <summary>
    /// Profiles the time required to scan a collection of barcode images repeatedly, with a specified AllowIncorrectBarcodes setting.
    /// </summary>
    /// <param name="files">List of barcode image file paths to scan.</param>
    /// <param name="allowIncorrect">Whether to allow incorrect barcodes during scanning.</param>
    /// <param name="repetitions">Number of times each file is scanned.</param>
    /// <returns>Total elapsed time in milliseconds for the entire profiling run.</returns>
    static double ProfileScanning(System.Collections.Generic.List<string> files, bool allowIncorrect, int repetitions)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Perform the scanning loop
        for (int i = 0; i < repetitions; i++)
        {
            foreach (var file in files)
            {
                if (!File.Exists(file))
                    continue;

                // Initialize a reader for the current barcode image
                using (var reader = new BarCodeReader(file))
                {
                    // Apply the AllowIncorrectBarcodes setting for this run
                    reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                    // ReadBarCodes returns an array; results are ignored as we only measure performance
                    var results = reader.ReadBarCodes();
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}