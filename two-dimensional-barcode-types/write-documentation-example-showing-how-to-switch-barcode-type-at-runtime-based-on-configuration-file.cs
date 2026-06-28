using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates reading a simple configuration file and generating a barcode
/// using Aspose.BarCode based on the specified symbology and text.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Reads configuration, resolves the barcode symbology,
    /// generates the barcode image, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Path to the simple configuration file
        string configPath = "barcodeConfig.txt";

        // Ensure the configuration file exists; create a default one if missing
        if (!File.Exists(configPath))
        {
            // Default configuration: Code128 barcode with sample text
            File.WriteAllText(configPath, "Symbology=Code128\nCodeText=HelloWorld");
            Console.WriteLine($"Created default config file at '{configPath}'.");
        }

        // Read all lines from the configuration file
        string[] lines = File.ReadAllLines(configPath);
        string symbologyName = null;
        string codeText = null;

        // Parse each line to extract key/value pairs
        foreach (string line in lines)
        {
            // Skip empty lines or lines without an '=' delimiter
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                continue;

            // Split the line into key and value (limit to 2 parts)
            string[] parts = line.Split(new[] { '=' }, 2);
            string key = parts[0].Trim();
            string value = parts[1].Trim();

            // Assign values based on recognized keys (case‑insensitive)
            if (key.Equals("Symbology", StringComparison.OrdinalIgnoreCase))
                symbologyName = value;
            else if (key.Equals("CodeText", StringComparison.OrdinalIgnoreCase))
                codeText = value;
        }

        // Validate that required configuration fields were provided
        if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
        {
            Console.WriteLine("Configuration is missing required fields 'Symbology' or 'CodeText'.");
            return;
        }

        // Resolve the symbology name to a BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the reflected field value to the appropriate enum type
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Define the output file path for the generated barcode image
        string outputPath = "generated_barcode.png";

        // Create a barcode generator with the resolved type and provided text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example: set a simple property (optional)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine($"Barcode generated: {outputPath}");
    }
}