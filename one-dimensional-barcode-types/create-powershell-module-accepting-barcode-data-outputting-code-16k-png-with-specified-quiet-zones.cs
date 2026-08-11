// Title: Generate Code 16K barcode PNG with custom quiet zones
// Description: Demonstrates creating a Code 16K barcode image, configuring left and right quiet zone coefficients, and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Code16K. Developers commonly use these APIs to produce high‑density barcodes for packaging, inventory, and shipping labels, adjusting parameters such as quiet zones and aspect ratio to meet printing specifications.
// Prompt: Create PowerShell module accepting barcode data, outputting Code 16K PNG with specified quiet zones.
// Tags: barcode, code16k, generation, png, quietzone, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code 16K barcode image with configurable quiet zones
/// and saves it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments: barcode text, left quiet zone coefficient, right quiet zone coefficient.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Default barcode data and quiet zone coefficients
        string codeText = "1234567890";
        int quietLeft = 10;   // default left quiet zone coefficient
        int quietRight = 1;   // default right quiet zone coefficient

        // Parse command‑line arguments if provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && int.TryParse(args[1], out int left))
            quietLeft = left;

        if (args.Length > 2 && int.TryParse(args[2], out int right))
            quietRight = right;

        // Determine output file path (current directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code16k.png");

        try
        {
            // Initialize the barcode generator for Code 16K symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
            {
                // Apply quiet zone coefficients
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietLeft;
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietRight;

                // Optional: set aspect ratio (default is 1.0f)
                generator.Parameters.Barcode.Code16K.AspectRatio = 1f;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Code16K barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}