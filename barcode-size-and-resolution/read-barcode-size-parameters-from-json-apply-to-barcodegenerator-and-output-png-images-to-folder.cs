// Title: Generate Barcodes from JSON Size Parameters and Save as PNG
// Description: Demonstrates reading barcode size settings from a JSON file, configuring Aspose.BarCode Generator accordingly, and saving the resulting images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with custom size and padding parameters. It covers reading configuration data, setting XDimension, bar height, image dimensions, padding, and QR version via the API classes EncodeTypes, BaseEncodeType, QRVersion, and BarCodeImageFormat. Developers often need to programmatically create barcodes with precise dimensions for printing, labeling, or embedding in documents.
// Prompt: Read barcode size parameters from JSON, apply to BarcodeGenerator, and output PNG images to a folder.
// Tags: barcode, size, json, png, aspose.barcode, generation, encode types, qr, code128

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents the set of size and layout parameters for a barcode to be generated.
/// </summary>
public class BarcodeSizeParams
{
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public float? XDimensionPixels { get; set; }
    public float? BarHeightPoints { get; set; }
    public float? ImageWidthPixels { get; set; }
    public float? ImageHeightPixels { get; set; }
    public float? PaddingLeftPoints { get; set; }
    public float? PaddingTopPoints { get; set; }
    public float? PaddingRightPoints { get; set; }
    public float? PaddingBottomPoints { get; set; }
    public string QRVersion { get; set; }
}

/// <summary>
/// Main program class that reads barcode configuration from JSON, generates barcodes, and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes the JSON file, creates barcodes, and writes PNG images to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Path to the JSON file containing barcode parameters
        string jsonPath = "barcodeParams.json";
        List<BarcodeSizeParams> paramList;

        // Load parameters from JSON if the file exists; otherwise use sample defaults
        if (File.Exists(jsonPath))
        {
            try
            {
                string json = File.ReadAllText(jsonPath);
                paramList = JsonSerializer.Deserialize<List<BarcodeSizeParams>>(json);
                if (paramList == null) paramList = new List<BarcodeSizeParams>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read JSON: {ex.Message}");
                paramList = new List<BarcodeSizeParams>();
            }
        }
        else
        {
            // Sample default parameters for demonstration purposes
            paramList = new List<BarcodeSizeParams>
            {
                new BarcodeSizeParams
                {
                    Symbology = "Code128",
                    CodeText = "Sample123",
                    XDimensionPixels = 3f,
                    BarHeightPoints = 50f,
                    ImageWidthPixels = 300f,
                    ImageHeightPixels = 150f,
                    PaddingLeftPoints = 5f,
                    PaddingTopPoints = 5f,
                    PaddingRightPoints = 5f,
                    PaddingBottomPoints = 5f
                },
                new BarcodeSizeParams
                {
                    Symbology = "QR",
                    CodeText = "https://example.com",
                    XDimensionPixels = 4f,
                    QRVersion = "Version05"
                }
            };
        }

        // Create a unique temporary output folder for the generated PNG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Iterate over each parameter set and generate the corresponding barcode
        for (int i = 0; i < paramList.Count; i++)
        {
            var p = paramList[i];

            // Validate that a symbology is provided
            if (string.IsNullOrWhiteSpace(p.Symbology))
            {
                Console.WriteLine($"Item {i}: Symbology is missing, skipping.");
                continue;
            }

            // Resolve the symbology string to an EncodeTypes field
            var field = typeof(EncodeTypes).GetField(p.Symbology);
            if (field == null)
            {
                Console.WriteLine($"Item {i}: Unknown symbology '{p.Symbology}', skipping.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string codeText = p.CodeText ?? string.Empty;

            // Initialize the barcode generator with the resolved type and code text
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply optional size and padding parameters if they are provided
                if (p.XDimensionPixels.HasValue)
                    generator.Parameters.Barcode.XDimension.Pixels = p.XDimensionPixels.Value;

                if (p.BarHeightPoints.HasValue)
                    generator.Parameters.Barcode.BarHeight.Point = p.BarHeightPoints.Value;

                if (p.ImageWidthPixels.HasValue)
                    generator.Parameters.ImageWidth.Pixels = p.ImageWidthPixels.Value;

                if (p.ImageHeightPixels.HasValue)
                    generator.Parameters.ImageHeight.Pixels = p.ImageHeightPixels.Value;

                if (p.PaddingLeftPoints.HasValue)
                    generator.Parameters.Barcode.Padding.Left.Point = p.PaddingLeftPoints.Value;
                if (p.PaddingTopPoints.HasValue)
                    generator.Parameters.Barcode.Padding.Top.Point = p.PaddingTopPoints.Value;
                if (p.PaddingRightPoints.HasValue)
                    generator.Parameters.Barcode.Padding.Right.Point = p.PaddingRightPoints.Value;
                if (p.PaddingBottomPoints.HasValue)
                    generator.Parameters.Barcode.Padding.Bottom.Point = p.PaddingBottomPoints.Value;

                // QR version handling (only applicable for QR symbology)
                if (!string.IsNullOrWhiteSpace(p.QRVersion) && encodeType == EncodeTypes.QR)
                {
                    var versionField = typeof(QRVersion).GetField(p.QRVersion);
                    if (versionField != null)
                    {
                        QRVersion version = (QRVersion)versionField.GetValue(null);
                        generator.Parameters.Barcode.QR.Version = version;
                    }
                }

                // Construct the output file name and save the barcode as PNG
                string fileName = $"barcode_{i}_{p.Symbology}.png";
                string filePath = Path.Combine(outputFolder, fileName);
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved: {filePath}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}