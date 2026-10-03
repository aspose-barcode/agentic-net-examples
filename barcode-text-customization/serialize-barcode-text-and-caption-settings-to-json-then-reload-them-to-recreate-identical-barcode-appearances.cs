// Title: Serialize and Recreate Barcode with Caption Settings via JSON
// Description: Demonstrates how to capture barcode generation parameters—including caption text, font, color, and padding—into a JSON file, then deserialize them to produce an identical barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category. It shows how to use BarcodeGenerator, EncodeTypes, and related parameter objects (CaptionAbove, CaptionBelow, XDimension) to persist settings, a common need for dynamic barcode creation, configuration storage, and repeatable rendering in enterprise applications. Developers often serialize settings to share across services or to recreate barcodes without hard‑coding parameters.
/// Prompt: Serialize barcode text and caption settings to JSON, then reload them to recreate identical barcode appearances.
/// Tags: pdf417, json, serialization, caption, barcode, aspose.barcode, generation, png

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Simple DTO for caption configuration that can be serialized to JSON.
/// </summary>
class CaptionSettings
{
    public bool Visible { get; set; }
    public string Text { get; set; }
    public string FontFamily { get; set; }
    public float FontSizePoint { get; set; }
    public int TextColorArgb { get; set; }
    public float PaddingLeft { get; set; }
    public float PaddingTop { get; set; }
    public float PaddingRight { get; set; }
    public float PaddingBottom { get; set; }
}

/// <summary>
/// DTO that aggregates all barcode generation settings required for recreation.
/// </summary>
class BarcodeSettings
{
    public string EncodeType { get; set; }
    public string CodeText { get; set; }
    public float XDimensionPixels { get; set; }
    public CaptionSettings CaptionAbove { get; set; }
    public CaptionSettings CaptionBelow { get; set; }
}

/// <summary>
/// Demonstrates serialization of barcode and caption settings to JSON and recreation of the barcode from those settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves its settings to JSON, then rebuilds the same barcode from the JSON file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeJsonDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths
        string initialImagePath = Path.Combine(outputDir, "barcode_initial.png");
        string recreatedImagePath = Path.Combine(outputDir, "barcode_recreated.png");
        string jsonPath = Path.Combine(outputDir, "barcode_settings.json");

        // -----------------------------------------------------------------
        // Create initial barcode with custom caption settings and save it
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Caption Above
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;
            generator.Parameters.CaptionAbove.TextColor = Color.FromArgb(0xFF0000); // Red
            generator.Parameters.CaptionAbove.Padding.Left.Pixels = 5f;
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 5f;
            generator.Parameters.CaptionAbove.Padding.Right.Pixels = 5f;
            generator.Parameters.CaptionAbove.Padding.Bottom.Pixels = 5f;

            // Caption Below
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;
            generator.Parameters.CaptionBelow.TextColor = Color.FromArgb(0x0000FF); // Blue
            generator.Parameters.CaptionBelow.Padding.Left.Pixels = 3f;
            generator.Parameters.CaptionBelow.Padding.Top.Pixels = 3f;
            generator.Parameters.CaptionBelow.Padding.Right.Pixels = 3f;
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 3f;

            generator.Save(initialImagePath, BarCodeImageFormat.Png);
        }

        // ---------------------------------------------------------------
        // Extract current generator settings into a serializable DTO object
        // ---------------------------------------------------------------
        BarcodeSettings settings;
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            // Apply same settings as above to read them back
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;
            generator.Parameters.CaptionAbove.TextColor = Color.FromArgb(0xFF0000);
            generator.Parameters.CaptionAbove.Padding.Left.Pixels = 5f;
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 5f;
            generator.Parameters.CaptionAbove.Padding.Right.Pixels = 5f;
            generator.Parameters.CaptionAbove.Padding.Bottom.Pixels = 5f;

            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;
            generator.Parameters.CaptionBelow.TextColor = Color.FromArgb(0x0000FF);
            generator.Parameters.CaptionBelow.Padding.Left.Pixels = 3f;
            generator.Parameters.CaptionBelow.Padding.Top.Pixels = 3f;
            generator.Parameters.CaptionBelow.Padding.Right.Pixels = 3f;
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 3f;

            settings = new BarcodeSettings
            {
                EncodeType = nameof(EncodeTypes.Pdf417),
                CodeText = "Sample123",
                XDimensionPixels = generator.Parameters.Barcode.XDimension.Pixels,
                CaptionAbove = new CaptionSettings
                {
                    Visible = generator.Parameters.CaptionAbove.Visible,
                    Text = generator.Parameters.CaptionAbove.Text,
                    FontFamily = generator.Parameters.CaptionAbove.Font.FamilyName,
                    FontSizePoint = generator.Parameters.CaptionAbove.Font.Size.Point,
                    TextColorArgb = generator.Parameters.CaptionAbove.TextColor.ToArgb(),
                    PaddingLeft = generator.Parameters.CaptionAbove.Padding.Left.Pixels,
                    PaddingTop = generator.Parameters.CaptionAbove.Padding.Top.Pixels,
                    PaddingRight = generator.Parameters.CaptionAbove.Padding.Right.Pixels,
                    PaddingBottom = generator.Parameters.CaptionAbove.Padding.Bottom.Pixels
                },
                CaptionBelow = new CaptionSettings
                {
                    Visible = generator.Parameters.CaptionBelow.Visible,
                    Text = generator.Parameters.CaptionBelow.Text,
                    FontFamily = generator.Parameters.CaptionBelow.Font.FamilyName,
                    FontSizePoint = generator.Parameters.CaptionBelow.Font.Size.Point,
                    TextColorArgb = generator.Parameters.CaptionBelow.TextColor.ToArgb(),
                    PaddingLeft = generator.Parameters.CaptionBelow.Padding.Left.Pixels,
                    PaddingTop = generator.Parameters.CaptionBelow.Padding.Top.Pixels,
                    PaddingRight = generator.Parameters.CaptionBelow.Padding.Right.Pixels,
                    PaddingBottom = generator.Parameters.CaptionBelow.Padding.Bottom.Pixels
                }
            };
        }

        // -------------------------
        // Serialize settings to JSON
        // -------------------------
        string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Settings serialized to JSON at: {jsonPath}");

        // -------------------------
        // Deserialize settings from JSON
        // -------------------------
        string jsonRead = File.ReadAllText(jsonPath);
        BarcodeSettings loadedSettings = JsonSerializer.Deserialize<BarcodeSettings>(jsonRead);

        // -------------------------------------------------
        // Resolve the EncodeType string back to an EncodeTypes field via reflection
        // -------------------------------------------------
        var field = typeof(EncodeTypes).GetField(loadedSettings.EncodeType);
        if (field == null)
        {
            Console.WriteLine($"Unknown encode type: {loadedSettings.EncodeType}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // -------------------------------------------------
        // Recreate barcode using the deserialized settings and save it
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, loadedSettings.CodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = loadedSettings.XDimensionPixels;

            // Caption Above
            generator.Parameters.CaptionAbove.Visible = loadedSettings.CaptionAbove.Visible;
            generator.Parameters.CaptionAbove.Text = loadedSettings.CaptionAbove.Text;
            generator.Parameters.CaptionAbove.Font.FamilyName = loadedSettings.CaptionAbove.FontFamily;
            generator.Parameters.CaptionAbove.Font.Size.Point = loadedSettings.CaptionAbove.FontSizePoint;
            generator.Parameters.CaptionAbove.TextColor = Color.FromArgb(loadedSettings.CaptionAbove.TextColorArgb);
            generator.Parameters.CaptionAbove.Padding.Left.Pixels = loadedSettings.CaptionAbove.PaddingLeft;
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = loadedSettings.CaptionAbove.PaddingTop;
            generator.Parameters.CaptionAbove.Padding.Right.Pixels = loadedSettings.CaptionAbove.PaddingRight;
            generator.Parameters.CaptionAbove.Padding.Bottom.Pixels = loadedSettings.CaptionAbove.PaddingBottom;

            // Caption Below
            generator.Parameters.CaptionBelow.Visible = loadedSettings.CaptionBelow.Visible;
            generator.Parameters.CaptionBelow.Text = loadedSettings.CaptionBelow.Text;
            generator.Parameters.CaptionBelow.Font.FamilyName = loadedSettings.CaptionBelow.FontFamily;
            generator.Parameters.CaptionBelow.Font.Size.Point = loadedSettings.CaptionBelow.FontSizePoint;
            generator.Parameters.CaptionBelow.TextColor = Color.FromArgb(loadedSettings.CaptionBelow.TextColorArgb);
            generator.Parameters.CaptionBelow.Padding.Left.Pixels = loadedSettings.CaptionBelow.PaddingLeft;
            generator.Parameters.CaptionBelow.Padding.Top.Pixels = loadedSettings.CaptionBelow.PaddingTop;
            generator.Parameters.CaptionBelow.Padding.Right.Pixels = loadedSettings.CaptionBelow.PaddingRight;
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = loadedSettings.CaptionBelow.PaddingBottom;

            generator.Save(recreatedImagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Recreated barcode saved at: {recreatedImagePath}");
    }
}