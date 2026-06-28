using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "1234567890";

        // Select the barcode symbology. Code128 supports any alphanumeric string.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Specify the output file name (PNG format).
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator instance with the chosen symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure the X-dimension (module width) to 0.33 millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}