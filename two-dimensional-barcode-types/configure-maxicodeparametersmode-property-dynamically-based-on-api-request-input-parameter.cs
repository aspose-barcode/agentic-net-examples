// Title: Dynamic MaxiCode Mode Configuration Example
// Description: Demonstrates how to set MaxiCodeParameters.Mode at runtime based on an input string, useful for API-driven barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It shows how to use BarcodeGenerator, EncodeTypes, and MaxiCodeParameters to configure mode dynamically, a common requirement when building services that generate barcodes from client-supplied parameters. Developers often need to parse request data and map it to the appropriate MaxiCodeMode enum before rendering the image.
// Prompt: Configure MaxiCodeParameters.Mode property dynamically based on an API request input parameter.
// Tags: maxicode, barcode generation, dynamic configuration, apibarcode, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates dynamic configuration of MaxiCode mode based on a simulated API request.
/// </summary>
class Program
{
    /// <summary>
    /// Converts a string (e.g., "Mode4" or "4") to the corresponding <see cref="MaxiCodeMode"/> enum value.
    /// </summary>
    /// <param name="modeString">The mode string supplied by the caller.</param>
    /// <returns>The parsed <see cref="MaxiCodeMode"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the input cannot be parsed to a valid mode.</exception>
    static MaxiCodeMode ParseMode(string modeString)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(modeString))
            throw new ArgumentException("Mode string cannot be null or empty.");

        // Try to parse by enum name (e.g., "Mode4")
        if (Enum.TryParse<MaxiCodeMode>(modeString, true, out var modeByName))
            return modeByName;

        // Try to parse numeric value (e.g., "4")
        if (int.TryParse(modeString, out int numeric))
        {
            if (Enum.IsDefined(typeof(MaxiCodeMode), numeric))
                return (MaxiCodeMode)numeric;
        }

        // If parsing fails, report an error
        throw new ArgumentException($"Invalid MaxiCode mode: {modeString}");
    }

    /// <summary>
    /// Entry point. Parses requested mode, generates a MaxiCode barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Simulated API request parameter – change to test other modes (e.g., "Mode2", "3")
        string requestedMode = "Mode4";

        try
        {
            // Convert the request string to a MaxiCodeMode enum value
            MaxiCodeMode mode = ParseMode(requestedMode);
            Console.WriteLine($"Parsed mode: {mode}");

            // Simple codetext for demonstration; complex codetext may be required for modes 2/3
            string codeText = "Sample MaxiCode";

            // Initialize the barcode generator for MaxiCode symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                // Dynamically set the MaxiCode mode based on the parsed value
                generator.Parameters.Barcode.MaxiCode.Mode = mode;

                // Optional: adjust additional visual parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 10f;

                // Save the generated barcode image to a temporary file
                string outputPath = Path.Combine(Path.GetTempPath(), $"MaxiCode_{mode}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}