using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode (Mode 2) with a standard second message
    /// and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Create a MaxiCode codetext object for Mode 2 with required fields
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9‑digit US postal code
            CountryCode = 56,         // Example country code
            ServiceCategory = 999     // Example service category
        };

        // Create the standard second message to be embedded in the barcode
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode message"
        };

        // Assign the second message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Initialize the ComplexBarcodeGenerator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Configure image resolution (dots per inch) for higher print quality
            generator.Parameters.Resolution = 300f;

            // Define output file path and save the barcode as a PNG image
            string outputPath = "maxicode.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        }
    }
}