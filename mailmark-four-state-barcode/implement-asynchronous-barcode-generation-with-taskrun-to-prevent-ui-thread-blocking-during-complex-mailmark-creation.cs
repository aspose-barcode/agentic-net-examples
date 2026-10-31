// Title: Asynchronous Mailmark Barcode Generation Example
// Description: Demonstrates generating a Mailmark barcode asynchronously using Task.Run to avoid blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and MailmarkCodetext classes. It illustrates typical scenarios where developers need to create high‑capacity Mailmark barcodes without freezing the UI, such as in desktop or web applications that require responsive performance.
// Prompt: Implement asynchronous barcode generation with Task.Run to prevent UI thread blocking during complex Mailmark creation.
// Tags: mailmark, barcode, asynchronous, task.run, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Mailmark barcode on a background thread and saves it to the specified path.
    /// </summary>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    /// <returns>The same <paramref name="outputPath"/> after the file has been written.</returns>
    static async Task<string> GenerateMailmarkAsync(string outputPath)
    {
        // Run the intensive barcode creation on a thread‑pool thread to keep the UI responsive.
        return await Task.Run(() =>
        {
            // Configure Mailmark specific data.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Use ComplexBarcodeGenerator to create the barcode image.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module size) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Return the path so the caller can confirm the location.
            return outputPath;
        });
    }

    /// <summary>
    /// Entry point that creates a temporary folder, generates the barcode asynchronously, and writes the output path to the console.
    /// </summary>
    static async Task Main()
    {
        // Prepare a temporary directory for the output image.
        string folder = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(folder);

        // Define the full file path for the generated PNG.
        string filePath = Path.Combine(folder, "Mailmark4State.png");

        // Generate the barcode without blocking the main thread.
        string savedPath = await GenerateMailmarkAsync(filePath);

        // Inform the user where the file was saved.
        Console.WriteLine($"Mailmark barcode saved to: {savedPath}");
    }
}