using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode (Mode 2) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "maxicode.png";

        // Create MaxiCode codetext for Mode 2 (postal + data).
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code.
            CountryCode = 56,           // Country code (e.g., 056 for USA).
            ServiceCategory = 999       // Example service category.
        };

        // Create the standard second message (simple text) and assign it to the codetext.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode data"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the MaxiCode barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set custom quiet zone (padding) around the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Optional: set foreground (barcode) and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}