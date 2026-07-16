// Title: Generate QR Code and Serialize Settings to JSON
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, customizing its parameters, saving the image, and exporting the generation settings as a JSON string for reproducibility.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and configuration. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and generation parameters (ErrorLevel, ECIEncoding, AutoSizeMode). Typical use cases include generating QR codes for URLs or data payloads and persisting the exact settings to JSON so that the same barcode can be regenerated later, a common requirement for automated testing or documentation.
// Prompt: Generate QR Code barcode and serialize generation settings to JSON for reproducibility.
// Tags: qr code, barcode, generation, json, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as an image,
/// and serializes the generation settings to JSON for later reproducibility.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, configures its parameters,
    /// saves the image, and outputs the settings as formatted JSON.
    /// </summary>
    static void Main()
    {
        // Define the output image file path.
        const string imagePath = "qr.png";

        // Initialize a QR Code generator with the desired text (e.g., a URL).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR‑specific options.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH; // High error correction.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;   // Use UTF‑8 encoding.

            // Set image rendering options.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation; // Use interpolation for scaling.
            generator.Parameters.ImageWidth.Point = 300f;                    // Width in points.
            generator.Parameters.ImageHeight.Point = 300f;                   // Height in points.

            // Optional: adjust the size of individual QR modules.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Hide the human‑readable text beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated QR Code image to the specified path.
            generator.Save(imagePath);

            // Create an anonymous object containing the relevant generation settings.
            var settings = new
            {
                Symbology = "QR",
                CodeText = generator.CodeText,
                ErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                ECIEncoding = generator.Parameters.Barcode.QR.ECIEncoding.ToString(),
                ImageWidth = generator.Parameters.ImageWidth.Point,
                ImageHeight = generator.Parameters.ImageHeight.Point,
                XDimension = generator.Parameters.Barcode.XDimension.Point,
                AutoSizeMode = generator.Parameters.AutoSizeMode.ToString()
            };

            // Serialize the settings object to a formatted JSON string.
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });

            // Output the JSON representation to the console.
            Console.WriteLine("QR Code generation settings (JSON):");
            Console.WriteLine(json);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code image saved to '{Path.GetFullPath(imagePath)}'.");
    }
}