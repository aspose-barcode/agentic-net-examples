using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with a code text that exceeds the capacity of a small symbol,
/// triggering an exception that is caught and reported.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode with an intentionally oversized payload to illustrate error handling.
    /// </summary>
    static void Main()
    {
        // Create a code text that is intentionally too long for a small DataMatrix symbol.
        string longCodeText = new string('A', 500);

        // Choose the DataMatrix symbology.
        BaseEncodeType encodeType = EncodeTypes.DataMatrix;

        // Define an output file path in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        try
        {
            // Initialize the barcode generator with the selected type and code text.
            using (var generator = new BarcodeGenerator(encodeType, longCodeText))
            {
                // Force a small DataMatrix version to trigger capacity overflow.
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_10x10;

                // Enable exception throwing for invalid code text (including capacity issues).
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Attempt to save the barcode image.
                generator.Save(outputPath);
                Console.WriteLine($"Barcode successfully saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Handle the exception when the code text exceeds the symbol's capacity.
            Console.WriteLine("Error generating DataMatrix barcode: " + ex.Message);
        }
    }
}