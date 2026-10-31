// Title: Generate QR Code and Verify QR Version
// Description: This example generates a QR Code barcode, saves it as a PNG image, and then reads the image back to confirm that the QR version matches the specified version.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition for QR Code symbology. It uses BarcodeGenerator to create a QR Code with a specific QRVersion, saves the image, and then employs BarCodeReader to decode and validate the QR version. This pattern is common for developers needing to ensure compliance with QR specifications, such as version control, error correction levels, and data capacity, across generation and scanning workflows.
// Prompt: Generate QR Code barcode and validate that generated code complies with QR specification version.
// Tags: qr code, barcode generation, barcode recognition, qr version, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation with a specific version and validates the generated code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it, reads it back, and verifies the QR version.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "qr.png");

        // Desired QR version to generate
        QRVersion desiredVersion = QRVersion.Version07;

        // Generate QR Code and save as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set module size (pixel dimension) for the QR Code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Apply the desired QR version
            generator.Parameters.Barcode.QR.Version = desiredVersion;
            // Save the generated barcode image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the saved QR Code and validate its version
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    // Retrieve the detected QR version from the decoded result
                    QRVersion detectedVersion = result.Extended.QR.Version;
                    Console.WriteLine($"Detected QR version: {detectedVersion}");
                    Console.WriteLine($"Expected QR version: {desiredVersion}");
                    Console.WriteLine(detectedVersion == desiredVersion ? "Version matches." : "Version mismatch.");
                }
            }
        }

        // Cleanup temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}