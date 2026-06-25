using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a narrow‑column Code128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "narrow_column_barcode.png";

        // Create a BarcodeGenerator instance for Code128 with the sample text "123456".
        // The generator is wrapped in a using statement to ensure proper disposal.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Align the human‑readable text (the encoded value) to the left side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Enable automatic scaling so the barcode fits the specified image dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image width and height (in points) to create a narrow column appearance.
            generator.Parameters.ImageWidth.Point = 150f;   // narrow width
            generator.Parameters.ImageHeight.Point = 50f;   // appropriate height

            // Optionally increase the resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}