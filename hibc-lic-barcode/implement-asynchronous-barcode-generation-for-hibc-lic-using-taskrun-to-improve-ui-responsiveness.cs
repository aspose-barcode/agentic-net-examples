// Title: Asynchronous HIBC QR LIC Barcode Generation
// Description: Demonstrates generating a HIBC QR LIC barcode asynchronously using Aspose.BarCode to keep UI threads responsive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext for HIBC QR LIC symbology. It illustrates typical tasks such as setting barcode parameters, saving to PNG, and off‑loading work to a background thread with Task.Run—common needs for developers building responsive desktop or web applications that create barcodes on demand.
// Prompt: Implement asynchronous barcode generation for HIBC LIC using Task.Run to improve UI responsiveness.
// Tags: hibc, lic, qr, barcode, generation, async, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Entry point for the asynchronous HIBC QR LIC barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously creates the output directory, generates the barcode, and writes the result path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Determine and create the output folder.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "HIBCLICPrimary.png");

        // Generate the HIBC QR LIC barcode asynchronously.
        await GenerateHIBCLICBarcodeAsync(outputPath);

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated at: {outputPath}");
    }

    /// <summary>
    /// Builds the complex barcode data for a HIBC QR LIC and saves it as a PNG file on a background thread.
    /// </summary>
    /// <param name="outputPath">File system path where the barcode image will be saved.</param>
    /// <returns>A task representing the asynchronous generation operation.</returns>
    static async Task GenerateHIBCLICBarcodeAsync(string outputPath)
    {
        // Prepare the complex code text with required primary data fields.
        var complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Off‑load the barcode generation and saving to a background thread.
        await Task.Run(() =>
        {
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Set the X‑dimension (module width) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 10f;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }
}