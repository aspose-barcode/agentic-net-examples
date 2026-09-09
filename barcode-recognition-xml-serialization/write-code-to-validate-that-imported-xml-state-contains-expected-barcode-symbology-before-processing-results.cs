// Title: Validate barcode symbology from imported XML state
// Description: Demonstrates generating a QR barcode, exporting the reader state to XML, importing it back, and confirming that the detected symbology matches the expected type.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to use BarcodeGenerator, BarCodeReader, and the ExportToXml/ImportFromXml APIs. Typical use cases include persisting recognition settings, sharing reader configurations across services, and validating that imported states still correspond to the intended barcode symbology. Developers often need to verify symbology before processing results to ensure data integrity.
// Prompt: Write code to validate that an imported XML state contains the expected barcode symbology before processing results.
// Tags: barcode symbology, validation, xml, export, import, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that validates the barcode symbology after importing a BarCodeReader state from XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, exports the reader state, re-imports it, and checks that the detected symbology matches the expected value.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working directory for generated files.
        // --------------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeValidate_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        string barcodePath = Path.Combine(workDir, "qr.png");
        string readerXmlPath = Path.Combine(workDir, "readerState.xml");
        string expectedSymbology = "QR";

        // --------------------------------------------------------------------
        // Generate a QR barcode image and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Create a BarCodeReader, read the barcode, and export its internal state to XML.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            var initialResults = reader.ReadBarCodes();
            Console.WriteLine($"Initial read count: {initialResults.Length}");
            reader.ExportToXml(readerXmlPath);
        }

        // --------------------------------------------------------------------
        // Import the BarCodeReader state from the previously saved XML file.
        // --------------------------------------------------------------------
        using (var importedReader = BarCodeReader.ImportFromXml(readerXmlPath))
        {
            // The image source is not stored in XML, so set it explicitly.
            importedReader.SetBarCodeImage(barcodePath);

            // Resolve the expected symbology string to the corresponding BaseDecodeType via reflection.
            var field = typeof(DecodeType).GetField(expectedSymbology);
            if (field == null)
            {
                Console.WriteLine($"Unknown expected symbology: {expectedSymbology}");
                return;
            }
            BaseDecodeType expectedDecode = (BaseDecodeType)field.GetValue(null);

            // Read barcodes using the imported state.
            var results = importedReader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate over detection results and verify symbology.
                foreach (var result in results)
                {
                    bool matches = result.CodeType.Equals(expectedDecode);
                    Console.WriteLine($"Detected: {result.CodeTypeName}, Text: {result.CodeText}, MatchExpected: {matches}");
                    if (!matches)
                    {
                        Console.WriteLine("Validation failed: unexpected symbology.");
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and directory.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(readerXmlPath)) File.Delete(readerXmlPath);
            Directory.Delete(workDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit.
        }
    }
}