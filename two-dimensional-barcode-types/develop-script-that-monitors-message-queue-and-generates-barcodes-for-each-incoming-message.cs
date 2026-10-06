// Title: Barcode generation from simulated message queue
// Description: Demonstrates generating Code128 barcodes for a series of messages, saving each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes. Typical use cases include batch processing of messages, order IDs, or inventory items where each entry needs a visual barcode representation. Developers often need to automate barcode creation in background services or queue listeners, and this sample shows the core steps for such scenarios.
// Prompt: Develop a script that monitors a message queue and generates barcodes for each incoming message.
// Tags: barcode generation, code128, png, aspose.barcode, batch processing, message queue simulation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that simulates a message queue and generates a barcode image for each message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Simulates receiving messages, generates barcodes, and saves them to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Simulated message queue: a static list of order identifiers.
        string[] messages = new string[]
        {
            "Order001",
            "Order002",
            "Order003",
            "Order004",
            "Order005"
        };

        // Create a unique temporary output directory for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        Console.WriteLine("Generating barcodes in: " + outputFolder);

        // Iterate over each message, generate a barcode, and save it as a PNG file.
        for (int i = 0; i < messages.Length; i++)
        {
            string message = messages[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");
            try
            {
                GenerateBarcode(message, filePath);
                Console.WriteLine($"Generated barcode for \"{message}\" -> {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for \"{message}\": {ex.Message}");
            }
        }

        Console.WriteLine("Processing complete.");
    }

    /// <summary>
    /// Generates a Code128 barcode image for the specified text and saves it to the given path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file system path where the PNG image will be saved.</param>
    static void GenerateBarcode(string codeText, string outputPath)
    {
        // Initialize the barcode generator with Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, ""))
        {
            // Assign the code text using UTF-8 encoding to support a wide range of characters.
            generator.SetCodeText(codeText, Encoding.UTF8);

            // Optional: adjust the module (bar) size for better readability.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}