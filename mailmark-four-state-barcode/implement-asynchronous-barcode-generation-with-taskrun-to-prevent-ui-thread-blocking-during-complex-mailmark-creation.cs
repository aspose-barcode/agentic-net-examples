using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode and writes its location to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "mailmark.png";

        // Generate the Mailmark barcode asynchronously.
        await GenerateMailmarkAsync(outputPath);

        // Output the full path of the saved image to the console.
        Console.WriteLine($"Mailmark barcode saved to {Path.GetFullPath(outputPath)}");
    }

    /// <summary>
    /// Generates a Mailmark barcode with predefined data and saves it as a PNG file.
    /// </summary>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    private static async Task GenerateMailmarkAsync(string outputPath)
    {
        // Run the barcode generation on a background thread to avoid blocking the UI thread.
        await Task.Run(() =>
        {
            // Prepare Mailmark codetext with required fields.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state format identifier.
                VersionID = 1,                  // Version of the Mailmark specification.
                Class = "0",                    // Class identifier as a string.
                SupplychainID = 384224,         // Supply chain identifier.
                ItemID = 16563762,              // Item identifier.
                DestinationPostCodePlusDPS = "EF61AH8T " // Destination postcode plus DPS (trailing space required).
            };

            // Create a generator for the complex Mailmark barcode using the prepared codetext.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode as a PNG image to the specified path.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }
}