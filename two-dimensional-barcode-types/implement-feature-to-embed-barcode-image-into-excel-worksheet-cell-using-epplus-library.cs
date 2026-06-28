using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode and saving it to a temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Create a BarcodeGenerator for Code128 format with the data "123456".
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved at: {outputPath}");
    }
}