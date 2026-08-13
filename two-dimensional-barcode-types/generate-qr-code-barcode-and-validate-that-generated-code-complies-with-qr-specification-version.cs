// Title: Generate QR Code with Specific Version and Validate It
// Description: This example creates a QR Code barcode with a defined QR version and error correction level, saves it as an image, and then reads the image to confirm the generated QR version matches the requested one.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for QR Code symbology. It shows how to configure QR version and error correction using BarcodeGenerator, save the barcode image, and verify properties with BarCodeReader. Ideal for developers needing precise QR specifications in applications such as ticketing, product labeling, or data encoding.
// Prompt: Generate QR Code barcode and validate that generated code complies with QR specification version.
// Tags: qr, barcode, generation, recognition, qrcode, version, error-correction, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR Code with a specific version and error correction level,
/// then validates the generated code using Aspose.BarCode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates, saves, validates, and cleans up a QR Code image.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the QR Code image
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_test.png");

        // Generate QR Code with specific version (Version05) and high error correction level (LevelH)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "Hello World";

            // Set QR version to Version05 (37x37 modules)
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;

            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to the defined path
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // Read the generated QR Code and check its version
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            bool versionMatched = false;

            // Iterate through all detected barcodes (should be only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the detected QR version from the extended result information
                QRVersion detectedVersion = result.Extended.QR.Version;
                Console.WriteLine($"Detected QR Version: {detectedVersion}");

                // Compare detected version with the expected version
                if (detectedVersion == QRVersion.Version05)
                {
                    versionMatched = true;
                }
                else
                {
                    Console.WriteLine($"Version mismatch. Expected: {QRVersion.Version05}, Detected: {detectedVersion}");
                }
            }

            // Output validation result
            if (versionMatched)
            {
                Console.WriteLine("QR code version validation succeeded.");
            }
            else
            {
                Console.WriteLine("QR code version validation failed.");
            }
        }

        // Optional cleanup: delete the temporary QR Code image file
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}