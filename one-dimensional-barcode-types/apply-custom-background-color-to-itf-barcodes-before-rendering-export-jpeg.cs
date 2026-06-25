using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an ITF-14 barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates an ITF-14 barcode with custom visual settings.
    /// </summary>
    static void Main()
    {
        // Define the barcode data: 14 numeric characters required for ITF-14.
        const string codeText = "12345678901231";

        // Initialize the barcode generator with ITF-14 symbology and the provided data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Set a light yellow background color for the image.
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 224); // Light yellow

            // Configure visual appearance of the barcode bars.
            generator.Parameters.Barcode.BarColor = Color.Black;          // Color of the bars
            generator.Parameters.Barcode.BarHeight.Point = 40f;          // Height of the bars in points

            // Define the output file path and save the barcode as a JPEG image.
            const string outputPath = "itf_barcode.jpg";
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}