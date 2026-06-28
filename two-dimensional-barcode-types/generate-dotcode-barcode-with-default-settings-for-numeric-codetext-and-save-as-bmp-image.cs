using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DotCode barcode using Aspose.BarCode and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DotCode barcode with numeric data and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image
        string outputPath = "dotcode.bmp";

        // Initialize the barcode generator for DotCode format with the specified numeric text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Save the generated barcode image to the specified path in BMP format
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved successfully
        Console.WriteLine($"DotCode barcode saved to {outputPath}");
    }
}