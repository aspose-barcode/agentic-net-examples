using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation of a HIBC LIC barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously generates a HIBC LIC barcode image and saves it to the specified file path.
    /// </summary>
    /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
    /// <returns>The same <paramref name="outputPath"/> if generation succeeds.</returns>
    private static async Task<string> GenerateHibcLicAsync(string outputPath)
    {
        // Prepare secondary data required for the HIBC LIC barcode.
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",                                 // Lot identifier.
            SerialNumber = "SN456",                               // Serial number.
            ExpiryDate = DateTime.Today.AddMonths(6),             // Expiration date (6 months from today).
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,          // Date format for the expiry date.
            Quantity = 10,                                        // Quantity of items.
            DateOfManufacture = DateTime.Today                    // Manufacture date.
        };

        // Create codetext object that combines barcode type, link character, and secondary data.
        var codetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC, // Use Code128 variant of HIBC LIC.
            LinkCharacter = '+',                     // Required link character.
            Data = secondaryData                     // Attach the secondary data defined above.
        };

        // Offload the generation to a background thread to avoid blocking the calling thread.
        return await Task.Run(() =>
        {
            // Ensure the output directory exists before attempting to save the file.
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Generate the barcode using the ComplexBarcodeGenerator and save it to disk.
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                generator.Save(outputPath);
            }

            // Return the path of the generated file.
            return outputPath;
        });
    }

    /// <summary>
    /// Application entry point. Generates a HIBC LIC barcode and writes the result to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can specify the output file path.</param>
    static async Task Main(string[] args)
    {
        // Determine the output file path: use the first argument if provided, otherwise a temporary file.
        string outputFile = args.Length > 0
            ? args[0]
            : Path.Combine(Path.GetTempPath(), "hibc_lic.png");

        try
        {
            // Generate the barcode asynchronously and obtain the resulting file path.
            string resultPath = await GenerateHibcLicAsync(outputFile);
            Console.WriteLine($"HIBC LIC barcode generated successfully: {resultPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}