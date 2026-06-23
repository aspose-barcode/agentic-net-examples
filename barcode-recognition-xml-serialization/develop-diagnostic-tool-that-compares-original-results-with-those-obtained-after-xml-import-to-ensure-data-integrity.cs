using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting a <see cref="BarcodeGenerator"/> configuration to XML,
/// importing it back, generating barcode images, and comparing the original and
/// imported results.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Creates a QR barcode, exports its settings to an XML stream,
    /// imports the settings into a new generator, generates images from both,
    /// and outputs a diagnostic comparison of key properties.
    /// </summary>
    static void Main()
    {
        // Initialize the original barcode generator with sample settings.
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Configure QR-specific parameters.
            originalGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            // Set the module (X) dimension in points.
            originalGenerator.Parameters.Barcode.XDimension.Point = 2f;
            // Define image resolution (dpi) and rotation.
            originalGenerator.Parameters.Resolution = 300f;
            originalGenerator.Parameters.RotationAngle = 0f;

            // Generate the original barcode image and store it in a byte array.
            byte[] originalImageBytes;
            using (var originalBitmap = originalGenerator.GenerateBarCodeImage())
            using (var originalMs = new MemoryStream())
            {
                originalBitmap.Save(originalMs, ImageFormat.Png);
                originalImageBytes = originalMs.ToArray();
            }

            // Export the generator's configuration to an in‑memory XML stream.
            using (var xmlMs = new MemoryStream())
            {
                originalGenerator.ExportToXml(xmlMs);
                // Reset stream position for reading.
                xmlMs.Position = 0;

                // Import the settings from XML into a new generator instance.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlMs))
                {
                    // Generate a barcode image from the imported generator.
                    byte[] importedImageBytes;
                    using (var importedBitmap = importedGenerator.GenerateBarCodeImage())
                    using (var importedMs = new MemoryStream())
                    {
                        importedBitmap.Save(importedMs, ImageFormat.Png);
                        importedImageBytes = importedMs.ToArray();
                    }

                    // Compare core properties between original and imported generators.
                    bool codeTextMatch = originalGenerator.CodeText == importedGenerator.CodeText;
                    bool symbologyMatch = originalGenerator.BarcodeType.TypeName == importedGenerator.BarcodeType.TypeName;
                    bool imageMatch = AreByteArraysEqual(originalImageBytes, importedImageBytes);

                    // Output diagnostic results to the console.
                    Console.WriteLine("Diagnostic Comparison Results:");
                    Console.WriteLine($"CodeText match: {codeTextMatch}");
                    Console.WriteLine($"Symbology match: {symbologyMatch}");
                    Console.WriteLine($"Generated image match: {imageMatch}");
                }
            }
        }
    }

    /// <summary>
    /// Compares two byte arrays for equality.
    /// </summary>
    /// <param name="a">First byte array.</param>
    /// <param name="b">Second byte array.</param>
    /// <returns>True if both arrays are non‑null, have the same length, and contain identical bytes; otherwise, false.</returns>
    static bool AreByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }

        return true;
    }
}