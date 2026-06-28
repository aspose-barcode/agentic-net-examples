using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codabar barcode and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Codabar barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "codabar.png";

        // Initialize a BarcodeGenerator for the Codabar symbology within a using block
        // to ensure proper disposal of unmanaged resources.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Assign the text to be encoded in the barcode.
            generator.CodeText = "A123456B";

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}