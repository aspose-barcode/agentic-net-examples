// Title: Generate Barcodes from Simulated Message Queue
// Description: Demonstrates iterating over a collection of messages and creating Code128 barcode images saved to a temporary directory.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical use cases include batch barcode creation for order IDs, inventory tags, or any message-driven workflow where each incoming message must be represented as a scannable barcode. Developers often need to automate image generation and storage, which this snippet illustrates.
/// Prompt: Develop a script that monitors a message queue and generates barcodes for each incoming message.
/// Tags: barcode symbology, generation, png, aspose.barcode, code128, message queue

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that simulates reading messages from a queue and generates a barcode image for each message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images for a list of simulated queue messages.
    /// </summary>
    static void Main()
    {
        // Simulate a message queue with a list of order identifiers.
        List<string> simulatedQueue = new List<string>
        {
            "Order-1001",
            "Order-1002",
            "Order-1003"
        };

        // Create a unique temporary folder to store the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each simulated message and generate a corresponding barcode.
        for (int i = 0; i < simulatedQueue.Count; i++)
        {
            string message = simulatedQueue[i];
            string fileName = $"barcode_{i + 1}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator with Code128 symbology and the message text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, message))
            {
                // Configure visual appearance: set module size and colors.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated barcode image.
            Console.WriteLine($"Generated barcode for \"{message}\" at: {filePath}");
        }

        // Indicate that all barcode images have been created.
        Console.WriteLine("All barcodes have been generated.");
    }
}