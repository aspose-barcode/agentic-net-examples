using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample codetext for MaxiCode. Mode4 (standard) accepts any text.
        const string codeText = "Sample MaxiCode";

        // Create the barcode generator for MaxiCode with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure the aspect ratio (height/width) of the generated MaxiCode.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.5f;

            // Define the output file path for the PNG image.
            const string outputPath = "maxicode.png";

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        }
    }
}