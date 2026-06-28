using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, configures its appearance, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode.
            generator.CodeText = "123ABC";

            // Configure the human‑readable text (the text displayed below the barcode):
            // - Font family: Verdana
            // - Font size: 14 points
            // - Font style: Bold
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Bold;

            // Define the output file path for the generated PNG image.
            string outputPath = "barcode.png";

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        } // The using block disposes the generator and releases resources.
    }
}