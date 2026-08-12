// Title: Generate QR Code barcode and serialize generation settings to JSON
// Description: Demonstrates how to create a QR Code using Aspose.BarCode, save it as a PNG image, and capture the generator settings in a JSON file for reproducibility.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and QR-specific parameters (ErrorLevel, EncodeMode, ECIEncoding). Typical use cases include generating QR codes for URLs or data payloads and persisting configuration for later reuse or testing. Developers often need to serialize settings to ensure consistent barcode output across environments.
// Prompt: Generate QR Code barcode and serialize generation settings to JSON for reproducibility.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, image output

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace AsposeBarcodeQrExample
{
    // DTO for serializing QR generation settings
    public class QrSettings
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float XDimensionPoint { get; set; }
        public string ErrorLevel { get; set; }
        public string EncodeMode { get; set; }
        public string ECIEncoding { get; set; }
    }

    /// <summary>
    /// Demonstrates QR Code generation with Aspose.BarCode and serialization of its settings to JSON.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates a QR Code image, saves it, and writes the generation settings to a JSON file.
        /// </summary>
        static void Main()
        {
            // Define output file paths
            string imagePath = "qr.png";
            string jsonPath = "qr_settings.json";

            // Initialize the QR barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to encode (e.g., a URL)
                generator.CodeText = "https://example.com";

                // Configure QR-specific parameters
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;      // Medium error correction
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;      // Automatic encoding mode
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;     // UTF-8 character set

                // Configure general barcode appearance
                generator.Parameters.Barcode.XDimension.Point = 2f;                 // Module size in points
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black; // Foreground color
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;        // Background color

                // Generate the QR Code image and save it as PNG
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(imagePath, ImageFormat.Png);
                }

                // Capture the current generator settings into a DTO for serialization
                var settings = new QrSettings
                {
                    Symbology = "QR",
                    CodeText = generator.CodeText,
                    XDimensionPoint = generator.Parameters.Barcode.XDimension.Point,
                    ErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                    EncodeMode = generator.Parameters.Barcode.QR.EncodeMode.ToString(),
                    ECIEncoding = generator.Parameters.Barcode.QR.ECIEncoding.ToString()
                };

                // Serialize the settings DTO to a formatted JSON string
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(settings, jsonOptions);
                File.WriteAllText(jsonPath, json);
            }

            // Output the locations of the generated files
            Console.WriteLine($"QR code image saved to: {Path.GetFullPath(imagePath)}");
            Console.WriteLine($"Generation settings saved to: {Path.GetFullPath(jsonPath)}");
        }
    }
}