// Title: Demonstrate checksum validation for unsupported barcode symbology
// Description: Shows that enabling checksum on a QR code (which lacks checksum support) throws a clear exception.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters and handle validation errors. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common when developers need to generate barcodes and ensure parameter compatibility. Typical use cases include validating configuration before production deployment.
// Prompt: Test that setting IsChecksumEnabled true for a symbology lacking checksum support throws a meaningful exception.
// Tags: barcode symbology, checksum validation, qr code, exception handling, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates that enabling checksum on a QR code (which does not support checksums) results in an exception.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a QR code, then attempts to enable checksum and captures the expected exception.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test output
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string qrPath = Path.Combine(tempDir, "qr.png");

        // Generate a QR code with default settings (no checksum)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(qrPath, BarCodeImageFormat.Png);
            Console.WriteLine($"QR code generated at: {qrPath}");
        }

        // Attempt to enable checksum for QR (unsupported symbology) and capture the validation exception
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            try
            {
                // Enable checksum – this should trigger validation failure for QR codes
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Force barcode generation to invoke validation logic
                generator.GenerateBarCodeImage();

                Console.WriteLine("Unexpected: QR code generated with checksum enabled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expected exception caught:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}