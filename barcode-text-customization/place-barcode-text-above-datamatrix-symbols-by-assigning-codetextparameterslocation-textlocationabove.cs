using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode with human‑readable text placed above the symbol,
    /// saves it to a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for DataMatrix with the sample text "Sample"
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample"))
        {
            // Configure the barcode to display the code text above the symbol
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Define the output file name and path
            string outputPath = "datamatrix.png";

            // Save the generated barcode image to the specified PNG file
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}