// Title: Checksum Validation for Unsupported Symbology
// Description: Demonstrates that enabling checksum on a barcode symbology that does not support it (QR) throws an exception.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on checksum handling. It shows how to configure barcode parameters using the BarcodeGenerator class, a common task when creating barcodes programmatically. Developers often need to verify that unsupported features, such as checksums for certain symbologies, raise meaningful errors, ensuring robust error handling in automated workflows.
// Prompt: Test that setting IsChecksumEnabled true for a symbology lacking checksum support throws a meaningful exception.
// Tags: barcode, checksum, qr, exception, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that verifies an exception is thrown when attempting to enable a checksum
/// for a barcode symbology (QR) that does not support this feature.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code with checksum enabled to provoke an error.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for the output file
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(tempDir, "qr.png");

        try
        {
            // Initialize the barcode generator for QR code with sample data
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "Test"))
            {
                // QR symbology does not support checksum; enabling it should trigger an exception
                gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Attempt to generate and save the barcode image (expected to fail)
                gen.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine("Barcode generated successfully (unexpected).");
            }
        }
        catch (Exception ex)
        {
            // Expected path: capture and display the meaningful exception message
            Console.WriteLine("Expected exception caught: " + ex.Message);
        }
    }
}