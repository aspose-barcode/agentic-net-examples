// Title: Generate multiple barcodes from a configuration file
// Description: Demonstrates reading a simple text configuration to select barcode symbology and data, then generating corresponding images at runtime.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to dynamically choose barcode types using EncodeTypes and BarcodeGenerator. Typical use cases include batch processing, configurable reporting, and on‑the‑fly barcode creation where the symbology is defined outside the code. Developers often need to map configuration values to BaseEncodeType, generate images, and store them programmatically.
// Prompt: Write documentation example showing how to switch barcode type at runtime based on configuration file.
// Tags: barcode symbology, runtime configuration, batch generation, aspnet, aspose.barcode, encode types, image output

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates reading a configuration file to generate different barcode types at runtime using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a sample config if missing, reads each line, resolves the symbology, generates a barcode image, and saves it.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Create a sample configuration file if it does not already exist
        // ------------------------------------------------------------
        string configPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.txt");
        if (!File.Exists(configPath))
        {
            var sampleLines = new[]
            {
                "Code128,HelloWorld",
                "QR,https://example.com",
                "DataMatrix,1234567890"
            };
            File.WriteAllLines(configPath, sampleLines);
            Console.WriteLine($"Sample configuration created at: {configPath}");
        }

        // ------------------------------------------------------------
        // Prepare the output folder where generated barcode images will be saved
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // ------------------------------------------------------------
        // Read all configuration lines and initialise a list to track generated files
        // ------------------------------------------------------------
        var lines = File.ReadAllLines(configPath);
        var generatedFiles = new List<string>();

        // ------------------------------------------------------------
        // Process each non‑empty, non‑comment line in the configuration
        // ------------------------------------------------------------
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            // Skip empty lines or comments (lines starting with '#')
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue;

            // Split the line into symbology name and the text to encode
            string[] parts = line.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
            {
                Console.WriteLine($"Invalid config line {i + 1}: {line}");
                continue;
            }

            string symbologyName = parts[0].Trim();
            string codeText = parts[1].Trim();

            // --------------------------------------------------------
            // Resolve the symbology name to a BaseEncodeType using reflection
            // --------------------------------------------------------
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{symbologyName}' on line {i + 1}");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string outputPath = Path.Combine(outputFolder, $"{symbologyName}_{i + 1}.png");

            // --------------------------------------------------------
            // Generate the barcode image and save it as PNG
            // --------------------------------------------------------
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Optional: set resolution or other parameters here if needed
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            generatedFiles.Add(outputPath);
            Console.WriteLine($"Generated {symbologyName} barcode: {outputPath}");
        }

        // ------------------------------------------------------------
        // Output summary of generated files
        // ------------------------------------------------------------
        Console.WriteLine("Barcode generation completed.");
        Console.WriteLine("Generated files:");
        foreach (var file in generatedFiles)
        {
            Console.WriteLine(file);
        }
    }
}