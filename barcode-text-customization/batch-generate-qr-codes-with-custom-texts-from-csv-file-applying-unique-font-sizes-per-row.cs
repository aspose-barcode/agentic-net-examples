// Title: Batch QR Code Generation from CSV with Custom Font Sizes
// Description: Demonstrates how to read text and font size values from a CSV file and generate corresponding QR code images using Aspose.BarCode. Each QR code is saved as a PNG with the specified font size applied to the human‑readable text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing batch processing of multiple barcodes. It utilizes the BarcodeGenerator class with EncodeTypes.QR and configures CodeTextParameters for custom font rendering. Developers often need to automate creation of large numbers of barcodes or QR codes from data sources such as CSV, databases, or APIs, customizing appearance and output formats for integration into reports, labels, or web applications.
// Prompt: Batch generate QR codes with custom texts from a CSV file, applying unique font sizes per row.
// Tags: qr code, barcode generation, csv, custom font size, aspose.barcode, png, batch processing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates QR code images in batch by reading text and font size values from a CSV file.
/// Each QR code is saved as a PNG file with a custom font size applied to the code text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary working directory, writes a sample CSV,
    /// processes each line to generate a QR code with the specified font size, and saves the images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory for the demo files
        string workDir = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define paths for the CSV input file and the folder that will hold the generated images
        string csvPath = Path.Combine(workDir, "data.csv");
        string outputDir = Path.Combine(workDir, "output");
        Directory.CreateDirectory(outputDir);

        // Sample CSV content: each line contains the text to encode and the desired font size
        string[] sampleLines = new[]
        {
            "Hello World,12",
            "Aspose.BarCode,14",
            "QR Code Sample,10",
            "CSharp Demo,16",
            "Batch Generation,11"
        };
        File.WriteAllLines(csvPath, sampleLines, Encoding.UTF8);

        // Read all lines from the CSV file using UTF‑8 encoding
        string[] lines = File.ReadAllLines(csvPath, Encoding.UTF8);
        int index = 1;

        // Iterate through each non‑empty line and generate a QR code
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            // Split the line into text and font size components
            string[] parts = line.Split(',');
            if (parts.Length < 2)
                continue; // Skip malformed lines

            string text = parts[0].Trim();

            // Parse the font size; fall back to 12 points if parsing fails
            if (!float.TryParse(parts[1].Trim(), System.Globalization.CultureInfo.InvariantCulture, out float fontSize))
                fontSize = 12f;

            // Build the output file path for the current QR code image
            string outputPath = Path.Combine(outputDir, $"qr_{index}.png");

            // Initialize the barcode generator for QR encoding with the specified text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set the module (pixel) size of the QR code
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Configure the code text to use a manually specified font size
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Save the generated QR code as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR code {index}: \"{text}\" with font size {fontSize} -> {outputPath}");
            index++;
        }

        Console.WriteLine($"All QR codes saved to: {outputDir}");
    }
}