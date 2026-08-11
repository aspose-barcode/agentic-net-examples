// Title: Generate Code16K Barcode with Configurable Quiet Zone Coefficients
// Description: Demonstrates how to create a Code16K barcode using Aspose.BarCode, allowing the left and right quiet zone coefficients to be set via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to configure barcode parameters such as quiet zone coefficients and X‑dimension using the BarcodeGenerator class. Typical use cases include customizing barcode appearance for printing or display, where developers need to control margins and module size.
// Prompt: Implement UI allowing users to set quiet zone left and right coefficients, preview barcode.
// Tags: barcode, code16k, quiet zone, generation, aspose.barcode, aspose.drawing, console, command-line

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program demonstrating generation of a Code16K barcode with adjustable quiet zone coefficients.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments for quiet zone left and right coefficients,
    /// generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Simulate a UI by accepting optional command‑line arguments.
        // If not provided, default safe values are used.
        int quietZoneLeft = 10;   // minimum allowed value for left quiet zone
        int quietZoneRight = 1;   // minimum allowed value for right quiet zone

        // Parse command‑line arguments: first = left coefficient, second = right coefficient
        string[] args = Environment.GetCommandLineArgs();
        if (args.Length > 1 && int.TryParse(args[1], out int left) && left >= 10)
            quietZoneLeft = left;
        if (args.Length > 2 && int.TryParse(args[2], out int right) && right >= 1)
            quietZoneRight = right;

        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code16k.png");

        try
        {
            // Create a barcode generator for Code16K symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
            {
                // Set the data to encode
                generator.CodeText = "12345678901234567890";

                // Apply quiet zone coefficients (must satisfy minimum constraints)
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietZoneLeft;
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietZoneRight;

                // Optionally adjust module size for better visibility
                generator.Parameters.Barcode.XDimension.Point = 2f; // 2 points per module

                // Generate the barcode image (Aspose.Drawing.Bitmap)
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the bitmap to a PNG file using Aspose.Drawing.Imaging.ImageFormat
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }

            // Inform the user about successful generation
            Console.WriteLine("Barcode generated successfully:");
            Console.WriteLine($"  QuietZoneLeftCoef  = {quietZoneLeft}");
            Console.WriteLine($"  QuietZoneRightCoef = {quietZoneRight}");
            Console.WriteLine($"  Saved to: {outputPath}");
        }
        catch (ArgumentException ex)
        {
            // Handles cases where quiet zone values are out of allowed range
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}