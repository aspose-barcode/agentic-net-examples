using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator with the desired symbology (Code128) and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable anti‑aliasing to improve visual quality of the rendered barcode.
            generator.Parameters.UseAntiAlias = true;

            // Set the resolution (dots per inch) for the output image; higher values give sharper results.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image to the specified file in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been successfully saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}