// Title: Generate QR Code and Validate QR Version
// Description: This example creates a QR Code barcode, saves it as a PNG file, and uses Aspose.BarCode recognition to verify that the generated code matches the specified QR version.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. Key API classes include BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and related parameter objects for configuring QR specifics. Typical use cases involve producing QR codes for marketing or data exchange and programmatically confirming they conform to a required QR specification version. Developers often need to validate generated barcodes against standards before distribution.
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
/// Demonstrates generating a QR Code barcode with a specific QR version,
/// saving it to a temporary PNG file, and validating the version using
/// Aspose.BarCode recognition APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code, saves it, recognizes it,
    /// and checks that the detected QR version matches the desired version.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated PNG image
        string tempPath = Path.Combine(Path.GetTempPath(), "qr_test.png");

        // Specify the QR version we want the generator to use
        QRVersion desiredVersion = QRVersion.Version05;

        // Create a QR Code generator with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR Test"))
        {
            // Set module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Apply the desired QR version and high error correction level
            generator.Parameters.Barcode.QR.Version = desiredVersion;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code to a PNG file (optional step)
            generator.Save(tempPath, BarCodeImageFormat.Png);

            // Generate an in‑memory bitmap for immediate recognition
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a reader that will decode QR codes from the bitmap
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.QR))
                {
                    // Perform the recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no QR code is detected, report and exit
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No QR code detected.");
                        return;
                    }

                    // Iterate through all detected QR codes (normally just one)
                    foreach (BarCodeResult result in results)
                    {
                        // Retrieve the version detected by the recognizer
                        QRVersion detectedVersion = result.Extended.QR.Version;

                        // Output both expected and actual versions
                        Console.WriteLine($"Detected QR Version: {detectedVersion}");
                        Console.WriteLine($"Expected QR Version: {desiredVersion}");

                        // Validate that the detected version matches the desired version
                        if (detectedVersion == desiredVersion)
                        {
                            Console.WriteLine("Version validation succeeded.");
                        }
                        else
                        {
                            Console.WriteLine("Version validation failed.");
                        }
                    }
                }
            }
        }

        // Clean up the temporary PNG file if it still exists
        if (File.Exists(tempPath))
        {
            try
            {
                File.Delete(tempPath);
            }
            catch
            {
                // Suppress any exceptions during cleanup to avoid breaking the flow
            }
        }
    }
}