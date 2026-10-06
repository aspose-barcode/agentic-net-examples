// Title: Barcode generation with configurable appearance settings from a text file
// Description: Demonstrates how to read barcode appearance parameters from a simple configuration file and apply them to an Aspose.BarCode generator to produce a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode configuration and rendering category, illustrating the use of BarcodeGenerator, EncodeTypes, and the Parameters property to customize AutoSizeMode, XDimension, padding, and resolution. Developers often need to externalize barcode styling for dynamic generation in web services, batch processing, or desktop applications. The pattern shown helps integrate configurable barcode appearance without recompiling code.
// Prompt: Design a configuration file format to store barcode appearance settings such as AutoSizeMode, XDimension, and padding values.
// Tags: barcode,code128,generation,configuration,autosizemode,xdimension,padding,aspose.barcode,aspose.drawing,png

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates loading barcode appearance settings from a configuration file and generating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary directory, writes a default config if missing, loads settings,
    /// applies them to a <see cref="BarcodeGenerator"/>, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeConfigDemo");
        Directory.CreateDirectory(tempDir);

        // Path to the simple key=value configuration file.
        string configPath = Path.Combine(tempDir, "barcodeConfig.txt");

        // If the config file does not exist, create it with default values.
        if (!File.Exists(configPath))
        {
            using (var writer = new StreamWriter(configPath))
            {
                writer.WriteLine("AutoSizeMode=None");
                writer.WriteLine("XDimension=3");
                writer.WriteLine("PaddingLeft=5");
                writer.WriteLine("PaddingTop=5");
                writer.WriteLine("PaddingRight=5");
                writer.WriteLine("PaddingBottom=5");
                writer.WriteLine("Resolution=300");
            }
        }

        // Load configuration key/value pairs into a dictionary.
        var settings = LoadConfig(configPath);

        // Destination path for the generated barcode image.
        string outputPath = Path.Combine(tempDir, "generatedBarcode.png");

        // Create a barcode generator for Code128 with the text "ASPOSE".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Apply AutoSizeMode if present in the config.
            if (settings.TryGetValue("AutoSizeMode", out string autoSizeStr) &&
                Enum.TryParse<AutoSizeMode>(autoSizeStr, true, out var autoSize))
            {
                generator.Parameters.AutoSizeMode = autoSize;
            }

            // Apply XDimension (module width) if present.
            if (settings.TryGetValue("XDimension", out string xDimStr) &&
                float.TryParse(xDimStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var xDim))
            {
                generator.Parameters.Barcode.XDimension.Point = xDim;
            }

            // Apply left padding if present.
            if (settings.TryGetValue("PaddingLeft", out string padLStr) &&
                float.TryParse(padLStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var padL))
            {
                generator.Parameters.Barcode.Padding.Left.Point = padL;
            }

            // Apply top padding if present.
            if (settings.TryGetValue("PaddingTop", out string padTStr) &&
                float.TryParse(padTStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var padT))
            {
                generator.Parameters.Barcode.Padding.Top.Point = padT;
            }

            // Apply right padding if present.
            if (settings.TryGetValue("PaddingRight", out string padRStr) &&
                float.TryParse(padRStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var padR))
            {
                generator.Parameters.Barcode.Padding.Right.Point = padR;
            }

            // Apply bottom padding if present.
            if (settings.TryGetValue("PaddingBottom", out string padBStr) &&
                float.TryParse(padBStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var padB))
            {
                generator.Parameters.Barcode.Padding.Bottom.Point = padB;
            }

            // Apply image resolution if present.
            if (settings.TryGetValue("Resolution", out string resStr) &&
                float.TryParse(resStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var res))
            {
                generator.Parameters.Resolution = res;
            }

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }

    /// <summary>
    /// Reads a simple key=value configuration file, ignoring empty lines and comments,
    /// and returns a case‑insensitive dictionary of settings.
    /// </summary>
    /// <param name="path">Full path to the configuration file.</param>
    /// <returns>Dictionary containing configuration keys and their string values.</returns>
    static Dictionary<string, string> LoadConfig(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!File.Exists(path))
        {
            Console.WriteLine($"Configuration file not found: {path}");
            return dict;
        }

        foreach (var line in File.ReadAllLines(path))
        {
            // Skip blank lines and lines starting with '#'.
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            int idx = line.IndexOf('=');
            // Skip malformed lines.
            if (idx <= 0 || idx == line.Length - 1)
                continue;

            string key = line.Substring(0, idx).Trim();
            string value = line.Substring(idx + 1).Trim();
            dict[key] = value;
        }

        return dict;
    }
}