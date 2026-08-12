// Title: Generate Code128 barcodes from simulated message queue
// Description: Demonstrates creating PNG barcode images for each message using Aspose.BarCode's BarcodeGenerator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes to produce 1D barcodes. Typical use cases include encoding order IDs, invoice numbers, or any textual data into barcodes for labeling, scanning, or tracking. Developers often need to configure barcode dimensions and export formats, which this snippet illustrates.
// Prompt: Develop a script that monitors a message queue and generates barcodes for each incoming message.
// Tags: code128, generation, png, barcodegenerator, encodetypes

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that simulates a message queue and generates a Code128 barcode image for each message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over simulated messages, creates a barcode for each, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Simulated message queue with sample messages
        string[] messages = new[]
        {
            "Order001",
            "Order002",
            "Invoice12345",
            "CustomerABC",
            "ProductXYZ"
        };

        // Create a unique temporary folder for generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // Process each message and generate a corresponding barcode image
        for (int i = 0; i < messages.Length; i++)
        {
            string codeText = messages[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            // Initialize the barcode generator with Code128 symbology and the message text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: configure barcode appearance
                generator.Parameters.Barcode.XDimension.Point = 2f;   // Module (X) size
                generator.Parameters.Barcode.BarHeight.Point = 50f; // Bar height for 1D barcode

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode for \"{codeText}\" -> {filePath}");
        }

        // Note: In a production scenario, replace the simulated array with actual
        // message queue consumption logic (e.g., Azure Service Bus, RabbitMQ, etc.).
        // The core barcode generation code above would remain unchanged.
    }
}