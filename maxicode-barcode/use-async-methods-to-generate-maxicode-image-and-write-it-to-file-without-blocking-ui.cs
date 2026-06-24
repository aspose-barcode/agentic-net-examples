using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a MaxiCode barcode (Mode 2) with a standard second message
    /// and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "maxicode.png";

        // Configure the MaxiCode codetext for Mode 2.
        // This includes a 9‑digit postal code, a country code, and a service category.
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Example country code
            ServiceCategory = 999       // Example service category
        };

        // Create the standard second message that accompanies the MaxiCode.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode message"
        };
        // Attach the second message to the codetext.
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate and save the barcode on a background thread to avoid blocking.
        await Task.Run(() =>
        {
            // ComplexBarcodeGenerator is required for MaxiCode generation.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });

        // Output the full path of the saved image for user confirmation.
        Console.WriteLine($"MaxiCode image saved to: {Path.GetFullPath(outputPath)}");
    }
}