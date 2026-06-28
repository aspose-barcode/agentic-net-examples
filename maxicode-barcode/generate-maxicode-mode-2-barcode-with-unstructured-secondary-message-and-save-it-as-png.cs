using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a MaxiCode Mode 2 barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MaxiCode Mode 2 codetext,
    /// configures a secondary message, generates the barcode image, and saves it.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "maxicode_mode2.png";

        // Initialize MaxiCode Mode 2 codetext with required fields.
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            // 9‑digit US postal code (example value).
            PostalCode = "524032140",
            // Country code (example value).
            CountryCode = 56,
            // Service category (example value).
            ServiceCategory = 999
        };

        // Create an unstructured (standard) secondary message.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            // Text of the secondary message.
            Message = "Sample secondary message"
        };
        // Assign the secondary message to the MaxiCode codetext.
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Use ComplexBarcodeGenerator to generate the barcode image.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Generate the barcode image in memory.
            complexGenerator.GenerateBarCodeImage();

            // Save the generated image to the specified file path.
            complexGenerator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode Mode 2 barcode saved to: {outputPath}");
    }
}