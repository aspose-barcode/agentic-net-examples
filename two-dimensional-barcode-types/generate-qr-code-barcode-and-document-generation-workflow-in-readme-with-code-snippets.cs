using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // QR code content
        string qrContent = "https://example.com";

        // Generate QR code image
        string qrImagePath = Path.Combine(outputDir, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Set QR error correction level to High
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Use automatic encoding mode
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Set image size (300 points = 4.17 inches)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Set resolution to 300 DPI
            generator.Parameters.Resolution = 300;

            // Set colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save QR code image
            generator.Save(qrImagePath);
        }

        // Create a simple README file describing the workflow
        string readmePath = Path.Combine(outputDir, "README.txt");
        string readmeContent = @"QR Code Generation Workflow
--------------------------------
1. Define the content to encode (e.g., a URL).
2. Create a BarcodeGenerator instance with EncodeTypes.QR.
3. Configure QR parameters:
   - Error correction level (LevelH for high reliability).
   - Encoding mode (Auto).
   - Image dimensions and resolution.
   - Bar and background colors.
4. Save the generated image to a file (qr.png).
5. Use the generated QR code image as needed (e.g., embed in documents).";

        File.WriteAllText(readmePath, readmeContent);
    }
}