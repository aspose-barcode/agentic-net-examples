using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode with sample text and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the DataMatrix barcode.
        string codeText = "Sample DataMatrix";

        // Specify the output file path for the generated barcode image.
        string outputPath = "datamatrix.png";

        // Initialize the barcode generator for DataMatrix with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Configure the generator to automatically select the optimal symbol size.
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
    }
}