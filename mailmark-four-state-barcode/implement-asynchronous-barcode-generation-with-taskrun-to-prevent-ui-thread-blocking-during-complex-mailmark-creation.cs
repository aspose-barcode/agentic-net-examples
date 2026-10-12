// Title: Asynchronous Mailmark Barcode Generation Example
// Description: Demonstrates generating a Mailmark 4‑State barcode asynchronously to avoid blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and MailmarkCodetext for creating Mailmark barcodes. Typical use cases include postal services and logistics applications where Mailmark codes are required. Developers often need to run such generation on background threads to keep UI responsive.
// Prompt: Implement asynchronous barcode generation with Task.Run to prevent UI thread blocking during complex Mailmark creation.
// Tags: mailmark, barcode, asynchronous, task.run, complexbarcode, generation, png, aspnet.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation of a Mailmark 4‑State barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Mailmark barcode on a background thread and saves it to the specified path.
    /// </summary>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    /// <returns>The same <paramref name="outputPath"/> after successful generation.</returns>
    static async Task<string> GenerateMailmarkAsync(string outputPath)
    {
        // Run the intensive barcode creation on a thread‑pool thread.
        return await Task.Run(() =>
        {
            // Create Mailmark 4‑State codetext with required parameters.
            var mailmarkCode = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Initialise the complex barcode generator with the codetext.
            using (var generator = new ComplexBarcodeGenerator(mailmarkCode))
            {
                // Set barcode visual properties (pixel size of X‑dimension).
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Return the path for further processing or logging.
            return outputPath;
        });
    }

    /// <summary>
    /// Entry point. Prepares output folder, invokes asynchronous barcode generation, and writes the result path.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder for the demo output.
        string folder = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(folder);

        // Define the full file path for the generated PNG image.
        string filePath = Path.Combine(folder, "Mailmark4State.png");

        // Execute the asynchronous generation synchronously for console app simplicity.
        string resultPath = GenerateMailmarkAsync(filePath).GetAwaiter().GetResult();

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Mailmark barcode generated at: {resultPath}");
    }
}