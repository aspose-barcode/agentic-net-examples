using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code containing vCard data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image from vCard data,
    /// saves it to a file, and outputs the file path and Base64 representation.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // vCard data to be encoded in the QR code
        string vCard = "BEGIN:VCARD\r\n" +
                       "VERSION:3.0\r\n" +
                       "N:Doe;John;;;\r\n" +
                       "FN:John Doe\r\n" +
                       "ORG:Example Company\r\n" +
                       "TITLE:Software Engineer\r\n" +
                       "TEL;TYPE=WORK,VOICE:+1-111-555-0100\r\n" +
                       "EMAIL:john.doe@example.com\r\n" +
                       "END:VCARD";

        // Output file name for the generated QR code image
        string outputPath = "vcard_qr.png";

        // Generate QR code with high error correction level
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, vCard))
        {
            // Set error correction level to High (Level H) for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set image resolution to 300 DPI for higher quality output
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and output its Base64 representation
        if (File.Exists(outputPath))
        {
            // Read the generated image file into a byte array
            byte[] imageBytes = File.ReadAllBytes(outputPath);

            // Convert the image bytes to a Base64 string
            string base64 = Convert.ToBase64String(imageBytes);

            // Display the full path of the saved QR code image
            Console.WriteLine("QR code saved to: " + Path.GetFullPath(outputPath));

            // Output the Base64 representation of the image
            Console.WriteLine("Base64 representation:");
            Console.WriteLine(base64);
        }
        else
        {
            // Inform the user if the image file was not created
            Console.WriteLine("Failed to generate the QR code image.");
        }
    }
}