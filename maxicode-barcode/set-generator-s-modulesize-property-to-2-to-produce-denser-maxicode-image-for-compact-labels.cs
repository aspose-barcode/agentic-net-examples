using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a MaxiCode barcode.
    /// </summary>
    static void Main()
    {
        // The text to encode in the MaxiCode barcode.
        const string codeText = "Sample MaxiCode";

        // Initialize the barcode generator with MaxiCode type and the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the X-dimension (module width) of the barcode to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Define the output file path for the generated barcode image.
            const string outputPath = "maxicode.png";

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        }
    }
}