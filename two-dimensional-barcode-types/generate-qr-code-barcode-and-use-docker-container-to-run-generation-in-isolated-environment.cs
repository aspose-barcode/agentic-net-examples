using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and outputs its Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves it as a PNG file, and prints its Base64 representation.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output QR code image (qr.png) in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Create a BarcodeGenerator for a QR code with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the QR code to use the highest error correction level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the image resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG file at the specified output path.
            generator.Save(outputPath);
        }

        // Check whether the QR code image file was successfully created.
        if (File.Exists(outputPath))
        {
            // Read the image bytes from the file.
            byte[] imageBytes = File.ReadAllBytes(outputPath);

            // Convert the image bytes to a Base64-encoded string.
            string base64 = Convert.ToBase64String(imageBytes);

            // Output the file location and its Base64 representation to the console.
            Console.WriteLine("QR code generated at: " + outputPath);
            Console.WriteLine("Base64 representation:");
            Console.WriteLine(base64);
        }
        else
        {
            // Inform the user that the QR code generation failed.
            Console.WriteLine("Failed to generate QR code.");
        }
    }
}