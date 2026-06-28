using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code16K barcode with configurable quiet zone coefficients.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments to set left and right quiet zone coefficients.
    /// </summary>
    /// <param name="args">Command‑line arguments: leftCoef rightCoef</param>
    static void Main(string[] args)
    {
        // Default quiet zone coefficients
        int leftCoef = 10;   // default left quiet zone coefficient
        int rightCoef = 1;   // default right quiet zone coefficient

        // Parse optional command‑line arguments: leftCoef rightCoef
        if (args.Length >= 2)
        {
            // Try to parse left coefficient; fallback to default on failure
            if (int.TryParse(args[0], out int parsedLeft) && parsedLeft > 0)
                leftCoef = parsedLeft;
            else
                Console.WriteLine($"Invalid left coefficient '{args[0]}', using default {leftCoef}.");

            // Try to parse right coefficient; fallback to default on failure
            if (int.TryParse(args[1], out int parsedRight) && parsedRight > 0)
                rightCoef = parsedRight;
            else
                Console.WriteLine($"Invalid right coefficient '{args[1]}', using default {rightCoef}.");
        }

        // Sample Code16K barcode text (must be numeric)
        const string codeText = "123456789012";

        // Generate the barcode with specified quiet zone coefficients
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply quiet zone coefficients to the generator parameters
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

            // Define output file path
            const string outputPath = "code16k.png";

            // Save the barcode image to a file
            generator.Save(outputPath);

            // Inform the user about the saved file and used coefficients
            Console.WriteLine($"Barcode saved to '{outputPath}'.");
            Console.WriteLine($"QuietZoneLeftCoef = {leftCoef}, QuietZoneRightCoef = {rightCoef}");
        }
    }
}