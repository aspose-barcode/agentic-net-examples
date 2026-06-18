using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode (Mode 2) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode, applies rotation,
    /// padding, and canvas size, then saves the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext (Mode 2) with sample data
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample MaxiCode" }
        };

        // Create the ComplexBarcodeGenerator using the codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Apply a 45-degree rotation to the barcode
            generator.Parameters.RotationAngle = 45f;

            // Apply uniform padding of 30 points on each side
            generator.Parameters.Barcode.Padding.Left.Point   = 30f;
            generator.Parameters.Barcode.Padding.Top.Point    = 30f;
            generator.Parameters.Barcode.Padding.Right.Point  = 30f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 30f;

            // Define the target canvas size (in points)
            generator.Parameters.ImageWidth.Point  = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Ensure the MaxiCode mode is set to Mode2 (optional, default is Mode2)
            generator.Parameters.Barcode.MaxiCode.MaxiCodeMode = MaxiCodeMode.Mode2;

            // Save the generated barcode image to a file
            const string outputPath = "maxicode.png";
            generator.Save(outputPath);
            Console.WriteLine($"MaxiCode barcode saved to {outputPath}");
        }
    }
}