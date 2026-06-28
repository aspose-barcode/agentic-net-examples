using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "barcode_300dpi.png";

        // Determine the directory part of the output path.
        string directory = Path.GetDirectoryName(outputPath);
        // If a directory is specified and it does not exist, create it.
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set the image resolution to 300 DPI for high‑quality output.
            generator.Parameters.Resolution = 300f;

            // Disable auto‑size mode to keep default dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Output the full path of the saved barcode image to the console.
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}