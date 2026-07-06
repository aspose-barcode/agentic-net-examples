// Title: Asynchronous Mailmark Barcode Generation Example
// Description: Demonstrates generating Mailmark barcodes asynchronously to avoid UI thread blocking, saving each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator with MailmarkCodetext. It illustrates typical scenarios where developers need to create multiple Mailmark barcodes efficiently, leveraging asynchronous Task.Run to keep UI responsive. Key API classes include ComplexBarcodeGenerator, MailmarkCodetext, and BarCodeImageFormat, commonly used for high‑volume barcode creation in .NET applications.
// Prompt: Implement asynchronous barcode generation with Task.Run to prevent UI thread blocking during complex Mailmark creation.
// Tags: mailmark, barcode, asynchronous, task.run, complexbarcode, generation, png, aspnet, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates asynchronous generation of Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously generates a Mailmark barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="mailmark">The Mailmark codetext containing barcode data.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    /// <returns>A task that resolves to the output path once the image is saved.</returns>
    private static Task<string> GenerateMailmarkAsync(MailmarkCodetext mailmark, string outputPath)
    {
        // Run the generation on a background thread to avoid blocking the UI.
        return Task.Run(() =>
        {
            // Ensure the output directory exists.
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create the complex barcode generator with the Mailmark codetext.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Optional: set barcode colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image as PNG.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Return the path of the generated file.
            return outputPath;
        });
    }

    /// <summary>
    /// Entry point. Prepares sample Mailmark data, generates barcodes asynchronously, and outputs file locations.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Prepare a list of sample Mailmark codetexts.
        var mailmarks = new List<MailmarkCodetext>();

        // Sample 1
        var mailmark1 = new MailmarkCodetext
        {
            Format = 4,                     // Default/unspecified format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };
        mailmarks.Add(mailmark1);

        // Sample 2 (different ItemID)
        var mailmark2 = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563763,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };
        mailmarks.Add(mailmark2);

        // Sample 3 (different ItemID)
        var mailmark3 = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563764,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };
        mailmarks.Add(mailmark3);

        // Create tasks for asynchronous generation.
        var tasks = new List<Task<string>>();
        int index = 1;
        foreach (var mailmark in mailmarks)
        {
            // Build a unique file name for each barcode.
            string fileName = $"mailmark_{index}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            // Queue the generation task.
            tasks.Add(GenerateMailmarkAsync(mailmark, outputPath));
            index++;
        }

        // Await all generation tasks to complete.
        string[] results = await Task.WhenAll(tasks);

        // Output the locations of the generated barcode images.
        foreach (var path in results)
        {
            Console.WriteLine($"Generated Mailmark barcode saved to: {path}");
        }
    }
}