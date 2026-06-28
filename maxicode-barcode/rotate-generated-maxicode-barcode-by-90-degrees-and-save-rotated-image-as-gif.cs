using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, rotating it, and saving as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a MaxiCodeStandardCodetext object with Mode 4 and a sample message.
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode"
        };

        // Initialize the ComplexBarcodeGenerator with the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Configure the rotation angle (root parameter) to rotate the barcode 90 degrees.
            generator.Parameters.RotationAngle = 90f;

            // Define the output file path for the rotated GIF image.
            string outputPath = "maxicode_rotated.gif";

            // Save the generated barcode image as a GIF file.
            generator.Save(outputPath, BarCodeImageFormat.Gif);

            // Inform the user about the saved file location.
            Console.WriteLine($"Rotated MaxiCode saved to: {outputPath}");
        }
    }
}