using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main(string[] args)
    {
        // Output file for the generated MaxiCode barcode
        const string outputFile = "maxicode.png";

        // Create a standard MaxiCode codetext (modes 4, 5, 6)
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode"
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Optional: set aspect ratio and encode mode (defaults are acceptable)
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.0f; // Height/Width ratio
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Auto;

            // Log generation parameters
            Console.WriteLine($"MaxiCode Mode: {generator.Parameters.Barcode.MaxiCode.Mode}");
            Console.WriteLine($"Aspect Ratio: {generator.Parameters.Barcode.MaxiCode.AspectRatio}");
            Console.WriteLine($"Encode Mode: {generator.Parameters.Barcode.MaxiCode.EncodeMode}");

            // Save the generated barcode image to a PNG file
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode saved to '{outputFile}'.");
    }
}