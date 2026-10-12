// Title: Checksum Validation for Unsupported Symbology
// Description: Demonstrates that enabling checksum on a barcode symbology that does not support it (QR) throws an exception.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on checksum handling and error validation. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to illustrate typical scenarios where developers need to verify API behavior when configuring unsupported features. Such examples help developers understand exception handling patterns for barcode symbology settings.
// Prompt: Test that setting IsChecksumEnabled true for a symbology lacking checksum support throws a meaningful exception.
// Tags: barcode, symbology, checksum, exception handling, aspose.barcode, generation, qr, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains the entry point for the checksum validation example.
/// </summary>
class Program
{
    /// <summary>
    /// Attempts to enable a checksum on a QR code, which does not support checksums,
    /// and verifies that a meaningful exception is thrown.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files.
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the output PNG file.
        string outputFile = Path.Combine(tempDir, "qr.png");

        // Initialize the barcode generator with QR symbology and sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Test"))
        {
            try
            {
                // Attempt to enable checksum for a symbology that does not support it (QR).
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Save the generated barcode image; this line should not be reached.
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine("No exception was thrown. Checksum was incorrectly accepted.");
            }
            catch (Exception ex)
            {
                // Expected path: capture and display the exception message.
                Console.WriteLine("Expected exception caught:");
                Console.WriteLine(ex.Message);
            }
        }

        // Clean up temporary files and directory.
        try
        {
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}