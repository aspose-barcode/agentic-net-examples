using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a MaxiCode Mode 2 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MaxiCode codetext, configures the generator,
    /// and saves the resulting barcode image to a file.
    /// </summary>
    static void Main()
    {
        // Initialize MaxiCode codetext for Mode 2 with required fields.
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit postal code
            CountryCode = 56,           // Numeric country identifier
            ServiceCategory = 999       // Service category value
        };

        // Create the optional second message and assign it to the codetext.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Test message"
        };
        maxiCode.SecondMessage = secondMessage;

        // Use ComplexBarcodeGenerator to render the MaxiCode barcode.
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Set the color of the barcode bars (foreground). No ForeColor property exists.
            generator.Parameters.Barcode.BarColor = Color.Red;

            // Define output file name and format, then save the image.
            string outputPath = "maxicode_mode2.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Output the full path of the saved file for verification.
            Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}