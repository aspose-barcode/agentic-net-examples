// Title: Generate DataBar barcodes from JSON specifications
// Description: This example reads a JSON file containing barcode parameters, creates DataBar barcode images using Aspose.BarCode, and saves them to a temporary folder.
// Category-Description: Demonstrates Aspose.BarCode generation for DataBar symbologies. It shows how to deserialize barcode specifications, map symbology names to EncodeTypes via reflection, configure barcode properties such as BarHeight and XDimension, and export images in PNG format. Useful for developers automating bulk barcode creation in .NET applications.
// Prompt: Develop script reading barcode specs from JSON, creating DataBar images, saving to folder.
// Tags: databar, barcode, generation, json, aspose.barcode, image, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents a barcode specification loaded from JSON.
/// </summary>
class BarcodeSpec
{
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public float? BarHeight { get; set; }
    public float? XDimension { get; set; }
    public string FileName { get; set; }
}

/// <summary>
/// Sample console application that generates DataBar barcodes from JSON specifications using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images based on JSON specs and writes them to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "DataBarOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare sample JSON specifications and write them to a file
        string jsonPath = Path.Combine(outputFolder, "specs.json");
        string sampleJson = @"[
  {
    ""Symbology"": ""DatabarOmniDirectional"",
    ""CodeText"": ""(01)12345678901231"",
    ""BarHeight"": 30,
    ""XDimension"": 2,
    ""FileName"": ""DatabarOmni.png""
  },
  {
    ""Symbology"": ""DatabarStackedOmniDirectional"",
    ""CodeText"": ""(01)12345678901231"",
    ""XDimension"": 2,
    ""FileName"": ""DatabarStacked.png""
  },
  {
    ""Symbology"": ""DatabarLimited"",
    ""CodeText"": ""(01)08888888888888"",
    ""BarHeight"": 40,
    ""XDimension"": 2,
    ""FileName"": ""DatabarLimited.png""
  }
]";
        File.WriteAllText(jsonPath, sampleJson);

        // Verify that the specification file exists
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine("Specification file not found.");
            return;
        }

        // Read and deserialize the JSON specifications
        string jsonContent = File.ReadAllText(jsonPath);
        List<BarcodeSpec> specs;
        try
        {
            specs = JsonSerializer.Deserialize<List<BarcodeSpec>>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Ensure we have at least one specification to process
        if (specs == null || specs.Count == 0)
        {
            Console.WriteLine("No barcode specifications found.");
            return;
        }

        // Process each specification
        foreach (var spec in specs)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(spec.Symbology) ||
                string.IsNullOrWhiteSpace(spec.CodeText) ||
                string.IsNullOrWhiteSpace(spec.FileName))
            {
                Console.WriteLine("Invalid specification entry; skipping.");
                continue;
            }

            // Resolve symbology name to BaseEncodeType via reflection
            FieldInfo field = typeof(EncodeTypes).GetField(spec.Symbology);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {spec.Symbology}; skipping.");
                continue;
            }

            BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
            if (encodeType == null)
            {
                Console.WriteLine($"Failed to obtain encode type for: {spec.Symbology}; skipping.");
                continue;
            }

            // Determine the full output path for the image
            string outputPath = Path.Combine(outputFolder, spec.FileName);

            try
            {
                // Create the barcode generator with the resolved type and code text
                using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
                {
                    // Apply optional BarHeight if provided
                    if (spec.BarHeight.HasValue && spec.BarHeight.Value > 0)
                    {
                        generator.Parameters.Barcode.BarHeight.Point = spec.BarHeight.Value;
                    }

                    // Apply optional XDimension if provided
                    if (spec.XDimension.HasValue && spec.XDimension.Value > 0)
                    {
                        generator.Parameters.Barcode.XDimension.Point = spec.XDimension.Value;
                    }

                    // Save the generated barcode as a PNG image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Saved barcode to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for {spec.FileName}: {ex.Message}");
            }
        }

        Console.WriteLine($"All done. Images are located in: {outputFolder}");
    }
}