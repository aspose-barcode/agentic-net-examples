// Title: Serialize and Recreate Barcode Settings with JSON
// Description: Demonstrates how to capture barcode generation parameters, serialize them to JSON, and reload them to produce an identical barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator, EncodeTypes, and related parameter objects to configure a barcode, persist its settings as JSON, and later reconstruct the same visual output. Developers working with dynamic barcode creation, configuration persistence, or automated testing often need to serialize settings for reuse or version control.
// Prompt: Serialize barcode text and caption settings to JSON, then reload them to recreate identical barcode appearances.
// Tags: pdf417, serialization, json, aspose.barcode, caption, barcodegeneration

using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Simple DTO that holds the subset of barcode settings we want to persist.
/// </summary>
class BarcodeSettings
{
    public string EncodeTypeName { get; set; }
    public string CodeText { get; set; }
    public bool CaptionAboveVisible { get; set; }
    public string CaptionAboveText { get; set; }
    public string CaptionAboveFontFamily { get; set; }
    public float CaptionAboveFontSizePoint { get; set; }
    public string CaptionAboveTextColor { get; set; }
}

/// <summary>
/// Demonstrates serialization of barcode parameters to JSON and recreation of the barcode from those parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an original barcode, saves its settings to JSON, then recreates the barcode from the JSON.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory.
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the original image, recreated image, and JSON settings.
        string originalImagePath = Path.Combine(workDir, "original.png");
        string recreatedImagePath = Path.Combine(workDir, "recreated.png");
        string jsonPath = Path.Combine(workDir, "settings.json");

        // -----------------------------------------------------------------
        // 1. Generate the original barcode with a caption and save it as PNG.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;
            generator.Parameters.CaptionAbove.TextColor = Color.Green;
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
        }

        // ---------------------------------------------------------------
        // 2. Extract the relevant settings from a fresh generator instance.
        // ---------------------------------------------------------------
        BarcodeSettings settings;
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            settings = new BarcodeSettings
            {
                EncodeTypeName = nameof(EncodeTypes.Pdf417),
                CodeText = generator.CodeText,
                CaptionAboveVisible = generator.Parameters.CaptionAbove.Visible,
                CaptionAboveText = generator.Parameters.CaptionAbove.Text,
                CaptionAboveFontFamily = generator.Parameters.CaptionAbove.Font.FamilyName,
                CaptionAboveFontSizePoint = generator.Parameters.CaptionAbove.Font.Size.Point,
                CaptionAboveTextColor = GetColorName(generator.Parameters.CaptionAbove.TextColor)
            };
        }

        // -------------------------------------------------
        // 3. Serialize the settings object to a formatted JSON file.
        // -------------------------------------------------
        string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);

        // -------------------------------------------------
        // 4. Read the JSON back and deserialize into a settings object.
        // -------------------------------------------------
        string readJson = File.ReadAllText(jsonPath);
        BarcodeSettings loadedSettings = JsonSerializer.Deserialize<BarcodeSettings>(readJson);

        // -------------------------------------------------
        // 5. Resolve the encode type (via reflection) and recreate the barcode.
        // -------------------------------------------------
        BaseEncodeType encodeType = ResolveEncodeType(loadedSettings.EncodeTypeName) ?? EncodeTypes.Pdf417;

        using (var generator = new BarcodeGenerator(encodeType, loadedSettings.CodeText))
        {
            generator.Parameters.CaptionAbove.Visible = loadedSettings.CaptionAboveVisible;
            generator.Parameters.CaptionAbove.Text = loadedSettings.CaptionAboveText;
            generator.Parameters.CaptionAbove.Font.FamilyName = loadedSettings.CaptionAboveFontFamily;
            generator.Parameters.CaptionAbove.Font.Size.Point = loadedSettings.CaptionAboveFontSizePoint;
            generator.Parameters.CaptionAbove.TextColor = ResolveColor(loadedSettings.CaptionAboveTextColor) ?? Color.Black;
            generator.Save(recreatedImagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // 6. Output the locations of the generated files.
        // -------------------------------------------------
        Console.WriteLine($"Original image saved to: {originalImagePath}");
        Console.WriteLine($"Settings JSON saved to: {jsonPath}");
        Console.WriteLine($"Recreated image saved to: {recreatedImagePath}");
    }

    /// <summary>
    /// Resolves an encode type name (e.g., "Pdf417") to the corresponding <see cref="BaseEncodeType"/> instance.
    /// </summary>
    static BaseEncodeType ResolveEncodeType(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static);
        return field?.GetValue(null) as BaseEncodeType;
    }

    /// <summary>
    /// Resolves a static color name (e.g., "Green") to the corresponding <see cref="Color"/> value.
    /// </summary>
    static Color? ResolveColor(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        PropertyInfo prop = typeof(Color).GetProperty(name, BindingFlags.Public | BindingFlags.Static);
        return prop?.GetValue(null) as Color?;
    }

    /// <summary>
    /// Returns the name of a known static color that matches the supplied <see cref="Color"/>; falls back to "Black".
    /// </summary>
    static string GetColorName(Color color)
    {
        // Simple mapping for known static colors
        if (color.ToArgb() == Color.Black.ToArgb()) return nameof(Color.Black);
        if (color.ToArgb() == Color.White.ToArgb()) return nameof(Color.White);
        if (color.ToArgb() == Color.Red.ToArgb()) return nameof(Color.Red);
        if (color.ToArgb() == Color.Green.ToArgb()) return nameof(Color.Green);
        if (color.ToArgb() == Color.Blue.ToArgb()) return nameof(Color.Blue);
        // Fallback
        return nameof(Color.Black);
    }
}