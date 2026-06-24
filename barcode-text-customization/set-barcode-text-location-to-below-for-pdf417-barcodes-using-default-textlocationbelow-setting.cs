using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a PDF417 barcode using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PDF417 barcode with sample text,
    /// configures the human‑readable text location, saves the image, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for PDF417 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Configure the human‑readable text to appear below the barcode (default setting).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Persist the generated barcode as a PNG image file.
            generator.Save("pdf417.png");

            // Inform the user that the barcode has been saved successfully.
            Console.WriteLine("PDF417 barcode saved to pdf417.png");
        }
    }
}