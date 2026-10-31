// Title: Generate DataBar barcodes from JSON specifications
// Description: This example reads barcode parameters from a JSON file, creates DataBar barcode images using Aspose.BarCode, and saves them to a temporary folder.
// Category-Description: Demonstrates Aspose.BarCode generation of DataBar symbologies. It covers reading configuration data, using EncodeTypes via reflection, setting barcode parameters (XDimension, BarHeight, AspectRatio, GS1 encoding, composite component), and exporting PNG images. Ideal for developers needing batch barcode creation or dynamic barcode generation based on external data sources.
// Prompt: Develop script reading barcode specs from JSON, creating DataBar images, saving to folder.
// Tags: databar, barcode generation, json, aspose.barcode, png, encode types, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Represents a barcode specification loaded from JSON.
/// </summary>
class BarcodeSpec
{
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public float? XDimension { get; set; }
    public float? BarHeight { get; set; }
    public int? AspectRatio { get; set; }
    public bool? AllowOnlyGS1Encoding { get; set; }
    public bool? Is2DCompositeComponent { get; set; }
}

/// <summary>
/// Demonstrates reading barcode specifications from JSON, generating DataBar barcodes, and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Prepares sample JSON if missing, reads specifications, generates barcodes, and writes them to an output directory.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a JSON specification file in the temp folder (create sample if absent)
        // --------------------------------------------------------------------
        string jsonPath = Path.Combine(Path.GetTempPath(), "databar_specs.json");
        if (!File.Exists(jsonPath))
        {
            var sampleSpecs = new List<BarcodeSpec>
            {
                new BarcodeSpec
                {
                    Symbology = "DatabarExpanded",
                    CodeText = "(01)12345678901231",
                    XDimension = 2f,
                    BarHeight = 30f,
                    AllowOnlyGS1Encoding = true
                },
                new BarcodeSpec
                {
                    Symbology = "DatabarStackedOmniDirectional",
                    CodeText = "(01)12345678901231",
                    XDimension = 2f,
                    AspectRatio = 15,
                    BarHeight = 40f,
                    AllowOnlyGS1Encoding = false
                },
                new BarcodeSpec
                {
                    Symbology = "DatabarOmniDirectional",
                    CodeText = "(01)12345678901231",
                    XDimension = 2f,
                    BarHeight = 30f,
                    Is2DCompositeComponent = true
                }
            };
            string json = JsonSerializer.Serialize(sampleSpecs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
        }

        // --------------------------------------------------------------------
        // Read the JSON specifications into a list of BarcodeSpec objects
        // --------------------------------------------------------------------
        string jsonContent = File.ReadAllText(jsonPath);
        List<BarcodeSpec> specs = JsonSerializer.Deserialize<List<BarcodeSpec>>(jsonContent);

        // --------------------------------------------------------------------
        // Create a unique output folder for the generated barcode images
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "DataBarOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        int index = 0;
        foreach (var spec in specs)
        {
            index++;

            // Resolve the symbology name to the corresponding EncodeTypes field via reflection
            FieldInfo field = typeof(EncodeTypes).GetField(spec.Symbology);
            if (field == null)
            {
                Console.WriteLine($"[Warning] Unknown symbology: {spec.Symbology}. Skipping.");
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // ----------------------------------------------------------------
            // Generate the barcode using the resolved encode type and supplied text
            // ----------------------------------------------------------------
            using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
            {
                // Apply optional parameters if they are present in the JSON spec
                if (spec.XDimension.HasValue)
                {
                    generator.Parameters.Barcode.XDimension.Pixels = spec.XDimension.Value;
                }
                if (spec.BarHeight.HasValue)
                {
                    generator.Parameters.Barcode.BarHeight.Pixels = spec.BarHeight.Value;
                }
                if (spec.AspectRatio.HasValue)
                {
                    generator.Parameters.Barcode.DataBar.AspectRatio = spec.AspectRatio.Value;
                }
                if (spec.AllowOnlyGS1Encoding.HasValue)
                {
                    generator.Parameters.Barcode.DataBar.AllowOnlyGS1Encoding = spec.AllowOnlyGS1Encoding.Value;
                }
                if (spec.Is2DCompositeComponent.HasValue)
                {
                    generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = spec.Is2DCompositeComponent.Value;
                }

                // Save the generated barcode as a PNG file
                string fileName = $"{spec.Symbology}_{index}.png";
                string filePath = Path.Combine(outputDir, fileName);
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated: {filePath}");
            }
        }

        Console.WriteLine($"All barcodes saved to: {outputDir}");
    }
}