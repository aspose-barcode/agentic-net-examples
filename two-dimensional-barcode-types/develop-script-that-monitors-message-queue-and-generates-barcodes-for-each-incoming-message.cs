using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes for a set of messages.
/// In a real application this would process messages from a queue.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Simulates receiving messages and creates a PNG barcode for each.
    /// </summary>
    static void Main()
    {
        // Simulated incoming messages (replace with queue reading in production).
        string[] incomingMessages = new string[]
        {
            "Order12345",
            "Invoice67890",
            "CustomerABC"
        };

        // Iterate over each message and generate a corresponding barcode image.
        for (int i = 0; i < incomingMessages.Length; i++)
        {
            // Current message to encode.
            string message = incomingMessages[i];

            // Choose Code128 symbology for encoding.
            BaseEncodeType encodeType = EncodeTypes.Code128;

            // Initialize the barcode generator with the selected symbology and message.
            using (var generator = new BarcodeGenerator(encodeType, message))
            {
                // Configure visual appearance of the barcode.
                generator.Parameters.Barcode.BarColor = Color.Black;   // Barcode bars color.
                generator.Parameters.BackColor = Color.White;          // Background color.
                generator.Parameters.Resolution = 300f;                // Image resolution in DPI.

                // Enable checksum for Code128 as required by the standard.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Build the output file name and full path.
                string fileName = $"barcode_{i + 1}.png";
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);

                // Log the successful generation to the console.
                Console.WriteLine($"Generated barcode for message '{message}' -> {outputPath}");
            }
        }

        // Simulated processing complete.
    }
}