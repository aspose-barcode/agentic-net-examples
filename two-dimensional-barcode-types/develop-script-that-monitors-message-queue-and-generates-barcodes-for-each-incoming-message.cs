// Title: Generate Barcodes from Queue Messages
// Description: Demonstrates how to read messages from a simulated queue and create Code128 barcode images for each entry.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image parameter customization. Developers often need to automate barcode creation from data streams such as message queues, databases, or files, and this snippet shows the typical workflow for generating PNG images programmatically.
// Prompt: Develop a script that monitors a message queue and generates barcodes for each incoming message.
// Tags: barcode symbology, generation, png, code128, aspose.barcode, message queue

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes from a list of messages, simulating a message queue.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Iterates over sample messages, creates barcode images, and saves them to disk.
    /// </summary>
    static void Main()
    {
        // Simulated message queue containing sample identifiers.
        List<string> messages = new List<string>
        {
            "Order001",
            "Invoice2023",
            "CustomerABC",
            "ProductXYZ",
            "Shipment123"
        };

        // Determine the output directory for barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Process each message and generate a corresponding barcode image.
        for (int i = 0; i < messages.Count; i++)
        {
            string codeText = messages[i];
            string fileName = $"barcode_{i + 1}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Initialize a barcode generator for Code128 symbology with the current message text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Customize barcode appearance (colors and size).
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;

                // Save the generated barcode as a PNG file.
                generator.Save(filePath);
            }

            // Log the successful generation of the barcode.
            Console.WriteLine($"Generated barcode for \"{codeText}\" at: {filePath}");
        }

        // Indicate that all barcode images have been created.
        Console.WriteLine("All barcodes have been generated.");
    }
}