// Title: Barcode detection count per image across quality presets
// Description: Demonstrates generating sample barcodes, then measuring how many barcodes each image yields under different QualitySettings presets to assess detection consistency.
// Prompt: Record the number of barcodes detected per image under each preset to analyze consistency.
// Tags: barcode symbology, detection, qualitysettings, output, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating sample barcode images and recording detection counts per image under various <see cref="QualitySettings"/> presets.
/// </summary>
class Program
{
    /// <summary>
    /// Returns the <see cref="QualitySettings"/> preset that matches the supplied name.
    /// </summary>
    /// <param name="name">Name of the preset (e.g., "HighPerformance").</param>
    /// <returns>The corresponding <see cref="QualitySettings"/> value.</returns>
    static QualitySettings GetPreset(string name)
    {
        return name switch
        {
            "HighPerformance" => QualitySettings.HighPerformance,
            "NormalQuality"   => QualitySettings.NormalQuality,
            "HighQuality"     => QualitySettings.HighQuality,
            "MaxQuality"      => QualitySettings.MaxQuality,
            _ => QualitySettings.NormalQuality,
        };
    }

    /// <summary>
    /// Entry point. Generates barcodes, runs detection with each preset, and prints a table of counts.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for sample barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        if (!Directory.Exists(tempFolder))
            Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define a list of sample barcodes to generate.
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "1234567890", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // --------------------------------------------------------------------
        // Generate sample images and save them as PNG files.
        // --------------------------------------------------------------------
        foreach (var (type, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(type, text))
            {
                generator.Save(filePath); // defaults to PNG
            }
        }

        // --------------------------------------------------------------------
        // Define the QualitySettings preset names to evaluate.
        // --------------------------------------------------------------------
        string[] presetNames = { "HighPerformance", "NormalQuality", "HighQuality", "MaxQuality" };

        // --------------------------------------------------------------------
        // Collect all generated PNG image files.
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(tempFolder, "*.png");

        // --------------------------------------------------------------------
        // Dictionary to hold results: preset -> (image file -> count).
        // --------------------------------------------------------------------
        var results = new Dictionary<string, Dictionary<string, int>>();

        // --------------------------------------------------------------------
        // Iterate over each preset and count detected barcodes per image.
        // --------------------------------------------------------------------
        foreach (string preset in presetNames)
        {
            var perImage = new Dictionary<string, int>();
            QualitySettings qs = GetPreset(preset);

            foreach (string imagePath in imageFiles)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"Warning: file not found {imagePath}");
                    perImage[Path.GetFileName(imagePath)] = 0;
                    continue;
                }

                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    reader.QualitySettings = qs;
                    BarCodeResult[] detected = reader.ReadBarCodes();
                    int count = detected?.Length ?? 0;
                    perImage[Path.GetFileName(imagePath)] = count;
                }
            }

            results[preset] = perImage;
        }

        // --------------------------------------------------------------------
        // Output the counts in a tabular format.
        // --------------------------------------------------------------------
        Console.WriteLine("Barcode detection counts per image under each preset:");
        Console.WriteLine();

        // Header row
        Console.Write("Image\\Preset".PadRight(20));
        foreach (string preset in presetNames)
            Console.Write(preset.PadRight(15));
        Console.WriteLine();

        // Data rows per image
        foreach (string imageFile in imageFiles)
        {
            string fileName = Path.GetFileName(imageFile);
            Console.Write(fileName.PadRight(20));
            foreach (string preset in presetNames)
            {
                int count = results[preset].ContainsKey(fileName) ? results[preset][fileName] : 0;
                Console.Write(count.ToString().PadRight(15));
            }
            Console.WriteLine();
        }

        // --------------------------------------------------------------------
        // Cleanup (optional). Uncomment to delete the temporary folder.
        // --------------------------------------------------------------------
        // Directory.Delete(tempFolder, true);
    }
}