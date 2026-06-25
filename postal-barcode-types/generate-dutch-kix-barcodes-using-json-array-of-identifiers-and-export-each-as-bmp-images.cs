using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation console application.
/// </summary>
class Program
{
    /// <summary>
    /// Reads a JSON file (or uses sample data), parses identifiers, and generates Dutch KIX barcodes.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a path to a JSON file.</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Determine JSON source: command‑line argument or fallback sample.
        // ------------------------------------------------------------
        string jsonContent;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            // Read JSON content from the supplied file.
            jsonContent = File.ReadAllText(args[0]);
        }
        else
        {
            // No valid file supplied – use a hard‑coded sample array of identifiers.
            var sampleIds = new[] { "123456789012", "987654321098", "555555555555" };
            jsonContent = JsonSerializer.Serialize(sampleIds);
            Console.WriteLine("No valid JSON file supplied – using sample identifiers.");
        }

        // ------------------------------------------------------------
        // Parse the JSON array of strings into a List<string>.
        // ------------------------------------------------------------
        List<string> identifiers;
        try
        {
            identifiers = JsonSerializer.Deserialize<List<string>>(jsonContent);
            if (identifiers == null)
                throw new Exception("Deserialized list is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Ensure the output directory exists.
        // ------------------------------------------------------------
        string outputDir = "KixBarcodes";
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Generate a Dutch KIX barcode for each identifier.
        // ------------------------------------------------------------
        foreach (string id in identifiers)
        {
            // Skip null, empty, or whitespace identifiers.
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Skipping empty identifier.");
                continue;
            }

            // Create a file‑system‑safe file name from the identifier.
            string safeFileName = Path.GetInvalidFileNameChars().Length > 0
                ? string.Concat(id.Split(Path.GetInvalidFileNameChars()))
                : id;
            string outputPath = Path.Combine(outputDir, $"{safeFileName}.bmp");

            try
            {
                // Initialize the barcode generator with Dutch KIX encoding.
                using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, id))
                {
                    // Optional: let the engine choose dimensions via auto‑size mode.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                    // Save the generated barcode as a BMP image.
                    generator.Save(outputPath, BarCodeImageFormat.Bmp);
                }

                Console.WriteLine($"Saved barcode for '{id}' to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for '{id}': {ex.Message}");
            }
        }
    }
}