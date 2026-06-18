using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode with specific
    /// parameters and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "datamatrix.png";

        // Initialize a BarcodeGenerator for DataMatrix encoding with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleDataMatrix12345"))
        {
            // Reduce the module (dot) size to increase barcode density.
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Apply bar width reduction to compensate for potential ink spread.
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.1f;

            // Set a high resolution (dots per inch) for better image quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file at the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
    }
}