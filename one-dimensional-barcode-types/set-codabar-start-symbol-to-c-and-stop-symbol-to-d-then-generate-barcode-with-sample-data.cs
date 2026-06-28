using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codabar barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Codabar barcode image and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode (without start/stop symbols).
        const string sampleCode = "123456";

        // Initialize the barcode generator with Codabar symbology and the sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, sampleCode))
        {
            // Configure start and stop symbols for Codabar.
            generator.Parameters.Barcode.Codabar.CodabarStartSymbol = CodabarSymbol.C; // Start symbol 'C'
            generator.Parameters.Barcode.Codabar.CodabarStopSymbol = CodabarSymbol.D;  // Stop symbol 'D'

            // Define the output file path.
            const string outputPath = "codabar.png";

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath);

            // Inform the user where the file was saved.
            Console.WriteLine($"Codabar barcode saved to: {outputPath}");
        }
    }
}