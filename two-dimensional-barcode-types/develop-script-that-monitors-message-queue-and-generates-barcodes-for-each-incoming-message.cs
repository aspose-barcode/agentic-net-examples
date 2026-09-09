// Title: Generate Code128 Barcodes from Queue Messages
// Description: Demonstrates creating Code128 barcodes for a set of messages and saving them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce barcodes. Typical use cases include encoding identifiers, order numbers, or any textual data into machine‑readable images for inventory, shipping, or document processing. Developers often need to configure barcode dimensions, appearance, and output format, which this sample illustrates.
// Prompt: Develop a script that monitors a message queue and generates barcodes for each incoming message.
// Tags: code128, barcode generation, png, aspose.barcode, message queue, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that simulates reading messages from a queue and generates a Code128 barcode for each message using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Iterates over sample messages, creates a barcode for each, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Simulated message queue with sample messages
        string[] messages = { "Hello123", "Order001", "Invoice2023", "SampleData", "TestMsg" };

        // Create a dedicated output folder in the temp directory
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine("Barcodes will be saved to: " + outputDir);

        // Process each message and generate a corresponding barcode image
        for (int i = 0; i < messages.Length; i++)
        {
            string message = messages[i];
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

            try
            {
                // Initialize the barcode generator with Code128 symbology and the message text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, message))
                {
                    // Optional appearance settings
                    generator.Parameters.Barcode.XDimension.Point = 2f;                     // Width of the narrowest bar
                    generator.Parameters.Barcode.BarHeight.Point = 30f;                    // Height of the barcode
                    generator.Parameters.Barcode.FilledBars = false;                       // Use unfilled bars for a lighter look
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false; // Suppress validation exceptions
                    generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below; // Place human‑readable text below the barcode
                    generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f; // Font size for the code text

                    // Save the generated barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode for message '{message}' at {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation
                Console.WriteLine($"Error generating barcode for message '{message}': {ex.Message}");
            }
        }

        // Real implementation note:
        // In production, replace the static 'messages' array with actual queue consumption logic
        // (e.g., Azure Service Bus, RabbitMQ, etc.) and process each incoming message similarly.
    }
}