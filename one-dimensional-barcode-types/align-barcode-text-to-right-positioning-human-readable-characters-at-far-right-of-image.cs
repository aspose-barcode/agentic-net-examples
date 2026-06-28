using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with right‑aligned human‑readable text using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image and saves it to the specified file path.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "barcode_right_aligned.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the sample data "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum calculation for Code128 (required for valid encoding).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set the alignment of the human‑readable text to the right side of the image.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // (Optional) Keep the text below the barcode – this is the default location.
            // generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode image to the previously defined output path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}