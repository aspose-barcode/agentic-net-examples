// Title: DataMatrix Barcode Generation with Auto Encoding Mode
// Description: Demonstrates how to generate a DataMatrix barcode using Aspose.BarCode with the encoding mode set to Auto, allowing the engine to select the optimal symbol size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixEncodeMode classes to create barcodes. Developers commonly need to generate DataMatrix codes for inventory, tracking, or labeling, and often require the engine to automatically determine the best symbol size based on the input data.
// Prompt: Set DataMatrix encoding mode to Auto to let the engine choose the optimal symbol size.
// Tags: datamatrix, encoding mode, auto, barcode generation, aspnet, aspnetcore, aspnet5, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with automatic encoding mode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and saves a DataMatrix barcode image.
    /// </summary>
    static void Main()
    {
        // Define the text to be encoded in the barcode.
        const string codeText = "Sample DataMatrix Text";

        // Initialize a DataMatrix barcode generator with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Configure the generator to let the engine choose the optimal symbol size.
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

            // Define the output file path for the generated PNG image.
            const string outputPath = "datamatrix_auto.png";

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
        }
    }
}