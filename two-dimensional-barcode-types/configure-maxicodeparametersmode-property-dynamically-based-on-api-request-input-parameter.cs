// Title: Dynamic MaxiCode Mode Configuration Based on Input Parameter
// Description: Demonstrates how to set the MaxiCode barcode mode at runtime using a value supplied via command‑line arguments, mimicking an API request.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on MaxiCode symbology. It showcases the use of EncodeTypes, BarcodeGenerator, and MaxiCodeParameters to create barcodes. Typical scenarios include e‑commerce shipping labels and logistics where MaxiCode mode must be chosen dynamically based on external data. Developers often need to parse input, validate enum values, and fall back to safe defaults when generating barcodes.
// Prompt: Configure MaxiCodeParameters.Mode property dynamically based on an API request input parameter.
// Tags: maxicode, barcode, generation, dynamic, mode, aspose.barcode, png, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode whose mode is selected at runtime based on a supplied argument.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Accepts an optional command‑line argument that represents the desired MaxiCode mode.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is interpreted as the requested MaxiCode mode.</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Simulate receiving a mode value from an external API request.
        // ------------------------------------------------------------
        string requestedMode = args.Length > 0 ? args[0] : "Mode4";

        // ------------------------------------------------------------
        // Try to convert the supplied string to the MaxiCodeMode enum.
        // If conversion fails, fall back to a safe default (Mode4).
        // ------------------------------------------------------------
        if (!Enum.TryParse<MaxiCodeMode>(requestedMode, true, out MaxiCodeMode mode))
        {
            Console.WriteLine($"Invalid MaxiCode mode '{requestedMode}'. Falling back to Mode4.");
            mode = MaxiCodeMode.Mode4;
        }

        // ------------------------------------------------------------
        // Modes 2 and 3 require structured codetext that this demo does not provide.
        // Replace them with Mode4 to ensure successful generation.
        // ------------------------------------------------------------
        if (mode == MaxiCodeMode.Mode2 || mode == MaxiCodeMode.Mode3)
        {
            Console.WriteLine($"Mode '{mode}' requires structured codetext. Using Mode4 instead.");
            mode = MaxiCodeMode.Mode4;
        }

        // ------------------------------------------------------------
        // Prepare a unique temporary folder to store the generated image.
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "maxicode.png");

        // ------------------------------------------------------------
        // Create the barcode generator, assign the selected mode, and save as PNG.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            generator.Parameters.Barcode.MaxiCode.Mode = mode;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Inform the user about the result.
        // ------------------------------------------------------------
        Console.WriteLine($"MaxiCode barcode generated with mode '{mode}'. Saved to: {outputPath}");
    }
}