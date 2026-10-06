// Title: Runtime Barcode Type Switching Using Configuration File
// Description: Demonstrates how to read a simple text configuration at runtime, map the specified symbology to Aspose.BarCode's EncodeTypes, and generate a barcode image accordingly.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating dynamic barcode creation based on external configuration. It showcases key API classes such as EncodeTypes, BarcodeGenerator, and BarCodeImageFormat. Developers often need to switch barcode symbologies on the fly—for instance, when different partners require different barcode standards—making this pattern useful for configurable, automated barcode workflows.
// Prompt: Write documentation example showing how to switch barcode type at runtime based on configuration file.
// Tags: barcode, symbology, runtime configuration, generation, aspose.barcode, encode types, png output

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads barcode settings from a configuration file,
/// resolves the requested symbology via reflection, and generates a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads configuration, determines the EncodeTypes value,
    /// creates a BarcodeGenerator, and saves the resulting image.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Prepare configuration file path (using the system temporary folder)
        // --------------------------------------------------------------------
        string configPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.txt");

        // ---------------------------------------------------------------
        // Create a default configuration file if one does not already exist
        // ---------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            File.WriteAllLines(configPath, new[]
            {
                "Symbology=Code128",
                "CodeText=Sample123"
            });
            Console.WriteLine($"Default config created at: {configPath}");
        }

        // -------------------------------------------------
        // Load configuration values (fallback to defaults)
        // -------------------------------------------------
        string symbology = "Code128";
        string codeText = "Sample123";

        foreach (string line in File.ReadAllLines(configPath))
        {
            // Split each line into key/value pair (max 2 parts)
            string[] parts = line.Split('=', 2);
            if (parts.Length != 2) continue;

            string key = parts[0].Trim();
            string value = parts[1].Trim();

            // Assign values based on recognized keys
            if (key.Equals("Symbology", StringComparison.OrdinalIgnoreCase))
                symbology = value;
            else if (key.Equals("CodeText", StringComparison.OrdinalIgnoreCase))
                codeText = value;
        }

        // --------------------------------------------------------------
        // Resolve the symbology name to a BaseEncodeType using reflection
        // --------------------------------------------------------------
        FieldInfo field = typeof(EncodeTypes).GetField(symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbology}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // -------------------------------------------------
        // Build output file path (includes timestamp for uniqueness)
        // -------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(),
            $"barcode_{symbology}_{DateTime.Now:yyyyMMddHHmmss}.png");

        // -------------------------------------------------
        // Generate the barcode and save it as PNG
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Inform the user about the generated file
        // -------------------------------------------------
        Console.WriteLine($"Barcode generated with type '{symbology}' and saved to:");
        Console.WriteLine(outputPath);
    }
}