// Title: Dynamic MaxiCode Mode Selection Based on Input Parameter
// Description: Demonstrates how to set the MaxiCode barcode mode at runtime using a value supplied via command‑line arguments, mimicking an API request.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCodeParameters to adjust the Mode property dynamically. Developers often need to generate MaxiCode barcodes with different modes (e.g., Mode 2‑6) based on external input such as API parameters, making this pattern useful for web services and batch processing.
// Prompt: Configure MaxiCodeParameters.Mode property dynamically based on an API request input parameter.
// Tags: barcode, maxicode, mode, dynamic, generation, aspnet, aspose.barcode, encode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode with a mode selected at runtime based on a supplied argument.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Accepts an optional command‑line argument that specifies the desired MaxiCode mode.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is interpreted as the requested MaxiCode mode.</param>
    static void Main(string[] args)
    {
        // Simulated API request parameter for MaxiCode mode (e.g., "2", "3", "4", "5", "6")
        string requestedMode = args.Length > 0 ? args[0] : "4";

        // Try to parse the requested mode to the MaxiCodeMode enum; fall back to Mode4 if parsing fails
        if (!Enum.TryParse<MaxiCodeMode>(requestedMode, ignoreCase: true, out var mode))
        {
            Console.WriteLine($"Invalid MaxiCode mode '{requestedMode}'. Falling back to default mode 4.");
            mode = MaxiCodeMode.Mode4;
        }

        // Sample code text for the barcode
        string codeText = "Sample MaxiCode";

        // Create a temporary output path for the generated PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCodeDynamicMode.png");

        // Generate the MaxiCode barcode with the selected mode
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Apply the dynamically determined mode
            generator.Parameters.Barcode.MaxiCode.Mode = mode;

            // Optional: adjust module size for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the barcode image to the specified file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode generated with mode {mode} at: {outputPath}");
    }
}