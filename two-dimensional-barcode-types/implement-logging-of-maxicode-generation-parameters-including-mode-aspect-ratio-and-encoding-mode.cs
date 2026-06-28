using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Sample codetext for a standard MaxiCode (mode 4)
        const string codeText = "Test message";

        // Create the barcode generator for MaxiCode with the specified codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure MaxiCode specific parameters
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;               // Set the MaxiCode mode to Mode4
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.0f;                     // Set aspect ratio (height/width) to 1.0
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Auto; // Use automatic encoding mode

            // Log the configured parameters to the console for verification
            Console.WriteLine("MaxiCode Generation Parameters:");
            Console.WriteLine($"  Mode          : {generator.Parameters.Barcode.MaxiCode.Mode}");
            Console.WriteLine($"  Aspect Ratio : {generator.Parameters.Barcode.MaxiCode.AspectRatio}");
            Console.WriteLine($"  Encode Mode  : {generator.Parameters.Barcode.MaxiCode.EncodeMode}");

            // Save the generated barcode image to a file
            const string outputPath = "maxicode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}