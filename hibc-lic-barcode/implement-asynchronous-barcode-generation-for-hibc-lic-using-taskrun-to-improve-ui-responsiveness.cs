// Title: Asynchronous HIBC LIC barcode generation example
// Description: Demonstrates generating a HIBC QR LIC barcode asynchronously using Task.Run to keep the UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and related classes to create high‑density barcodes for labeling and tracking. Developers often need to generate these barcodes in background threads to avoid blocking UI threads in desktop or web applications.
// Prompt: Implement asynchronous barcode generation for HIBC LIC using Task.Run to improve UI responsiveness.
// Tags: hibc, lic, barcode, async, task.run, png, complexbarcodegenerator, hibclicprimarydatacodetext

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates asynchronous generation of a HIBC QR LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output directory, invokes asynchronous barcode generation, and reports the result.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Determine a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLIC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "HIBCLICPrimary.png");

        try
        {
            // Generate the barcode asynchronously
            await GenerateHIBCLICAsync(outputPath);
            Console.WriteLine($"HIBC LIC barcode generated at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a HIBC QR LIC barcode on a background thread.
    /// </summary>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    /// <returns>A task representing the background generation work.</returns>
    static Task GenerateHIBCLICAsync(string outputPath)
    {
        return Task.Run(() =>
        {
            // Prepare primary data codetext for the HIBC LIC barcode
            HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCQRLIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = "12345",
                    LabelerIdentificationCode = "A999",
                    UnitOfMeasureID = 1
                }
            };

            // Create the generator with the prepared codetext
            using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Set barcode visual parameters (e.g., module size)
                gen.Parameters.Barcode.XDimension.Pixels = 10f;

                // Save the generated barcode as a PNG image
                gen.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }
}