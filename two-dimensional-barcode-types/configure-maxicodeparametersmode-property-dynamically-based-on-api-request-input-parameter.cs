// Title: Dynamic MaxiCode Mode Configuration Example
// Description: Demonstrates how to set MaxiCodeParameters.Mode at runtime based on an input argument, useful for API-driven barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, MaxiCodeParameters, and EncodeTypes to create MaxiCode images. Developers often need to adjust barcode settings such as mode dynamically in web or service APIs, and this snippet illustrates that common scenario.
// Prompt: Configure MaxiCodeParameters.Mode property dynamically based on an API request input parameter.
// Tags: maxicode, barcode, generation, dynamic-configuration, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates dynamic configuration of MaxiCode mode based on input parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a mode name from command‑line arguments, validates it, and generates a MaxiCode barcode image using the selected mode.
    /// </summary>
    /// <param name="args">Command‑line arguments where the first element may specify a <see cref="MaxiCodeMode"/> value.</param>
    static void Main(string[] args)
    {
        // Determine the desired MaxiCode mode from the first command‑line argument.
        // If not provided or invalid, default to Mode4.
        string modeInput = args.Length > 0 ? args[0] : "Mode4";

        // Try to parse the input string to a MaxiCodeMode enum value (case‑insensitive).
        if (!Enum.TryParse<MaxiCodeMode>(modeInput, true, out MaxiCodeMode selectedMode))
        {
            // Inform the user about the invalid input and fall back to the default mode.
            Console.WriteLine($"Invalid mode \"{modeInput}\". Falling back to default Mode4.");
            selectedMode = MaxiCodeMode.Mode4;
        }

        // Create a BarcodeGenerator for MaxiCode with a sample message.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Apply the selected mode via the MaxiCode parameters.
            generator.Parameters.Barcode.MaxiCode.Mode = selectedMode;

            // Define the output file path for the generated PNG image.
            string outputPath = "maxicode.png";

            // Save the barcode image to the specified file.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Notify the user of successful generation.
            Console.WriteLine($"MaxiCode generated with mode {selectedMode} and saved to \"{outputPath}\".");
        }
    }
}