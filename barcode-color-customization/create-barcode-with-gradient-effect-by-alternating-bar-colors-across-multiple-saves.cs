using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code128 barcodes with different bar colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcode images, each with a distinct bar color.
    /// </summary>
    static void Main()
    {
        // Sample barcode data to encode.
        const string codeText = "1234567890";

        // Define an array of colors to apply to the barcode bars.
        Color[] colors = new Color[]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Orange,
            Color.Purple
        };

        // Iterate over each color, generate a barcode, and save it to a PNG file.
        for (int i = 0; i < colors.Length; i++)
        {
            // Construct a unique file name for the current barcode image.
            string fileName = $"barcode_{i + 1}.png";

            // Create a BarcodeGenerator for Code128 with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the current color to the barcode bars.
                generator.Parameters.Barcode.BarColor = colors[i];

                // Optionally set a modest image size (width and height in points).
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode image as a PNG file.
                generator.Save(fileName, BarCodeImageFormat.Png);

                // Output a confirmation message to the console.
                Console.WriteLine($"Saved {fileName} with color {colors[i].Name}");
            }
        }

        // Indicate that all barcode images have been generated.
        Console.WriteLine("All barcodes generated.");
    }
}