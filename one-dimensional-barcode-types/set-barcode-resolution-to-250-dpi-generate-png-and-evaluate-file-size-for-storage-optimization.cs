using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it, and reports file size.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the barcode image resolution (dots per inch).
            generator.Parameters.Resolution = 250f;

            // Save the generated barcode to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Retrieve the size of the saved file for reporting or optimization purposes.
        long fileSize = new FileInfo(outputPath).Length;

        // Write the output location and file size to the console.
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"File size: {fileSize} bytes (Resolution: 250 DPI)");
    }
}