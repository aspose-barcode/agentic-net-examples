using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of MaxiCode barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Executes two examples: one with valid ASCII data and one with Unicode data that triggers an exception.
    /// </summary>
    static void Main()
    {
        // Example 1: valid ASCII data in Binary mode
        GenerateMaxiCode("Hello, World!", "maxicode_binary_ascii.png");

        // Example 2: Unicode data in Binary mode – should raise an exception
        GenerateMaxiCode("犬", "maxicode_binary_unicode.png");
    }

    /// <summary>
    /// Generates a MaxiCode barcode with the specified data and saves it to the given file path.
    /// </summary>
    /// <param name="data">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateMaxiCode(string data, string outputPath)
    {
        // Create a barcode generator configured for MaxiCode
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            // Assign the data to be encoded
            generator.CodeText = data;

            // Set encoding mode to Binary (required for this example)
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.Binary;

            try
            {
                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
            catch (Exception ex)
            {
                // Output error details (e.g., Unicode characters not allowed in Binary mode)
                Console.WriteLine($"Failed to generate barcode for data \"{data}\": {ex.Message}");
            }
        }
    }
}