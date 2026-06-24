using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with a caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode, adds a caption below it,
    /// and saves the result to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "datamatrix.png";

        // Initialize a BarcodeGenerator for DataMatrix encoding with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleCode"))
        {
            // Make the caption visible below the barcode.
            generator.Parameters.CaptionBelow.Visible = true;

            // Set the caption text that will appear beneath the barcode.
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";

            // Apply a bottom padding of 8 points to the caption for visual spacing.
            generator.Parameters.CaptionBelow.Padding.Bottom.Point = 8f;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}