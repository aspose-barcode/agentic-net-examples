// Title: Generate QR Code with Specific Version and Verify It
// Description: This example creates a QR Code barcode using Aspose.BarCode, saves it as a PNG file, then reads it back to confirm the QR version matches the requested specification.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition for QR Code symbology. It showcases the use of BarcodeGenerator to configure QR version and X‑dimension, and BarCodeReader to decode and inspect QR metadata. Ideal for developers needing to produce QR codes with precise version control and validate them programmatically.
// Prompt: Generate QR Code barcode and validate that generated code complies with QR specification version.
// Tags: qr code, barcode generation, barcode recognition, qrcode, version validation, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code with a specific version and verifying it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it, reads it back, and validates the QR version.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated QR Code image.
        string tempPath = Path.Combine(Path.GetTempPath(), "qr_test.png");

        // ------------------------------------------------------------
        // Generate QR Code with a specific version (Version05)
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code version to Version05.
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;

            // Optional: define the size of a single QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated QR Code as a PNG image to the temporary path.
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the generated QR Code reports the expected version
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.QR))
        {
            bool versionMatched = false;

            // Iterate through all detected barcodes (should be only one QR Code).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the detected QR version from the extended result data.
                QRVersion detectedVersion = result.Extended.QR.Version;
                Console.WriteLine($"Detected QR version: {detectedVersion}");

                // Compare the detected version with the expected version.
                if (detectedVersion == QRVersion.Version05)
                {
                    Console.WriteLine("Version matches the expected QRVersion.Version05.");
                    versionMatched = true;
                }
                else
                {
                    Console.WriteLine("Version does NOT match the expected QRVersion.Version05.");
                }
            }

            // If no matching version was found, inform the user.
            if (!versionMatched)
            {
                Console.WriteLine("No QR code detected or version mismatch.");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary file
        // ------------------------------------------------------------
        if (File.Exists(tempPath))
        {
            try
            {
                File.Delete(tempPath);
            }
            catch
            {
                // Ignore any cleanup errors to avoid interrupting the flow.
            }
        }
    }
}