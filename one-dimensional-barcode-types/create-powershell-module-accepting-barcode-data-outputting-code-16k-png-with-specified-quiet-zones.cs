using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 16K barcode with configurable quiet zone coefficients.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments to set the barcode data and quiet zone coefficients.
    /// </summary>
    /// <param name="args">
    /// args[0] – barcode text (default: "12345678901234567890")
    /// args[1] – left quiet zone coefficient (default: 10)
    /// args[2] – right quiet zone coefficient (default: 1)
    /// </param>
    static void Main(string[] args)
    {
        // Default values for barcode text and quiet zone coefficients
        string codeText = "12345678901234567890";
        int quietZoneLeft = 10;   // default left coefficient
        int quietZoneRight = 1;   // default right coefficient
        string outputPath = "code16k.png";

        // Parse command‑line arguments if provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            // Override barcode text with first argument
            codeText = args[0];
        }

        if (args.Length > 1 && int.TryParse(args[1], out int leftCoef) && leftCoef > 0)
        {
            // Override left quiet zone coefficient with second argument
            quietZoneLeft = leftCoef;
        }

        if (args.Length > 2 && int.TryParse(args[2], out int rightCoef) && rightCoef > 0)
        {
            // Override right quiet zone coefficient with third argument
            quietZoneRight = rightCoef;
        }

        // Create the barcode generator for Code 16K using the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply the quiet zone coefficients to the generator's parameters
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietZoneLeft;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietZoneRight;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath);
        }

        // Output information about the generated barcode
        Console.WriteLine($"Code 16K barcode saved to '{Path.GetFullPath(outputPath)}'.");
        Console.WriteLine($"Data: {codeText}");
        Console.WriteLine($"QuietZoneLeftCoef: {quietZoneLeft}, QuietZoneRightCoef: {quietZoneRight}");
    }
}