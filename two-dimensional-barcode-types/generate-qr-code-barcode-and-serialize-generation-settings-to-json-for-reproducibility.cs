// Title: Generate QR Code and Export Generation Settings to JSON
// Description: Demonstrates creating a QR Code barcode image and serializing its generation parameters to a JSON file for reproducibility.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with QR symbology, configure properties such as X‑Dimension and error correction level, and persist settings. Developers often need to recreate identical barcodes across environments, so exporting the configuration to JSON is a common practice. The key API classes include BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat.
// Prompt: Generate QR Code barcode and serialize generation settings to JSON for reproducibility.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, encode types, qrcode, settings export

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation and serialization of its settings to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code image, saves it, and writes the generation parameters to a JSON file.
    /// </summary>
    static void Main(string[] args)
    {
        // Define output directory in the temporary folder and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Paths for the generated QR image and the JSON settings file.
        string imagePath = Path.Combine(outputDir, "qr.png");
        string jsonPath = Path.Combine(outputDir, "qr_settings.json");

        // Create a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR Code specific parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Size of a single module.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Error correction level.

            // Save the QR Code image as PNG.
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Capture relevant generation settings for serialization.
            var settings = new
            {
                EncodeType = "QR",
                CodeText = generator.CodeText,
                XDimensionPixels = generator.Parameters.Barcode.XDimension.Pixels,
                QRErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                QRVersion = generator.Parameters.Barcode.QR.Version.ToString()
            };

            // Serialize settings to formatted JSON and write to file.
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
        }

        // Inform the user where the files have been saved.
        Console.WriteLine($"QR code image saved to: {imagePath}");
        Console.WriteLine($"Generation settings saved to: {jsonPath}");
    }
}