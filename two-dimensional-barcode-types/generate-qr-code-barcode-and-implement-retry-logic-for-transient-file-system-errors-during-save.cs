// Title: Generate QR Code with retry logic for file save
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it to a PNG file with retry handling for transient I/O errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR Code parameters (error correction level, module size) and persist the image using BarcodeGenerator. Typical use cases include generating QR codes for URLs, contact info, or product data, where developers need robust file system operations with retry logic for temporary failures. Key API classes: BarcodeGenerator, EncodeTypes, QRErrorLevel, BarCodeImageFormat.
// Prompt: Generate QR Code barcode and implement retry logic for transient file system errors during save.
// Tags: qr code, barcode generation, retry logic, io exception handling, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation and resilient file saving with retry logic.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code and attempts to save it with retry handling for I/O errors.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated QR code image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full file path for the PNG image
        string filePath = Path.Combine(outputDir, "qr.png");
        const int maxAttempts = 3; // Maximum number of save attempts

        // Initialize the barcode generator for QR code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose!"))
        {
            // Set QR code error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            // Define the size of each QR module in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Attempt to save the image, retrying on transient file system errors
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    // Save the generated QR code as a PNG file
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"QR code saved successfully to: {filePath}");
                    break; // Exit loop on successful save
                }
                catch (IOException ex)
                {
                    // Handle I/O errors such as file being locked or disk issues
                    Console.WriteLine($"IO attempt {attempt} failed: {ex.Message}");
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine("All attempts failed. Exiting.");
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Handle permission-related errors
                    Console.WriteLine($"Access attempt {attempt} failed: {ex.Message}");
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine("All attempts failed. Exiting.");
                    }
                }
            }
        }
    }
}