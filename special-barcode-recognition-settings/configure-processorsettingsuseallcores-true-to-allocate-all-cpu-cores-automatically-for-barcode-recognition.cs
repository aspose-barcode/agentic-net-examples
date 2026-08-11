// Title: Using Aspose.BarCode to generate and recognize a Code128 barcode with multi‑core processing
// Description: Demonstrates creating a Code128 barcode image, saving it, and then recognizing it while configuring the processor to utilize all CPU cores.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes and BarCodeReader with ProcessorSettings to perform high‑performance recognition. Developers often need to generate barcodes for labeling and then read them in batch scenarios, where enabling multi‑core processing improves throughput.
// Prompt: Configure ProcessorSettings.UseAllCores true to allocate all CPU cores automatically for barcode recognition.
// Tags: code128, barcode-generation, barcode-recognition, multithreading, useallcores, aspose-barcodes, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as PNG,
/// configures the recognition processor to use all CPU cores, reads the barcode,
/// and cleans up the temporary image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, multi‑core recognition,
    /// and cleanup operations.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path for the generated barcode image.
        string imagePath = "sample.png";

        // Generate a simple Code128 barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set barcode foreground and background colors (optional).
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image to the specified path.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Enable multi‑core processing for barcode recognition to improve performance.
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Read and display barcode information from the saved image.
        using (var reader = new BarCodeReader(imagePath))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }

        // Delete the temporary image file to clean up resources.
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}