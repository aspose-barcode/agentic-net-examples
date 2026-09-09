// Title: Generate QR Code and Log Exceptions
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as PNG, and logging success or errors for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with QR symbology, configure parameters, handle encoding exceptions, and write audit logs. Developers commonly need to generate barcodes programmatically, customize dimensions, and maintain an audit trail of generation outcomes.
// Prompt: Generate QR Code barcode and catch and log encoding exceptions for audit trail.
// Tags: qr code, barcode generation, exception handling, audit logging, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and logs outcomes for audit purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, saves it as PNG, and records success or failure in a log file.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Paths for the generated image and the audit log file
        string imagePath = Path.Combine(outputDir, "qr.png");
        string logPath = Path.Combine(outputDir, "audit.log");

        // Text to encode in the QR Code
        string codeText = "Hello, World!";

        try
        {
            // Initialize the barcode generator for QR symbology with the specified text
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Configure generator to throw an exception if the code text is invalid
                gen.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Set the module size (X dimension) in pixels
                gen.Parameters.Barcode.XDimension.Pixels = 8f;

                // Save the generated QR Code as a PNG image
                gen.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR Code saved to {imagePath}");

                // Log successful generation with a UTC timestamp
                File.AppendAllText(logPath, $"[{DateTime.UtcNow}] QR Code generated successfully.{Environment.NewLine}");
            }
        }
        catch (Exception ex)
        {
            // Build an error message with a UTC timestamp
            string message = $"[{DateTime.UtcNow}] Exception during QR Code generation: {ex.Message}{Environment.NewLine}";
            Console.WriteLine(message);

            // Append the error details to the audit log
            File.AppendAllText(logPath, message);
        }
    }
}