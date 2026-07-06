// Title: Asynchronous HIBC LIC Barcode Generation Example
// Description: Demonstrates generating a HIBC Code 128 LIC barcode with secondary data asynchronously to keep UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and related data classes to create barcodes with additional information. Developers often need to generate such barcodes in background threads to avoid blocking UI threads in desktop or web applications.
// Prompt: Implement asynchronous barcode generation for HIBC LIC using Task.Run to improve UI responsiveness.
// Tags: hibc, lic, barcode, asynchronous, task.run, complexbarcode, generation, png, aspnet, aspnetcore

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates asynchronous generation of a HIBC Code 128 LIC barcode with secondary and additional data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode asynchronously and saves it to the specified path.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can specify output file path.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Determine output file path (use default if not provided)
        string outputPath = args.Length > 0 ? args[0] : "hibc_secondary.png";

        // Ensure the target directory exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Generate the barcode on a background thread
        await GenerateHibcLicBarcodeAsync(outputPath);

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a HIBC LIC barcode with secondary data on a background thread.
    /// </summary>
    /// <param name="outputPath">Full path where the PNG image will be saved.</param>
    /// <returns>A task that completes when the barcode image has been saved.</returns>
    private static Task GenerateHibcLicBarcodeAsync(string outputPath)
    {
        // Offload the CPU‑intensive barcode generation to a thread‑pool thread
        return Task.Run(() =>
        {
            // Prepare secondary‑and‑additional data codetext
            var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCCode128LIC,
                LinkCharacter = '+',
                Data = new SecondaryAndAdditionalData
                {
                    LotNumber = "LOT123",
                    SerialNumber = "SN001",
                    ExpiryDate = DateTime.Today.AddMonths(6),
                    ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                    Quantity = 10,
                    DateOfManufacture = DateTime.Today
                }
            };

            // Create the generator with the prepared codetext
            using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
            {
                // Generate the barcode image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image as PNG to the specified path
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        });
    }
}