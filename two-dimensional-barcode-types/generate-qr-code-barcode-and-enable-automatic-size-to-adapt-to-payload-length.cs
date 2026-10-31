// Title: Generate QR Code with automatic size adaptation
// Description: Demonstrates creating QR Code barcodes where the image size automatically adjusts to the length of the payload.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.QR. It illustrates typical scenarios such as encoding short and long text strings into QR codes without manually setting dimensions, a common requirement for developers needing dynamic barcode sizing for varied data lengths.
// Prompt: Generate QR Code barcode and enable automatic size to adapt to payload length.
// Tags: qr code, barcode generation, automatic sizing, aspnet, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating QR Code barcodes with automatic size adaptation based on payload length.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates multiple QR Code images from predefined payloads and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define a set of payload strings with varying lengths.
        string[] payloads = new string[]
        {
            "A",
            "Hello",
            "Aspose.BarCode QR Code",
            "This is a longer text to demonstrate automatic size adaptation based on payload length.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
        };

        // Iterate over each payload, generate a QR code, and save it as a PNG file.
        for (int i = 0; i < payloads.Length; i++)
        {
            string text = payloads[i];
            string filePath = Path.Combine(outputDir, $"QRCode_{i + 1}.png");

            // Create a BarcodeGenerator for QR encoding; size adapts automatically.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // No explicit size settings; default auto-sizing adapts to the payload.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the generation result to the console.
            Console.WriteLine($"Generated QR code {i + 1}: \"{text}\" -> {filePath}");
        }
    }
}