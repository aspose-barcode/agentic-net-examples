using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode suitable for a narrow label using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (Code128) and the text to encode.
        BaseEncodeType symbology = EncodeTypes.Code128;
        string codeText = "123456789012";

        // Create a BarcodeGenerator instance inside a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(symbology, codeText))
        {
            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Enable automatic scaling (interpolation) to adapt the barcode to a narrow label.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image dimensions (in points) appropriate for a narrow label.
            generator.Parameters.ImageWidth.Point = 200f;   // Image width
            generator.Parameters.ImageHeight.Point = 100f;  // Image height

            // Increase the resolution (DPI) for higher print quality (optional).
            generator.Parameters.Resolution = 300f;

            // Specify the font family and size for the human‑readable code text (optional).
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 8f;

            // Define the output file path and save the generated barcode image.
            string outputPath = "narrow_label_barcode.png";
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}