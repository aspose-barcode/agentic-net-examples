// Title: Read QR Code version and error correction level from detected QR barcodes
// Description: Demonstrates generating a QR barcode with a specific version and error correction level, then reading those properties back from the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to set QR version and error correction level, and BarCodeReader with DecodeType.QR to extract extended QR information such as version and error correction level. Developers working with QR code customization and validation often need to read these parameters to ensure compliance with specifications.
// Prompt: Read QR Code version and error correction level from each detected QR barcode.
// Tags: qr code, version, error correction level, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with a specific version and error correction level,
/// then reading those properties from the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, reads its version and error correction level, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated QR image
        string qrImagePath = Path.Combine(tempFolder, "sample_qr.png");

        // Generate a QR barcode with a specific version (5) and error correction level (H)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Set QR version and error correction level via generator parameters
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode as a PNG image
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the QR image was successfully created before attempting to read it
        if (!File.Exists(qrImagePath))
        {
            Console.WriteLine("Failed to create QR image.");
            return;
        }

        // Read the QR barcode from the image and output its version and error correction level
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Ensure the detected barcode is a QR code before accessing QR-specific properties
                if (result.CodeType == DecodeType.QR)
                {
                    // Retrieve extended QR information (version and error correction level)
                    var qrExt = result.Extended.QR;

                    // Output the detected QR code details
                    Console.WriteLine($"Detected QR Code:");
                    Console.WriteLine($"  Version: {qrExt.Version}");
                    Console.WriteLine($"  Error Correction Level: {qrExt.ErrorLevel}");
                }
                else
                {
                    Console.WriteLine("Detected barcode is not a QR code.");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(qrImagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually clean temp files.
        }
    }
}