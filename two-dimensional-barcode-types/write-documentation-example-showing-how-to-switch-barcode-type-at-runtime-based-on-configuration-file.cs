using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Configuration file path (relative to executable)
        const string configPath = "barcodeConfig.txt";

        // Default values if configuration is missing or invalid
        string symbologyName = "Code128";
        string codeText = "Default123";

        // Read configuration if the file exists
        if (File.Exists(configPath))
        {
            try
            {
                // Expected format: SymbologyName=CodeText (e.g., QR=HelloWorld)
                string line = File.ReadAllLines(configPath)[0];
                string[] parts = line.Split(new[] { '=' }, 2);
                if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]) && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    symbologyName = parts[0].Trim();
                    codeText = parts[1].Trim();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                // Continue with default values
            }
        }

        // Resolve the symbology name to an EncodeTypes field using reflection
        BaseEncodeType encodeType = ResolveEncodeType(symbologyName) ?? EncodeTypes.Code128;

        // Prepare output file name
        string outputFile = $"{symbologyName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

        // Generate and save the barcode
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example of setting a simple parameter (optional)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            generator.Save(outputFile);
        }

        Console.WriteLine($"Barcode saved to: {outputFile}");
    }

    // Helper method to map a string name to a static EncodeTypes field
    private static BaseEncodeType ResolveEncodeType(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        // EncodeTypes fields are public static members of type BaseEncodeType
        FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static);
        if (field != null && typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
        {
            return field.GetValue(null) as BaseEncodeType;
        }

        // If not found, try case‑insensitive match
        foreach (FieldInfo fi in typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (string.Equals(fi.Name, name, StringComparison.OrdinalIgnoreCase) &&
                typeof(BaseEncodeType).IsAssignableFrom(fi.FieldType))
            {
                return fi.GetValue(null) as BaseEncodeType;
            }
        }

        // Unknown symbology
        Console.WriteLine($"Warning: Unknown symbology '{name}'. Falling back to Code128.");
        return null;
    }
}