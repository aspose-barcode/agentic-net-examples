// Title: Generate MaxiCode Barcode Image
// Description: Demonstrates how to generate a MaxiCode barcode using Aspose.BarCode and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and MaxiCode parameters to create shipping and logistics barcodes. Developers often need to produce MaxiCode images for parcel tracking, inventory, and data matrix applications, and this snippet illustrates the typical API workflow.
// Prompt: Create an ASP.NET MVC action that returns a generated MaxiCode barcode image based on query string parameters.
// Tags: barcode, maxicode, generation, image, aspnet-mvc, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that mimics the core logic of an ASP.NET MVC action.
    /// Accepts optional command‑line arguments for the barcode text and MaxiCode mode.
    /// </summary>
    static void Main(string[] args)
    {
        // Default barcode text and mode; these would normally come from the request query string in an MVC controller.
        string codeText = "Åspóse.Barcóde©";
        int modeNumber = 4; // Default to Mode4

        // Override defaults with command‑line arguments if provided.
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && int.TryParse(args[1], out int parsedMode))
            modeNumber = parsedMode;

        // Map the numeric mode to the corresponding MaxiCodeMode enum value.
        MaxiCodeMode maxiMode;
        switch (modeNumber)
        {
            case 2: maxiMode = MaxiCodeMode.Mode2; break;
            case 3: maxiMode = MaxiCodeMode.Mode3; break;
            case 4: maxiMode = MaxiCodeMode.Mode4; break;
            case 5: maxiMode = MaxiCodeMode.Mode5; break;
            case 6: maxiMode = MaxiCodeMode.Mode6; break;
            default: maxiMode = MaxiCodeMode.Mode4; break;
        }

        // Determine a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set visual parameters: pixel size, mode, and aspect ratio.
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.MaxiCode.Mode = maxiMode;
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated image (useful for debugging or console testing).
        Console.WriteLine($"MaxiCode barcode generated and saved to: {outputPath}");
    }
}