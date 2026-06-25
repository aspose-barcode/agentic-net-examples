using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a Code16K barcode and saving it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code16K barcode with the maximum allowed length and writes it to a TIFF file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a barcode text consisting of 77 numeric characters,
        // which is the maximum length for Code16K.
        string codeText = new string('1', 77);

        // Define the output file path for the generated TIFF image.
        string outputPath = "code16k.tiff";

        // Initialize the barcode generator with Code16K symbology and the prepared text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set a high resolution (300 DPI) to improve image quality.
            generator.Parameters.Resolution = 300f;

            // Optionally adjust the aspect ratio for Code16K barcodes.
            generator.Parameters.Barcode.Code16K.AspectRatio = 1f;

            // Save the generated barcode as a TIFF image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Code16K barcode saved to: {outputPath}");
    }
}