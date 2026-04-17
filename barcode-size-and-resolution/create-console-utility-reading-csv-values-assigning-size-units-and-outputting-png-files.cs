using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // CSV file path can be passed as first argument; otherwise use "input.csv"
        string csvPath = args.Length > 0 ? args[0] : "input.csv";

        // Load CSV rows (comma‑separated). Expected columns:
        // Symbology, CodeText, BarHeightMillimeters (optional), XDimensionPixels (optional)
        List<string[]> rows = new List<string[]>();

        if (File.Exists(csvPath))
        {
            foreach (var line in File.ReadAllLines(csvPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                rows.Add(parts);
            }
        }
        else
        {
            // Fallback sample data when CSV is missing
            rows.Add(new[] { "Code128", "ABC123", "10", "3" });
            rows.Add(new[] { "QR", "Hello World" });
            rows.Add(new[] { "Code39FullASCII", "A*B/C", "12", "2" });
        }

        int index = 1;
        foreach (var fields in rows)
        {
            if (fields.Length < 2)
                continue; // Need at least symbology and text

            string symbology = fields[0].Trim();
            string codeText = fields[1].Trim();

            // Resolve symbology name to a BaseEncodeType using reflection
            BaseEncodeType encodeType = ResolveEncodeType(symbology);

            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Optional: set bar height in millimeters if provided
                if (fields.Length >= 3 && float.TryParse(fields[2], out float barHeightMm) && barHeightMm > 0f)
                {
                    generator.Parameters.Barcode.BarHeight.Millimeters = barHeightMm;
                }

                // Optional: set X‑dimension in pixels if provided
                if (fields.Length >= 4 && float.TryParse(fields[3], out float xDimPx) && xDimPx > 0f)
                {
                    generator.Parameters.Barcode.XDimension.Pixels = xDimPx;
                }

                // Save each barcode as a PNG file
                string fileName = $"barcode_{index}_{symbology}.png";
                generator.Save(fileName);
                Console.WriteLine($"Saved {fileName}");
            }

            index++;
        }
    }

    // Resolves a symbology name (case‑insensitive) to the corresponding EncodeTypes field.
    // Returns Code128 as a safe default if the name cannot be resolved.
    static BaseEncodeType ResolveEncodeType(string name)
    {
        var prop = typeof(EncodeTypes).GetProperty(name,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (prop != null)
        {
            return (BaseEncodeType)prop.GetValue(null);
        }

        // Fallback to Code128 if the requested symbology is not found
        return EncodeTypes.Code128;
    }
}