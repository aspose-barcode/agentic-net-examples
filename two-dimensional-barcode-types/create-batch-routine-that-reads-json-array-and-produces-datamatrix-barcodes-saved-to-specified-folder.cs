using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates DataMatrix barcodes from a JSON array of strings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments:
    /// 0 – path to a JSON file containing an array of strings (default: "sample.json").
    /// 1 – output folder for generated PNG files (default: "Barcodes").
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine JSON input path (first argument) or use default.
        string jsonPath = args.Length > 0 ? args[0] : "sample.json";

        // Determine output folder (second argument) or use default.
        string outputFolder = args.Length > 1 ? args[1] : "Barcodes";

        // If the JSON file does not exist, create a sample file and exit.
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"JSON file not found: {jsonPath}");

            // Create a small sample JSON array to demonstrate functionality.
            string[] sampleData = new[] { "ABC123", "HelloWorld", "DataMatrix2026" };
            string sampleJson = JsonSerializer.Serialize(
                sampleData,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(jsonPath, sampleJson);
            Console.WriteLine($"Sample JSON file created at {jsonPath}. Rerun the program to generate barcodes.");
            return;
        }

        // Read the entire JSON file content.
        string jsonContent = File.ReadAllText(jsonPath);
        string[] codeTexts;

        // Attempt to deserialize the JSON array into a string[].
        try
        {
            codeTexts = JsonSerializer.Deserialize<string[]>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Validate that we have at least one code text to process.
        if (codeTexts == null || codeTexts.Length == 0)
        {
            Console.WriteLine("JSON array is empty. No barcodes to generate.");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Retrieve the BaseEncodeType for DataMatrix using reflection.
        var field = typeof(EncodeTypes).GetField(nameof(EncodeTypes.DataMatrix));
        if (field == null)
        {
            Console.WriteLine("EncodeTypes.DataMatrix not found.");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        int index = 0;
        // Iterate over each text value and generate a barcode.
        foreach (var text in codeTexts)
        {
            // Skip null, empty, or whitespace-only entries.
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine($"Skipping empty code text at index {index}.");
                index++;
                continue;
            }

            // Create a file‑system‑safe filename based on the text.
            string safeFileName = GetSafeFileName(text);
            string outputPath = Path.Combine(outputFolder, $"{index:D4}_{safeFileName}.png");

            // Generate the DataMatrix barcode and save it as PNG.
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Optional: set any DataMatrix‑specific parameters here if needed.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated DataMatrix barcode for \"{text}\" at {outputPath}");
            index++;
        }
    }

    /// <summary>
    /// Replaces characters that are invalid in file names with an underscore
    /// and truncates the result to a reasonable length.
    /// </summary>
    /// <param name="input">Original string to sanitize.</param>
    /// <returns>A file‑system‑safe string.</returns>
    private static string GetSafeFileName(string input)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, '_');
        }

        // Limit length to avoid overly long filenames (max 50 characters).
        return input.Length > 50 ? input.Substring(0, 50) : input;
    }
}