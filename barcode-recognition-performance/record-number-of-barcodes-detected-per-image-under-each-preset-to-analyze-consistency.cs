using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition with different quality settings,
/// and cleanup of temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, tests recognition under various quality presets,
    /// outputs the detection counts, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Generate sample barcode images and obtain their file paths.
        List<string> imagePaths = GenerateSampleBarcodes();

        // Define the quality presets to be evaluated.
        var presets = new List<(string Name, QualitySettings Settings)>
        {
            ("NormalQuality", QualitySettings.NormalQuality),
            ("HighPerformance", QualitySettings.HighPerformance),
            ("HighQuality", QualitySettings.HighQuality)
        };

        // Dictionary to store detection counts: preset name -> list of counts per image.
        var results = new Dictionary<string, List<int>>();

        // Iterate over each quality preset.
        foreach (var preset in presets)
        {
            var counts = new List<int>();

            // Process each generated barcode image.
            foreach (string path in imagePaths)
            {
                // Verify that the file exists before attempting to read.
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Warning: File not found '{path}'. Skipping.");
                    counts.Add(0);
                    continue;
                }

                // Open the barcode reader for the current image.
                using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset to the reader.
                    reader.QualitySettings = preset.Settings;

                    // Perform barcode recognition.
                    BarCodeResult[] detected = reader.ReadBarCodes();

                    // Record the number of barcodes detected in this image.
                    counts.Add(detected.Length);
                }
            }

            // Store the counts for the current preset.
            results[preset.Name] = counts;
        }

        // Output a header describing the collected data.
        Console.WriteLine("Barcode detection counts per image for each quality preset:");
        for (int i = 0; i < imagePaths.Count; i++)
        {
            Console.WriteLine($"Image {i + 1}: {Path.GetFileName(imagePaths[i])}");
        }
        Console.WriteLine();

        // Display detection results for each quality preset.
        foreach (var kvp in results)
        {
            Console.WriteLine($"Preset: {kvp.Key}");
            for (int i = 0; i < kvp.Value.Count; i++)
            {
                Console.WriteLine($"  Image {i + 1}: {kvp.Value[i]} barcode(s) detected");
            }
            Console.WriteLine();
        }

        // Remove temporary barcode images and the containing directory.
        CleanupSampleBarcodes(imagePaths);
    }

    /// <summary>
    /// Generates a set of barcode images using different symbologies and returns their file paths.
    /// </summary>
    /// <returns>List of full file paths to the generated barcode images.</returns>
    private static List<string> GenerateSampleBarcodes()
    {
        var paths = new List<string>();
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample");
        Directory.CreateDirectory(tempDir);

        // Define sample data for each barcode type.
        var samples = new List<(BaseEncodeType Type, string Text, string FileName)>
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // Generate each barcode and save it as a PNG file.
        foreach (var sample in samples)
        {
            string fullPath = Path.Combine(tempDir, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.Type, sample.Text))
            {
                // Save the generated barcode image.
                generator.Save(fullPath, BarCodeImageFormat.Png);
            }
            paths.Add(fullPath);
        }

        return paths;
    }

    /// <summary>
    /// Deletes the temporary barcode image files and their containing directory.
    /// </summary>
    /// <param name="paths">List of file paths to delete.</param>
    private static void CleanupSampleBarcodes(List<string> paths)
    {
        // Delete each individual file.
        foreach (string path in paths)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
                // Suppress any exceptions during file deletion.
            }
        }

        // Delete the temporary directory if it exists.
        try
        {
            string dir = Path.GetDirectoryName(paths[0]);
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }
        }
        catch
        {
            // Suppress any exceptions during directory deletion.
        }
    }
}