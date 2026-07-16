// Title: Generate GS1 Composite barcode with configurable linear component
// Description: Demonstrates reading a linear component type from a configuration file and creating a GS1 Composite barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to customize linear and 2D components. Developers often need to dynamically select symbologies based on configuration or user input, and this snippet illustrates that pattern.
// Prompt: Allow users to select linear component type via configuration file and generate corresponding GS1 Composite barcode.
// Tags: barcode symbology, gs1 composite, configuration, aspose.barcode, generation, png output

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads a linear component type from a configuration file
/// and generates a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a GS1 Composite barcode based on the
    /// linear component type specified in a configuration file.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can specify the config file path.</param>
    static void Main(string[] args)
    {
        // Determine configuration file path (first argument or default)
        string configPath = args.Length > 0 ? args[0] : "config.txt";

        // Default linear component type if configuration is missing or invalid
        BaseEncodeType linearComponent = EncodeTypes.GS1Code128;

        // Attempt to read the configuration file and resolve the symbology name
        if (File.Exists(configPath))
        {
            try
            {
                string symbologyName = File.ReadAllText(configPath).Trim();

                // Resolve symbology name to EncodeTypes field via reflection
                FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
                if (field != null && typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
                {
                    linearComponent = (BaseEncodeType)field.GetValue(null);
                }
                else
                {
                    Console.WriteLine($"Warning: Unknown symbology '{symbologyName}'. Using default GS1Code128.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}. Using default GS1Code128.");
            }
        }
        else
        {
            Console.WriteLine($"Configuration file '{configPath}' not found. Using default GS1Code128.");
        }

        // Sample GS1 Composite barcode text (1D and 2D parts separated by '|')
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Generate the barcode using the GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Apply the linear component type read from configuration
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearComponent;

            // Choose a 2D component type (CC_A is a common choice)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Example visual settings
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image to a PNG file
            string outputPath = "gs1composite.png";
            generator.Save(outputPath);
            Console.WriteLine($"GS1 Composite barcode saved to '{outputPath}'.");
        }
    }
}