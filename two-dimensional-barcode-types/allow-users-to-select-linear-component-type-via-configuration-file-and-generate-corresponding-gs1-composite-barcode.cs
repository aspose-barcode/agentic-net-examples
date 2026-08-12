// Title: Generate GS1 Composite Barcode with Configurable Linear Component Type
// Description: Demonstrates reading a linear component symbology from a configuration file and generating a GS1 Composite barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure composite barcodes. It shows usage of BarcodeGenerator, EncodeTypes, GS1CompositeBar parameters, and reflection to map configuration values to BaseEncodeType. Developers often need to create GS1 Composite symbols with different linear components for inventory and logistics applications.
// Prompt: Allow users to select linear component type via configuration file and generate corresponding GS1 Composite barcode.
// Tags: gs1 composite barcode, linear component, configuration, aspose.barcode, barcode generation, c#

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Composite barcode where the linear component type is read from a configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads configuration, builds the barcode data, and saves the image.
    /// </summary>
    static void Main()
    {
        // Path to the configuration file that contains the linear component type name.
        const string configFile = "linearComponentConfig.txt";

        // Default linear component type if configuration is missing or invalid.
        BaseEncodeType linearComponentType = EncodeTypes.GS1Code128;

        // Attempt to read the linear component type from the config file.
        if (File.Exists(configFile))
        {
            try
            {
                string configValue = File.ReadAllText(configFile).Trim();

                if (!string.IsNullOrEmpty(configValue))
                {
                    // Resolve the symbology name to a BaseEncodeType using reflection.
                    FieldInfo field = typeof(EncodeTypes).GetField(configValue, BindingFlags.Public | BindingFlags.Static);
                    if (field != null && typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
                    {
                        linearComponentType = (BaseEncodeType)field.GetValue(null);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Unknown symbology '{configValue}'. Using default GS1Code128.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}. Using default GS1Code128.");
            }
        }
        else
        {
            Console.WriteLine($"Configuration file '{configFile}' not found. Using default GS1Code128.");
        }

        // Sample GS1 Composite barcode data.
        // Linear component (14‑digit GTIN) – AI (01) requires exactly 14 digits.
        string linearComponent = "(01)00123456789012"; // 14‑digit GTIN (padded with leading zeros if needed)

        // 2D component – any additional AI, e.g., (21) for serial number.
        string twoDComponent = "(21)A12345678";

        // Combine linear and 2D parts with the required '|' separator.
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Output file for the generated barcode image.
        const string outputFile = "gs1composite.png";

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component type based on configuration.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearComponentType;

            // Choose a 2D component type (CC_A is a common choice).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings.
            generator.Parameters.Barcode.XDimension.Point = 2f;      // Module size.
            generator.Parameters.Barcode.BarHeight.Point = 100f;    // Height of the linear part.

            // Save the barcode image.
            generator.Save(outputFile);
        }

        Console.WriteLine($"GS1 Composite barcode generated and saved to '{outputFile}'.");
    }
}