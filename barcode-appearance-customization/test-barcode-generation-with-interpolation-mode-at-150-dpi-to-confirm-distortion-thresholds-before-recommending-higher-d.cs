// Title: Barcode generation with interpolation mode at different DPI settings
// Description: Demonstrates generating Code128 barcodes at 150 dpi and 300 dpi using Aspose.BarCode with interpolation auto‑size mode to evaluate image distortion.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure AutoSizeMode, resolution, and image dimensions when creating barcodes. It highlights typical use cases such as quality testing, DPI comparison, and visual verification for developers working with barcode image rendering.
// Prompt: Test barcode generation with Interpolation mode at 150 dpi to confirm distortion thresholds before recommending higher DPI.
// Tags: barcode symbology, generation, png, interpolation, dpi, aspose.barcode, autosizemode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates Code128 barcodes at specified DPI values using the Interpolation auto‑size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes at 150 dpi and 300 dpi for visual comparison.
    /// </summary>
    static void Main()
    {
        // Determine the folder where output images will be saved (current directory)
        string outputFolder = Directory.GetCurrentDirectory();

        // Generate a barcode image at 150 dpi
        GenerateBarcode(
            outputFolder,
            "barcode_150dpi.png",
            150f,
            "Test150DPI");

        // Generate a barcode image at 300 dpi for comparison
        GenerateBarcode(
            outputFolder,
            "barcode_300dpi.png",
            300f,
            "Test300DPI");

        // Inform the user that generation is complete and where to find the files
        Console.WriteLine("Barcode generation completed. Check the generated PNG files in:");
        Console.WriteLine(outputFolder);
    }

    /// <summary>
    /// Creates a Code128 barcode image with the specified DPI and saves it to the given folder.
    /// </summary>
    /// <param name="folder">The directory where the image will be saved.</param>
    /// <param name="fileName">The name of the output PNG file.</param>
    /// <param name="dpi">Resolution in dots per inch.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    private static void GenerateBarcode(string folder, string fileName, float dpi, string codeText)
    {
        // Combine folder and file name to get the full path
        string filePath = Path.Combine(folder, fileName);

        // Initialize the barcode generator with Code128 symbology and the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Use Interpolation auto‑size mode to let the generator scale the image
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired resolution (DPI)
            generator.Parameters.Resolution = dpi;

            // Define the target image dimensions in pixels
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Optional: set foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file
            generator.Save(filePath);
        }

        // Output the location of the generated file
        Console.WriteLine($"Generated barcode at {dpi} DPI: {filePath}");
    }
}