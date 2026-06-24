using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents the serializable configuration for a barcode, including caption settings.
/// </summary>
public class BarcodeConfig
{
    /// <summary>Gets or sets the barcode data string.</summary>
    public string CodeText { get; set; }

    /// <summary>Gets or sets the caption text displayed above the barcode.</summary>
    public string CaptionAboveText { get; set; }

    /// <summary>Gets or sets the font family name for the caption.</summary>
    public string CaptionAboveFontFamily { get; set; }

    /// <summary>Gets or sets the font size (in points) for the caption.</summary>
    public float CaptionAboveFontSize { get; set; }

    /// <summary>Gets or sets the alignment value (as string) for the caption.</summary>
    public string CaptionAboveAlignment { get; set; }

    /// <summary>Gets or sets the ARGB color value for the caption text.</summary>
    public int CaptionAboveTextColorArgb { get; set; }

    /// <summary>Gets or sets a value indicating whether the caption is visible.</summary>
    public bool CaptionAboveVisible { get; set; }
}

class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves its configuration,
    /// reloads the configuration, and recreates the barcode using the loaded settings.
    /// </summary>
    static void Main()
    {
        // Define file paths for the generated images and configuration JSON.
        string imagePath1 = "barcode1.png";
        string imagePath2 = "barcode2.png";
        string configPath = "barcodeConfig.json";

        // --------------------------------------------------------------------
        // Create the first barcode with explicit caption settings.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set caption text and styling.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;
            generator.Parameters.CaptionAbove.Visible = true;

            // Save the barcode image to disk.
            generator.Save(imagePath1);
        }

        // --------------------------------------------------------------------
        // Capture the current barcode settings into a serializable config object.
        // --------------------------------------------------------------------
        var config = new BarcodeConfig
        {
            CodeText = "123ABC",
            CaptionAboveText = "Sample Caption",
            CaptionAboveFontFamily = "Arial",
            CaptionAboveFontSize = 12f,
            CaptionAboveAlignment = TextAlignment.Center.ToString(),
            CaptionAboveTextColorArgb = Color.Blue.ToArgb(),
            CaptionAboveVisible = true
        };

        // Serialize the configuration to a formatted JSON string.
        string json = JsonSerializer.Serialize(
            config,
            new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON to the configuration file.
        File.WriteAllText(configPath, json);
        Console.WriteLine($"Configuration saved to {configPath}");

        // --------------------------------------------------------------------
        // Load the configuration from JSON and validate it.
        // --------------------------------------------------------------------
        var loadedConfig = JsonSerializer.Deserialize<BarcodeConfig>(File.ReadAllText(configPath));
        if (loadedConfig == null)
        {
            Console.WriteLine("Failed to load configuration.");
            return;
        }

        // --------------------------------------------------------------------
        // Recreate the barcode using the loaded configuration values.
        // --------------------------------------------------------------------
        using (var generator2 = new BarcodeGenerator(EncodeTypes.Code128, loadedConfig.CodeText))
        {
            generator2.Parameters.CaptionAbove.Text = loadedConfig.CaptionAboveText;
            generator2.Parameters.CaptionAbove.Font.FamilyName = loadedConfig.CaptionAboveFontFamily;
            generator2.Parameters.CaptionAbove.Font.Size.Point = loadedConfig.CaptionAboveFontSize;
            generator2.Parameters.CaptionAbove.Alignment = (TextAlignment)Enum.Parse(
                typeof(TextAlignment),
                loadedConfig.CaptionAboveAlignment);
            generator2.Parameters.CaptionAbove.TextColor = Color.FromArgb(loadedConfig.CaptionAboveTextColorArgb);
            generator2.Parameters.CaptionAbove.Visible = loadedConfig.CaptionAboveVisible;

            // Save the recreated barcode image.
            generator2.Save(imagePath2);
        }

        // Output the locations of the generated barcode images.
        Console.WriteLine($"Original barcode saved to {imagePath1}");
        Console.WriteLine($"Recreated barcode saved to {imagePath2}");
    }
}