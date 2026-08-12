// Title: Runtime Barcode Symbology Selection from Configuration File
// Description: Demonstrates how to read a simple configuration file to choose the barcode symbology and code text at runtime, then generate the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating dynamic selection of barcode types using EncodeTypes and BarcodeGenerator. Developers often need to switch symbologies based on external settings such as configuration files, user input, or database values. The pattern shown here is common for building flexible barcode creation services.
// Prompt: Write documentation example showing how to switch barcode type at runtime based on configuration file.
// Tags: barcode, symbology, runtime, configuration, generation, aspose.barcode, encode types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads barcode settings from a configuration file,
/// resolves the requested symbology at runtime, and generates a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Reads configuration, resolves the barcode type,
    /// generates the barcode, and writes status information to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Load configuration
        // --------------------------------------------------------------------
        // Path to the simple configuration file (key=value per line)
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barcodeConfig.txt");

        // Default values used when the config file is missing or incomplete
        string symbologyName = "Code128";
        string codeText = "SampleText";

        if (File.Exists(configPath))
        {
            try
            {
                // Parse each line of the config file
                foreach (string line in File.ReadAllLines(configPath))
                {
                    // Skip empty lines or lines without an '=' separator
                    if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                        continue;

                    // Split into key and value parts
                    string[] parts = line.Split(new[] { '=' }, 2);
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    // Assign values based on recognized keys
                    if (key.Equals("Symbology", StringComparison.OrdinalIgnoreCase))
                        symbologyName = value;
                    else if (key.Equals("CodeText", StringComparison.OrdinalIgnoreCase))
                        codeText = value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading config file: {ex.Message}");
                Console.WriteLine("Falling back to default settings.");
            }
        }
        else
        {
            Console.WriteLine("Config file not found. Using default barcode settings.");
        }

        // --------------------------------------------------------------------
        // Resolve the requested symbology to a BaseEncodeType instance
        // --------------------------------------------------------------------
        BaseEncodeType encodeType = ResolveEncodeType(symbologyName);
        if (encodeType == null)
        {
            Console.WriteLine($"Unknown symbology '{symbologyName}'. Defaulting to Code128.");
            encodeType = EncodeTypes.Code128;
        }

        // --------------------------------------------------------------------
        // Generate and save the barcode image
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.png");
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(outputPath);
        }

        // --------------------------------------------------------------------
        // Output summary information
        // --------------------------------------------------------------------
        Console.WriteLine($"Barcode generated: {outputPath}");
        Console.WriteLine($"Symbology: {encodeType.GetType().Name} ({symbologyName})");
        Console.WriteLine($"CodeText: {codeText}");
    }

    /// <summary>
    /// Uses reflection to map a symbology name (e.g., "Code128") to the corresponding
    /// static field in <see cref="EncodeTypes"/> and returns its <see cref="BaseEncodeType"/> value.
    /// </summary>
    /// <param name="name">The name of the symbology to resolve.</param>
    /// <returns>The matching <see cref="BaseEncodeType"/>, or null if not found.</returns>
    private static BaseEncodeType ResolveEncodeType(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        // EncodeTypes fields are static readonly members; locate the field by name
        FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            return null;

        return field.GetValue(null) as BaseEncodeType;
    }
}