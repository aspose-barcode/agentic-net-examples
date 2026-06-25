using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates dynamic sizing of a Code128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image with dimensions
    /// that adapt to the length of the supplied code text.
    /// </summary>
    static void Main()
    {
        // Sample code text; change length to see automatic size adjustment.
        string codeText = "DynamicSizeDemo-1234567890";

        // Base dimensions (in points). Adjust width based on code text length.
        float baseWidth = 150f;
        float widthPerChar = 8f; // additional width per character
        float targetWidth = baseWidth + (codeText.Length * widthPerChar);
        float targetHeight = 100f; // fixed height

        // Create a barcode generator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable automatic resizing based on target dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image size (units are points).
            generator.Parameters.ImageWidth.Point = targetWidth;
            generator.Parameters.ImageHeight.Point = targetHeight;

            // Optional: set barcode and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image to a file.
            string outputPath = "dynamic_barcode.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved and its dimensions.
            Console.WriteLine($"Barcode saved to {outputPath} (Width: {targetWidth}pt, Height: {targetHeight}pt)");
        }
    }
}