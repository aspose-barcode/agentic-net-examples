// Title: Asynchronous HIBC LIC Barcode Generation and Decoding
// Description: Demonstrates generating a HIBC QR LIC barcode asynchronously and then decoding it, using Aspose.BarCode's ComplexBarcodeGenerator and BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases how to work with HIBC LIC symbology by creating a primary data codetext, generating a PNG image, and reading the barcode back. Key API classes include ComplexBarcodeGenerator, BarCodeReader, HIBCLICPrimaryDataCodetext, and related helpers. Developers often need such patterns for batch processing, UI‑responsive barcode creation, and validation of HIBC‑compliant labels.
// Prompt: Implement asynchronous barcode generation for HIBC LIC using Task.Run to improve UI responsiveness.
// Tags: barcode, hibc, lic, asynchronous, task.run, generation, decoding, aspose.barcode, png

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides an example of asynchronous generation and decoding of a HIBC QR LIC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Generates a HIBC LIC barcode image asynchronously,
    /// writes the output path to the console, and decodes the image if it exists.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the temporary output file path.
        string outputPath = Path.Combine(Path.GetTempPath(), "HIBCLICPrimary.png");

        // Generate the barcode image asynchronously.
        await GenerateHIBCLICBarcodeAsync(outputPath);
        Console.WriteLine($"Barcode generated at: {outputPath}");

        // If the image was created successfully, decode it asynchronously.
        if (File.Exists(outputPath))
        {
            await DecodeHIBCLICBarcodeAsync(outputPath);
        }
    }

    /// <summary>
    /// Generates a HIBC LIC primary data barcode and saves it as a PNG file.
    /// The operation runs on a background thread via <see cref="Task.Run"/> to keep the UI responsive.
    /// </summary>
    /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
    /// <returns>A task representing the asynchronous generation operation.</returns>
    private static Task GenerateHIBCLICBarcodeAsync(string outputPath)
    {
        return Task.Run(() =>
        {
            // Create primary data codetext for the HIBC LIC barcode.
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

            // Initialize the complex barcode generator with the codetext.
            using (var gen = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Adjust the X‑dimension (module size) for better readability.
                gen.Parameters.Barcode.XDimension.Pixels = 10f;

                // Save the generated barcode as a PNG image.
                gen.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }

    /// <summary>
    /// Decodes a HIBC LIC barcode image and writes the extracted information to the console.
    /// The decoding runs on a background thread via <see cref="Task.Run"/> to avoid blocking the UI.
    /// </summary>
    /// <param name="imagePath">Full file path of the barcode image to decode.</param>
    /// <returns>A task representing the asynchronous decoding operation.</returns>
    private static Task DecodeHIBCLICBarcodeAsync(string imagePath)
    {
        return Task.Run(() =>
        {
            // Initialize the barcode reader for HIBC LIC QR symbology.
            using (var reader = new BarCodeReader(imagePath, DecodeType.HIBCQRLIC))
            {
                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                    // Attempt to parse the complex codetext back into its strongly‑typed representation.
                    var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (complex is HIBCLICPrimaryDataCodetext primary)
                    {
                        Console.WriteLine($"ProductOrCatalogNumber: {primary.Data.ProductOrCatalogNumber}");
                        Console.WriteLine($"LabelerIdentificationCode: {primary.Data.LabelerIdentificationCode}");
                        Console.WriteLine($"UnitOfMeasureID: {primary.Data.UnitOfMeasureID}");
                    }
                }
            }
        });
    }
}