using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors and saving it to a uniquely named PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (type) and the text to encode.
        BaseEncodeType symbology = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Build a unique file name using the current timestamp to avoid overwriting existing files.
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
        string fileName = $"barcode_{timestamp}.png";

        // Combine the current directory with the file name to get the full output path.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        // Create a BarcodeGenerator instance with the specified symbology and text.
        using (var generator = new BarcodeGenerator(symbology, codeText))
        {
            // Set the color of the barcode bars (foreground).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color of the image.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the generated barcode image to the previously constructed file path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}