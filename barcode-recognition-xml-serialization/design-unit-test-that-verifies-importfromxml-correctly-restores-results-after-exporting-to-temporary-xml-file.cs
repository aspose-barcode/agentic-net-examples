// Title: Verify ImportFromXml restores barcode generator settings
// Description: Demonstrates exporting barcode generator settings to XML, importing them back, and confirming that the barcode can be read correctly.
// Category-Description: This example belongs to the Aspose.BarCode settings management category, illustrating how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml. It shows typical use cases such as persisting barcode configurations, sharing them across applications, and validating that imported settings produce the expected barcode output. Developers working with barcode generation and recognition often need to serialize settings for reuse or deployment, and this snippet provides a clear reference.
// Prompt: Design a unit test that verifies ImportFromXml correctly restores results after exporting to a temporary XML file.
// Tags: barcode symbology, export, import, xml, unit-test, settings, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that exports barcode generator settings to XML,
/// imports them back, and validates that the restored settings generate
/// a readable barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export, import, and validation steps.
    /// </summary>
    static void Main()
    {
        // Prepare temporary file paths for the XML settings and barcode image.
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode_settings.xml");
        string imgPath = Path.Combine(Path.GetTempPath(), "barcode_image.png");

        // Create the original barcode generator with QR symbology and sample text.
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Set a custom XDimension to verify that the value is restored after import.
            originalGenerator.Parameters.Barcode.XDimension.Point = 2f;

            // Export the generator's configuration to an XML file.
            bool exportSuccess = originalGenerator.ExportToXml(xmlPath);
            if (!exportSuccess)
            {
                Console.WriteLine("FAILED: ExportToXml returned false.");
                return;
            }

            // Save a barcode image (used later for recognition).
            originalGenerator.Save(imgPath);
        }

        // Import the generator settings from the previously saved XML file.
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("FAILED: ImportFromXml returned null.");
            return;
        }

        // Generate a barcode image from the imported settings into a memory stream.
        using (var imageStream = new MemoryStream())
        {
            importedGenerator.Save(imageStream, BarCodeImageFormat.Png);
            imageStream.Position = 0; // Reset stream position for reading.

            // Initialize a barcode reader to verify the generated image.
            using (var reader = new BarCodeReader())
            {
                // Provide the image stream to the reader.
                reader.SetBarCodeImage(imageStream);
                // Configure the reader to attempt decoding all supported types.
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                // Perform barcode recognition.
                var results = reader.ReadBarCodes();

                // Validate that a barcode was detected and that settings match expectations.
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                }
                else
                {
                    var result = results[0];
                    bool codeTextMatch = string.Equals(result.CodeText, "Test123", StringComparison.Ordinal);
                    bool xDimensionMatch = Math.Abs(importedGenerator.Parameters.Barcode.XDimension.Point - 2f) < 0.001f;

                    if (codeTextMatch && xDimensionMatch)
                    {
                        Console.WriteLine("PASSED: ImportFromXml restored settings correctly.");
                    }
                    else
                    {
                        Console.WriteLine("FAILED: Restored settings do not match original.");
                        Console.WriteLine($"Expected CodeText: Test123, Actual: {result.CodeText}");
                        Console.WriteLine($"Expected XDimension: 2, Actual: {importedGenerator.Parameters.Barcode.XDimension.Point}");
                    }
                }
            }
        }

        // Clean up temporary files, ignoring any errors that may occur.
        try { if (File.Exists(xmlPath)) File.Delete(xmlPath); } catch { }
        try { if (File.Exists(imgPath)) File.Delete(imgPath); } catch { }
    }
}