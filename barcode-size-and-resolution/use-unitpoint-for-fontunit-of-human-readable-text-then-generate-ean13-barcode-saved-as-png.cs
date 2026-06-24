using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an EAN‑13 barcode and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates an EAN‑13 barcode with a specified value and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "ean13.png";

        // Create a barcode generator for the EAN‑13 symbology.
        // The code text includes the checksum digit (12‑digit data + 1 checksum).
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Configure the human‑readable text (the code text displayed below the barcode).
            // Set the font family to Arial and the size to 12 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode image to the specified file in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"EAN13 barcode saved to {outputPath}");
    }
}