using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image with custom colors and saving it to a uniquely named file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode, applies custom colors,
    /// saves it to a PNG file with a unique name, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (type) and the text to encode.
        BaseEncodeType symbology = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Create a unique file name using a GUID to avoid overwriting existing files.
        string fileName = $"barcode_{Guid.NewGuid()}.png";
        string outputPath = Path.Combine(Environment.CurrentDirectory, fileName);

        // Initialize the barcode generator with the specified symbology and text.
        using (var generator = new BarcodeGenerator(symbology, codeText))
        {
            // Set the color of the barcode bars (foreground).
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image.
            generator.Parameters.BackColor = Color.Yellow;

            // Save the generated barcode image to the unique file path.
            generator.Save(outputPath);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}