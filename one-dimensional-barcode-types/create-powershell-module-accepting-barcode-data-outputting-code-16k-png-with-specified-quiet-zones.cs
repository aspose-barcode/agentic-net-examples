// Title: Generate Code 16K barcode PNG with custom quiet zones
// Description: This example creates a Code 16K barcode image in PNG format, allowing the user to specify the barcode data and quiet‑zone coefficients via command‑line arguments.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.Code16K. Typical scenarios include creating high‑density barcodes for inventory, shipping labels, or product packaging where precise quiet‑zone control is required. Developers often need to adjust X‑dimension and quiet‑zone settings to meet scanner specifications, making this example a useful reference for similar barcode creation tasks.
// Prompt: Create PowerShell module accepting barcode data, outputting Code 16K PNG with specified quiet zones.
// Tags: barcode, code16k, generation, png, quietzone, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code 16K barcode PNG file with configurable quiet‑zone coefficients.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments, validates parameters, and saves the barcode image.
    /// </summary>
    /// <param name="args">
    /// Expected arguments:
    /// 0 – barcode text (default: "Aspose.Barcode")
    /// 1 – left quiet‑zone coefficient (default: 10, minimum: 10)
    /// 2 – right quiet‑zone coefficient (default: 10, minimum: 1)
    /// 3 – output file path (default: current directory + "Code16K.png")
    /// </param>
    static void Main(string[] args)
    {
        // Default values for barcode generation
        string codeText = "Aspose.Barcode";
        int quietLeft = 10;
        int quietRight = 10;
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16K.png");

        // --------------------------------------------------------------------
        // Parse command‑line arguments if they are provided
        // --------------------------------------------------------------------
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && int.TryParse(args[1], out int left))
            quietLeft = left;

        if (args.Length > 2 && int.TryParse(args[2], out int right))
            quietRight = right;

        if (args.Length > 3 && !string.IsNullOrWhiteSpace(args[3]))
            outputPath = args[3];

        // --------------------------------------------------------------------
        // Validate quiet‑zone coefficients (Aspose requires minimum values)
        // --------------------------------------------------------------------
        if (quietLeft < 10)
            throw new ArgumentOutOfRangeException(nameof(quietLeft), "QuietZoneLeftCoef must be >= 10.");

        if (quietRight < 1)
            throw new ArgumentOutOfRangeException(nameof(quietRight), "QuietZoneRightCoef must be >= 1.");

        // --------------------------------------------------------------------
        // Ensure the output directory exists before saving the image
        // --------------------------------------------------------------------
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // --------------------------------------------------------------------
        // Generate the Code 16K barcode using Aspose.BarCode
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set the X‑dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply custom quiet‑zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietLeft;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietRight;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}