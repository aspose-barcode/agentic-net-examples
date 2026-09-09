// Title: Runtime Barcode Type Switching Based on Configuration File
// Description: Demonstrates reading a configuration file to select a barcode symbology at runtime and generating the corresponding barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use EncodeTypes and BarcodeGenerator to create barcodes dynamically. Developers often need to switch barcode types based on external settings such as configuration files, databases, or user input. The code shows how to resolve a symbology name via reflection, configure basic appearance settings, and save the result in PNG format—common tasks when integrating barcode creation into flexible applications.
// Prompt: Write documentation example showing how to switch barcode type at runtime based on configuration file.
// Tags: barcode symbology, runtime configuration, generation, aspose.barcode, encode types, png output

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads a barcode configuration file, determines the requested symbology,
/// and generates a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads configuration, resolves the symbology, and creates the barcode image.
    /// </summary>
    static void Main()
    {
        // Build the full path to the configuration file located in the current directory.
        string configPath = Path.Combine(Directory.GetCurrentDirectory(), "barcodeConfig.txt");

        // If the configuration file does not exist, create a default one.
        if (!File.Exists(configPath))
        {
            // Default: Code128 symbology with sample code text.
            File.WriteAllText(configPath, "Code128,1234567890");
            Console.WriteLine($"Configuration file not found. Created default at: {configPath}");
        }

        // Read all lines from the configuration file.
        string[] lines = File.ReadAllLines(configPath);
        if (lines.Length == 0)
        {
            Console.WriteLine("Configuration file is empty.");
            return;
        }

        // Find the first non‑empty line; expected format: Symbology,CodeText
        string line = null;
        foreach (var l in lines)
        {
            if (!string.IsNullOrWhiteSpace(l))
            {
                line = l.Trim();
                break;
            }
        }

        if (line == null)
        {
            Console.WriteLine("No valid configuration line found.");
            return;
        }

        // Split the line into symbology name and code text.
        string[] parts = line.Split(new[] { ',' }, 2);
        if (parts.Length != 2)
        {
            Console.WriteLine("Configuration line must contain symbology and code text separated by a comma.");
            return;
        }

        string symbologyName = parts[0].Trim();
        string codeText = parts[1].Trim();

        // Resolve the symbology name to a BaseEncodeType value using reflection.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for symbology: {symbologyName}");
            return;
        }

        // Define the output file path for the generated barcode image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), $"barcode_{symbologyName}.png");

        // Create the barcode generator with the resolved type and provided code text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example: set a simple appearance property (module width).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated: {outputPath}");
    }
}