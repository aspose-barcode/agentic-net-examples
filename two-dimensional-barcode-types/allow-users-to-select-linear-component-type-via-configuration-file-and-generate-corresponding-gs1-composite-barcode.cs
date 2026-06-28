using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Composite barcode using Aspose.BarCode,
/// with the linear component type configurable via a text file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads configuration, resolves the linear component type, and generates a GS1 Composite barcode image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Configuration: read the linear component type name from a file.
        // --------------------------------------------------------------------
        const string configPath = "config.txt";                     // Path to the configuration file.
        BaseEncodeType defaultLinearType = EncodeTypes.GS1Code128; // Fallback type if config is missing/invalid.

        string linearTypeName = null;
        if (File.Exists(configPath))
        {
            try
            {
                // Read the entire file content and trim whitespace.
                linearTypeName = File.ReadAllText(configPath).Trim();
            }
            catch (Exception ex)
            {
                // Report any I/O errors but continue with the default type.
                Console.WriteLine($"Error reading config file: {ex.Message}");
            }
        }
        else
        {
            // Inform the user that the config file was not found.
            Console.WriteLine("Config file not found. Using default linear component type.");
        }

        // --------------------------------------------------------------------
        // Resolve the type name to a BaseEncodeType using reflection.
        // --------------------------------------------------------------------
        BaseEncodeType linearComponentType = defaultLinearType;
        if (!string.IsNullOrEmpty(linearTypeName))
        {
            var field = typeof(EncodeTypes).GetField(linearTypeName);
            if (field != null && typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
            {
                // Successful resolution – assign the configured type.
                linearComponentType = (BaseEncodeType)field.GetValue(null);
            }
            else
            {
                // Unknown or unsupported type – fall back to default.
                Console.WriteLine($"Unknown or unsupported linear component type '{linearTypeName}'. Using default.");
            }
        }

        // --------------------------------------------------------------------
        // Barcode data and output settings.
        // --------------------------------------------------------------------
        const string codeText = "(01)03212345678906|(21)A12345678"; // GS1 Composite codetext (1D|2D parts).
        const string outputPath = "gs1composite.png";              // Destination image file.

        // --------------------------------------------------------------------
        // Generate the barcode using Aspose.BarCode.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Apply the linear component type resolved from configuration.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearComponentType;

            // Choose a 2D component type (CC_A is a common choice for GS1 Composite).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: adjust visual dimensions.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // Width of a single module.
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Height of the linear component.

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // --------------------------------------------------------------------
        // Inform the user of successful generation.
        // --------------------------------------------------------------------
        Console.WriteLine($"GS1 Composite barcode generated with linear component '{linearComponentType.GetType().Name}'. Saved to '{outputPath}'.");
    }
}