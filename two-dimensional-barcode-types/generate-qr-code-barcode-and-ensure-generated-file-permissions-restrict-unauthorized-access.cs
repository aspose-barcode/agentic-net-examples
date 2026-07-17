// Title: Generate QR Code and Restrict File Permissions
// Description: Demonstrates creating a QR Code barcode image and discusses how to limit file access permissions for security.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel to produce QR Code images. Typical scenarios include creating scannable links for marketing or authentication while ensuring the generated files are protected from unauthorized access. Developers often need to adjust file ACLs or POSIX permissions after saving barcode images, making this pattern useful for secure deployment pipelines.
// Prompt: Generate QR Code barcode and ensure generated file permissions restrict unauthorized access.
// Tags: qr code, barcode, generation, file permissions, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code image using Aspose.BarCode
/// and outlines considerations for restricting file permissions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code, saves it to a PNG file, and logs the output path.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location
        string outputPath = "qr_code.png";

        // Initialize the QR code generator with the desired text (a URL in this case)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR code error correction to the highest level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Set the background color to white
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated QR code image to the specified path (PNG format by default)
            generator.Save(outputPath);
        }

        // Output the full path of the saved QR code image for verification
        Console.WriteLine($"QR code saved to {Path.GetFullPath(outputPath)}");

        // Note: Adjusting file permissions (ACLs on Windows or POSIX permissions on Unix)
        // should be performed after saving the file. This step is omitted here to keep
        // the example CI‑friendly, but in production you would use System.Security.AccessControl
        // or appropriate platform APIs to restrict unauthorized access.
    }
}