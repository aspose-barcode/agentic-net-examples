using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a QR code barcode, saving the image, and persisting the generation settings to JSON.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a QR code, saves it as an image, and writes the generation settings to a JSON file.
        /// </summary>
        static void Main()
        {
            // Define the content to encode and the barcode symbology (QR code).
            string codeText = "https://example.com";
            BaseEncodeType symbology = EncodeTypes.QR;

            // Create a BarcodeGenerator instance with the specified symbology and content.
            using (var generator = new BarcodeGenerator(symbology, codeText))
            {
                // ------------------------------
                // QR‑specific configuration
                // ------------------------------

                // Set the error correction level to high (Level H) for better resilience.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Use automatic encoding mode to let the library choose the optimal mode.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

                // Specify UTF‑8 as the ECI (Extended Channel Interpretation) encoding.
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                // ------------------------------
                // General image settings
                // ------------------------------

                // Set image dimensions (points) – 300 pt width and height.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Define the image resolution (dots per inch).
                generator.Parameters.Resolution = 300f;

                // ------------------------------
                // Save the generated barcode image
                // ------------------------------

                string imagePath = "qr.png";
                generator.Save(imagePath);

                // ------------------------------
                // Capture settings for reproducibility
                // ------------------------------

                var settings = new BarcodeSettingsDto
                {
                    Symbology = "QR",
                    CodeText = codeText,
                    ErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                    EncodeMode = generator.Parameters.Barcode.QR.EncodeMode.ToString(),
                    ECIEncoding = generator.Parameters.Barcode.QR.ECIEncoding.ToString(),
                    ImageWidth = generator.Parameters.ImageWidth.Point,
                    ImageHeight = generator.Parameters.ImageHeight.Point,
                    Resolution = generator.Parameters.Resolution
                };

                // Serialize the settings object to a formatted JSON string.
                string json = JsonSerializer.Serialize(
                    settings,
                    new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to a file.
                string jsonPath = "qr_settings.json";
                File.WriteAllText(jsonPath, json);

                // Output the locations of the generated files.
                Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(imagePath)}");
                Console.WriteLine($"Generation settings saved to: {Path.GetFullPath(jsonPath)}");
            }
        }
    }

    /// <summary>
    /// Data Transfer Object (DTO) used to serialize barcode generation parameters to JSON.
    /// </summary>
    public class BarcodeSettingsDto
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string ErrorLevel { get; set; }
        public string EncodeMode { get; set; }
        public string ECIEncoding { get; set; }
        public float ImageWidth { get; set; }
        public float ImageHeight { get; set; }
        public float Resolution { get; set; }
    }
}