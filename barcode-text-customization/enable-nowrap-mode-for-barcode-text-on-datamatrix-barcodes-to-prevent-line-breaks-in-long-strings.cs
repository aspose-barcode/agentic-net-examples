using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with NoWrap enabled to prevent line breaks in the human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode from a long text string, disables wrapping, sets resolution, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a long text string that would normally wrap in the human‑readable portion of the barcode.
        string longText = "ThisIsAVeryLongDataMatrixCodeTextThatShouldNotWrapEvenIfItExceedsTypicalLengths";

        // Initialize a BarcodeGenerator for DataMatrix using the long text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Disable wrapping of the human‑readable text to keep it on a single line.
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;

            // Optionally set the image resolution (dots per inch) for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Define the output file path for the generated barcode image.
            string outputPath = "datamatrix_nowrap.png";

            // Save the barcode image to the specified file.
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
        }
    }
}