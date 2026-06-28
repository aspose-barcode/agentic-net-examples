using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a DotCode barcode using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DotCode barcode and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Define output file name and the data to encode.
        const string outputPath = "dotcode.png";
        const string codeText = "1234567890";

        // Initialize a BarcodeGenerator for DotCode symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Inform the user that error correction level cannot be set for DotCode in this library.
            Console.WriteLine("DotCode error correction level configuration is not supported by Aspose.BarCode.");

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Notify the user that the barcode image has been saved.
        Console.WriteLine($"DotCode barcode saved to {outputPath}");
    }
}