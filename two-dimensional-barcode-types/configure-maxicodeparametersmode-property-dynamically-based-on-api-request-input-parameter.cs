using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a mode selected via command‑line argument.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument specifies the MaxiCode mode.</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Determine the desired MaxiCode mode from the first argument.
        // If no argument is supplied, default to "4" (Mode4).
        // ------------------------------------------------------------
        string input = args.Length > 0 ? args[0] : "4";

        // ------------------------------------------------------------
        // Try to parse the input string to the MaxiCodeMode enum.
        // Accept only the supported modes (2‑6). If parsing fails or an
        // unsupported mode is supplied, fall back to Mode4.
        // ------------------------------------------------------------
        if (!Enum.TryParse<MaxiCodeMode>(input, ignoreCase: true, out var mode) ||
            (mode != MaxiCodeMode.Mode2 && mode != MaxiCodeMode.Mode3 &&
             mode != MaxiCodeMode.Mode4 && mode != MaxiCodeMode.Mode5 &&
             mode != MaxiCodeMode.Mode6))
        {
            Console.WriteLine($"Invalid mode '{input}'. Falling back to default Mode4.");
            mode = MaxiCodeMode.Mode4;
        }

        // ------------------------------------------------------------
        // Create a barcode generator for MaxiCode with sample text.
        // The generator is disposed automatically via the using block.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Apply the selected MaxiCode mode to the generator's parameters.
            generator.Parameters.Barcode.MaxiCode.Mode = mode;

            // Define the output file path (current directory) and save the image as PNG.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user of the successful generation.
            Console.WriteLine($"MaxiCode barcode generated with mode {mode} and saved to: {outputPath}");
        }
    }
}