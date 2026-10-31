// Title: Serialize and Reload Barcode Settings with JSON
// Description: Demonstrates how to serialize barcode text and caption settings to JSON, then recreate the same barcode from the saved settings.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist and restore barcode generation parameters. It uses the BarcodeGenerator class along with its Parameters and CaptionAbove properties to capture visual settings, a common need when storing barcode templates or sharing configurations across applications. Developers often need to serialize settings for version control, dynamic generation, or reproducing identical barcodes in different environments.
// Prompt: Serialize barcode text and caption settings to JSON, then reload them to recreate identical barcode appearances.
// Tags: barcode, serialization, json, caption, aspose.barcode, pdf417, png, settings

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Represents a subset of barcode generation settings that can be serialized to JSON.
/// </summary>
class BarcodeSettings
{
    public string CodeText { get; set; }
    public bool CaptionAboveVisible { get; set; }
    public string CaptionAboveText { get; set; }
    public string CaptionAboveFontFamily { get; set; }
    public float CaptionAboveFontSizePoint { get; set; }
    public int CaptionAboveTextColor { get; set; }
    public float CaptionAbovePaddingLeft { get; set; }
    public float CaptionAbovePaddingTop { get; set; }
    public float CaptionAbovePaddingRight { get; set; }
    public float CaptionAbovePaddingBottom { get; set; }
}

/// <summary>
/// Demonstrates creating a barcode, saving its visual settings to JSON, and recreating the barcode from those settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two identical barcodes using persisted settings.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file paths
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeJsonDemo");
        Directory.CreateDirectory(outputDir);
        string imgPath1 = Path.Combine(outputDir, "barcode1.png");
        string jsonPath = Path.Combine(outputDir, "settings.json");
        string imgPath2 = Path.Combine(outputDir, "barcode2.png");

        // --------------------------------------------------------------------
        // Create the initial barcode and serialize its settings to JSON
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            // Configure caption appearance
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;
            generator.Parameters.CaptionAbove.TextColor = Color.Green;
            generator.Parameters.CaptionAbove.Padding.Left.Pixels = 5;
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 5;
            generator.Parameters.CaptionAbove.Padding.Right.Pixels = 5;
            generator.Parameters.CaptionAbove.Padding.Bottom.Pixels = 5;

            // Save the first barcode image
            generator.Save(imgPath1, BarCodeImageFormat.Png);

            // Capture the relevant settings into a serializable object
            var settings = new BarcodeSettings
            {
                CodeText = "Sample123",
                CaptionAboveVisible = generator.Parameters.CaptionAbove.Visible,
                CaptionAboveText = generator.Parameters.CaptionAbove.Text,
                CaptionAboveFontFamily = generator.Parameters.CaptionAbove.Font.FamilyName,
                CaptionAboveFontSizePoint = generator.Parameters.CaptionAbove.Font.Size.Point,
                CaptionAboveTextColor = generator.Parameters.CaptionAbove.TextColor.ToArgb(),
                CaptionAbovePaddingLeft = generator.Parameters.CaptionAbove.Padding.Left.Pixels,
                CaptionAbovePaddingTop = generator.Parameters.CaptionAbove.Padding.Top.Pixels,
                CaptionAbovePaddingRight = generator.Parameters.CaptionAbove.Padding.Right.Pixels,
                CaptionAbovePaddingBottom = generator.Parameters.CaptionAbove.Padding.Bottom.Pixels
            };

            // Serialize settings to a formatted JSON file
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
        }

        // --------------------------------------------------------------------
        // Load settings from JSON and recreate an identical barcode
        // --------------------------------------------------------------------
        string jsonRead = File.ReadAllText(jsonPath);
        var loadedSettings = JsonSerializer.Deserialize<BarcodeSettings>(jsonRead);
        using (var generator2 = new BarcodeGenerator(EncodeTypes.Pdf417, loadedSettings.CodeText))
        {
            // Apply the deserialized settings
            generator2.Parameters.CaptionAbove.Visible = loadedSettings.CaptionAboveVisible;
            generator2.Parameters.CaptionAbove.Text = loadedSettings.CaptionAboveText;
            generator2.Parameters.CaptionAbove.Font.FamilyName = loadedSettings.CaptionAboveFontFamily;
            generator2.Parameters.CaptionAbove.Font.Size.Point = loadedSettings.CaptionAboveFontSizePoint;
            generator2.Parameters.CaptionAbove.TextColor = Color.FromArgb(loadedSettings.CaptionAboveTextColor);
            generator2.Parameters.CaptionAbove.Padding.Left.Pixels = loadedSettings.CaptionAbovePaddingLeft;
            generator2.Parameters.CaptionAbove.Padding.Top.Pixels = loadedSettings.CaptionAbovePaddingTop;
            generator2.Parameters.CaptionAbove.Padding.Right.Pixels = loadedSettings.CaptionAbovePaddingRight;
            generator2.Parameters.CaptionAbove.Padding.Bottom.Pixels = loadedSettings.CaptionAbovePaddingBottom;

            // Save the recreated barcode image
            generator2.Save(imgPath2, BarCodeImageFormat.Png);
        }

        // Output the locations of generated files
        Console.WriteLine($"Generated images: {imgPath1}, {imgPath2}");
        Console.WriteLine($"Settings JSON: {jsonPath}");
    }
}