using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code39 barcode with custom appearance and saving it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, customizes its appearance, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output SVG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_barcode.svg");

        // Ensure the directory for the output file exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a barcode generator for Code39FullASCII with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CUSTOM123"))
        {
            // ----- Human‑readable text customization -----
            // Set the font family and size for the code text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Set the color, alignment, and location of the code text.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Blue;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // ----- Visual appearance of the barcode -----
            // Set the color of the bars and the background.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Disable automatic sizing and set a fixed resolution.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Resolution = 300f;

            // ----- Save the barcode as an SVG file -----
            // Wrap the save operation in a try‑catch block to handle potential errors (required for evaluation mode).
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save SVG: {ex.Message}");
            }
        }
    }
}