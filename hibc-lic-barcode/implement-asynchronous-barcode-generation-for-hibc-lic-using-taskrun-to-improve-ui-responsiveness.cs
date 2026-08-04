// Title: Asynchronous HIBC LIC Barcode Generation Example
// Description: Demonstrates generating a HIBC LIC barcode image asynchronously using Aspose.BarCode and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, EncodeTypes, and related classes to create HIBC symbology barcodes, a common requirement in healthcare and logistics for encoding product information. Developers often need to generate such barcodes on background threads to keep UI responsive.
// Prompt: Implement asynchronous barcode generation for HIBC LIC using Task.Run to improve UI responsiveness.
// Tags: barcode, hibc, lic, asynchronous, task.run, png, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating a HIBC LIC barcode asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode asynchronously and writes the output path.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Generate the HIBC LIC barcode asynchronously and wait for completion.
        string outputPath = await GenerateHibcLicBarcodeAsync();

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }

    // Asynchronously creates a HIBC LIC barcode image and saves it to a PNG file.
    private static Task<string> GenerateHibcLicBarcodeAsync()
    {
        return Task.Run(() =>
        {
            // Prepare the complex codetext for HIBC LIC (secondary data only).
            var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
            {
                // Use HIBC Code128 LIC symbology.
                BarcodeType = EncodeTypes.HIBCCode128LIC,
                // The link character is mandatory; '+' is the default.
                LinkCharacter = '+',
                // Populate secondary data (e.g., lot number).
                Data = new SecondaryAndAdditionalData
                {
                    LotNumber = "LOT123"
                }
            };

            // Define output file path.
            string fileName = "HibcLicBarcode.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            // Generate and save the barcode image.
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Return the full path of the saved image.
            return outputPath;
        });
    }
}