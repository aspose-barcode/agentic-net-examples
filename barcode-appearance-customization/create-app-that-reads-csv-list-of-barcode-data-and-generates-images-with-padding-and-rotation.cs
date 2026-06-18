using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Reads barcode specifications from a CSV file and generates corresponding PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Processes each line of the CSV and creates barcode images.
    /// </summary>
    static void Main()
    {
        const string csvPath = "barcodes.csv";
        const string outputDir = "output";

        // Verify that the CSV file exists before proceeding.
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Ensure the output directory exists; create it if necessary.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Expected CSV format per line: Symbology,CodeText,RotationAngle,Padding
        // Example: Code128,123ABC,45,20
        foreach (var line in File.ReadAllLines(csvPath))
        {
            // Skip empty or whitespace-only lines.
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line into its constituent parts.
            var parts = line.Split(',');

            // Validate that we have at least four columns.
            if (parts.Length < 4)
            {
                Console.WriteLine($"Invalid line (expected 4 columns): {line}");
                continue;
            }

            // Trim whitespace from each part.
            string symbologyName = parts[0].Trim();
            string codeText = parts[1].Trim();

            // Parse rotation angle; report and skip on failure.
            if (!float.TryParse(parts[2].Trim(), out float rotationAngle))
            {
                Console.WriteLine($"Invalid rotation angle in line: {line}");
                continue;
            }

            // Parse padding value; report and skip on failure.
            if (!float.TryParse(parts[3].Trim(), out float padding))
            {
                Console.WriteLine($"Invalid padding value in line: {line}");
                continue;
            }

            // Resolve the symbology name to an EncodeTypes field.
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {symbologyName}");
                continue;
            }

            // Cast the field value to BaseEncodeType.
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a barcode generator with the specified type and text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply rotation.
                generator.Parameters.RotationAngle = rotationAngle;

                // Apply uniform padding on all sides.
                generator.Parameters.Barcode.Padding.Left.Point = padding;
                generator.Parameters.Barcode.Padding.Top.Point = padding;
                generator.Parameters.Barcode.Padding.Right.Point = padding;
                generator.Parameters.Barcode.Padding.Bottom.Point = padding;

                // Build a safe file name by replacing invalid characters.
                string safeCodeText = codeText.Replace(Path.GetInvalidFileNameChars(), '_');
                string fileName = $"{symbologyName}_{safeCodeText}.png";
                string outputPath = Path.Combine(outputDir, fileName);

                // Attempt to save the generated barcode image.
                try
                {
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode for line: {line}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

/// <summary>
/// Extension methods for string manipulation.
/// </summary>
static class StringExtensions
{
    /// <summary>
    /// Replaces each character in <paramref name="chars"/> with the specified <paramref name="replacement"/>.
    /// </summary>
    /// <param name="str">The original string.</param>
    /// <param name="chars">Array of characters to replace.</param>
    /// <param name="replacement">The character to insert in place of each found character.</param>
    /// <returns>A new string with the specified characters replaced.</returns>
    public static string Replace(this string str, char[] chars, char replacement)
    {
        foreach (var c in chars)
        {
            str = str.Replace(c, replacement);
        }
        return str;
    }
}