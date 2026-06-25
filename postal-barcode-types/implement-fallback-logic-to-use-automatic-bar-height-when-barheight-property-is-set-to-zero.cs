using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the barcode
        string codeText = "1234567890";

        // Bar height of 0 triggers automatic height fallback
        float barHeight = 0f;

        // Destination file path for the generated barcode image
        string outputPath = "barcode.png";

        // Generate the barcode with the specified parameters
        GenerateBarcode(codeText, barHeight, outputPath);

        // Inform the user where the barcode was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }

    /// <summary>
    /// Generates a barcode image using the specified text, bar height, and output path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="barHeight">Desired bar height in points; if zero or negative, automatic sizing is used.</param>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    static void GenerateBarcode(string codeText, float barHeight, string outputPath)
    {
        // Create a barcode generator for Code128 (any 1D symbology can be used)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            if (barHeight > 0f)
            {
                // Use explicit bar height provided by the caller
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Point = barHeight;
            }
            else
            {
                // Fallback to automatic bar height calculation
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                // Do NOT set BarHeight when using automatic sizing
            }

            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}