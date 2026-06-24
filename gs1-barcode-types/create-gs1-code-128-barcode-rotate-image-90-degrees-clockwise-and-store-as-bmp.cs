using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, rotates it, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the barcode text. For GS1 Code 128, AI (Application Identifier) values must be enclosed in parentheses.
        string codeText = "(01)12345678901231";

        // Specify the output file path where the BMP image will be saved.
        string outputPath = "gs1code128.bmp";

        // Initialize the barcode generator with the desired encoding type and text.
        // The 'using' statement ensures the generator is properly disposed after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set the rotation angle to 90 degrees clockwise.
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode image to the specified path in BMP format.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}