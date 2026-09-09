// Title: Asynchronous Mailmark 4-State Barcode Generation
// Description: Demonstrates generating a Mailmark 4‑State barcode asynchronously to avoid blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.ComplexBarcode classes such as ComplexBarcodeGenerator and MailmarkCodetext to create postal Mailmark barcodes. Developers often need to generate these barcodes in UI applications or services where long‑running image creation must not freeze the interface, making asynchronous patterns like Task.Run essential.
// Prompt: Implement asynchronous barcode generation with Task.Run to prevent UI thread blocking during complex Mailmark creation.
// Tags: mailmark, barcode, async, task.run, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point for generating a Mailmark 4‑State barcode asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that prepares the output directory, triggers asynchronous barcode generation,
    /// and writes the result path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Build a unique temporary folder for the demo output.
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the full file path for the generated PNG image.
        string filePath = Path.Combine(outputFolder, "Mailmark4State.png");

        // Run the barcode generation synchronously from the async method.
        string savedPath = GenerateMailmarkAsync(filePath).GetAwaiter().GetResult();

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Mailmark barcode saved to: {savedPath}");
    }

    /// <summary>
    /// Generates a Mailmark 4‑State barcode and saves it to the specified path.
    /// The heavy work is performed on a background thread via <c>Task.Run</c> to keep the UI responsive.
    /// </summary>
    /// <param name="outputPath">The file system path where the PNG image will be saved.</param>
    /// <returns>A task that resolves to the same <paramref name="outputPath"/> when the operation completes.</returns>
    static async Task<string> GenerateMailmarkAsync(string outputPath)
    {
        return await Task.Run(() =>
        {
            // Create and configure the Mailmark 4‑State codetext.
            MailmarkCodetext mailmarkCode = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Initialize the complex barcode generator with the Mailmark codetext.
            using (var generator = new ComplexBarcodeGenerator(mailmarkCode))
            {
                // Set the X‑dimension (module size) in pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Return the path of the saved image.
            return outputPath;
        });
    }
}