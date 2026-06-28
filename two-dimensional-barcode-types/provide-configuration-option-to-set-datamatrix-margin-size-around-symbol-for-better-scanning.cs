using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with custom margin using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode, applies padding, and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        string outputPath = "datamatrix_with_margin.png";

        // Initialize a BarcodeGenerator for DataMatrix with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Hello Aspose"))
        {
            // Optional: set a specific DataMatrix version (square 20x20 modules)
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Configure padding (margin) around the barcode symbol.
            // Values are specified in points; adjust to meet scanning requirements.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;   // left margin
            generator.Parameters.Barcode.Padding.Top.Point = 10f;    // top margin
            generator.Parameters.Barcode.Padding.Right.Point = 10f;  // right margin
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f; // bottom margin

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}