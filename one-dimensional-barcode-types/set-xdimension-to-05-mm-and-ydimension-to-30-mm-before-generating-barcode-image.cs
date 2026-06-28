using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the smallest bar width (X dimension) to 0.5 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Configure the image height (Y dimension) to 30 millimeters
            generator.Parameters.ImageHeight.Millimeters = 30f;

            // Define the output file path for the generated barcode image
            string outputPath = "barcode.png";

            // Save the barcode image to the specified file
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}