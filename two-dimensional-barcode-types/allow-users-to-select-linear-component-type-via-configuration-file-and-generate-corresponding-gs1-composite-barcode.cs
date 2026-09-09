// Title: Generate GS1 Composite Barcode with Configurable Linear Component
// Description: Demonstrates reading a configuration file to select the linear component type and generating a GS1 Composite barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create GS1 Composite barcodes. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and GS1CompositeBar, covering typical scenarios where developers need to combine linear and 2‑D components, customize encoding types, and output images. Ideal for developers implementing product labeling, inventory tracking, or any GS1‑compliant barcode solutions.
// Prompt: Allow users to select linear component type via configuration file and generate corresponding GS1 Composite barcode.
// Tags: barcode symbology, gs1 composite, configuration, csharp, aspose.barcode, generation, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads a configuration file to determine the linear component type
/// and generates a GS1 Composite barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads configuration, resolves the linear component, builds the full GS1 Composite code,
    /// and saves the generated barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the path of the configuration file located alongside the executable.
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");
        string linearComponentName = "EAN13"; // Default linear component if config is missing or invalid.

        // Attempt to read the configuration file and extract the LinearComponent value.
        if (File.Exists(configPath))
        {
            try
            {
                string[] lines = File.ReadAllLines(configPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("LinearComponent=", StringComparison.OrdinalIgnoreCase))
                    {
                        linearComponentName = line.Substring("LinearComponent=".Length).Trim();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading config file: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Config file not found. Using default linear component: EAN13");
        }

        // Resolve the EncodeTypes field that matches the requested linear component via reflection.
        FieldInfo field = typeof(EncodeTypes).GetField(linearComponentName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown linear component type '{linearComponentName}'. Falling back to EAN13.");
            field = typeof(EncodeTypes).GetField("EAN13", BindingFlags.Public | BindingFlags.Static);
        }
        BaseEncodeType linearEncodeType = (BaseEncodeType)field.GetValue(null);

        // Provide sample linear code text for each supported component type.
        string linearCodeText = linearComponentName switch
        {
            "EAN8" => "12345670",
            "UPCA" => "001234567895",
            "EAN13" => "2001234567893",
            "UPCE" => "04252614",
            "GS1Code128" => "(01)12345678901231",
            "DatabarOmniDirectional" => "(01)24012345678905",
            "DatabarStackedOmniDirectional" => "(01)24012345678905",
            "DatabarStacked" => "(01)24012345678905",
            "DatabarLimited" => "(01)24012345678905",
            "DatabarExpanded" => "(01)24012345678905",
            "DatabarExpandedStacked" => "(01)24012345678905",
            _ => "2001234567893" // Fallback for unknown types.
        };

        // Sample 2‑D component text (non‑GS1 mode) to be combined with the linear part.
        string twoDComponentText = "(10)ABCD0123(240)0123456789";

        // Concatenate linear and 2‑D components using the GS1 Composite separator.
        string fullCodeText = $"{linearCodeText}|{twoDComponentText}";

        // Define the output file path for the generated barcode image.
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GS1Composite.png");

        // Create and configure the barcode generator for a GS1 Composite barcode.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set module size.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide human‑readable text.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A; // Choose 2‑D component type.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearEncodeType; // Apply selected linear type.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false; // Permit non‑GS1 data in 2‑D component.

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"GS1 Composite barcode generated with linear component '{linearComponentName}'. Saved to: {outputPath}");
    }
}