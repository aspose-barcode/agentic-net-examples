// Title: Validate DotCode barcode quiet zone
// Description: Demonstrates generating a DotCode barcode and checking that its quiet zone meets the minimum required size for reliable scanning.
// Category-Description: This example is part of the Aspose.BarCode barcode generation and validation collection. It shows how to use BarcodeGenerator, configure XDimension and padding, and verify quiet zone compliance for DotCode symbology. Typical scenarios include ensuring scanner compatibility by meeting quiet zone specifications. Developers often need to adjust module size and padding to satisfy scanner requirements.
// Prompt: Validate that generated DotCode barcode meets minimum quiet zone requirements for scanner compatibility.
// Tags: dotcode, quiet zone, barcode generation, validation, aspose.barcode, png, padding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DotCode barcode, validates its quiet zone against scanner requirements,
/// and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DotCode barcode, checks padding,
    /// and outputs validation results to the console.
    /// </summary>
    static void Main()
    {
        // Sample codetext for DotCode
        const string codeText = "Sample123";

        // Create a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeQuietZone_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "dotcode.png");

        // Generate DotCode barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set a reasonable module size (x-dimension) – 2 points per module
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Specify the number of columns; rows are chosen automatically
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Save the barcode image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Calculate required quiet zone (minimum 10 * x-dimension)
            float requiredQuietZone = 10f * generator.Parameters.Barcode.XDimension.Point;

            // Verify each side's padding meets the required quiet zone
            bool leftOk   = generator.Parameters.Barcode.Padding.Left.Point   >= requiredQuietZone;
            bool rightOk  = generator.Parameters.Barcode.Padding.Right.Point  >= requiredQuietZone;
            bool topOk    = generator.Parameters.Barcode.Padding.Top.Point    >= requiredQuietZone;
            bool bottomOk = generator.Parameters.Barcode.Padding.Bottom.Point >= requiredQuietZone;

            // Output validation details
            Console.WriteLine($"Required quiet zone (points): {requiredQuietZone}");
            Console.WriteLine($"Padding Left:   {generator.Parameters.Barcode.Padding.Left.Point}   {(leftOk   ? "OK" : "FAIL")}");
            Console.WriteLine($"Padding Right:  {generator.Parameters.Barcode.Padding.Right.Point}  {(rightOk  ? "OK" : "FAIL")}");
            Console.WriteLine($"Padding Top:    {generator.Parameters.Barcode.Padding.Top.Point}    {(topOk    ? "OK" : "FAIL")}");
            Console.WriteLine($"Padding Bottom: {generator.Parameters.Barcode.Padding.Bottom.Point} {(bottomOk ? "OK" : "FAIL")}");

            // Summarize overall quiet zone compliance
            if (leftOk && rightOk && topOk && bottomOk)
            {
                Console.WriteLine("Quiet zone requirements are satisfied.");
            }
            else
            {
                Console.WriteLine("Quiet zone requirements are NOT satisfied. Adjust padding as needed.");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not crash the program
        }
    }
}