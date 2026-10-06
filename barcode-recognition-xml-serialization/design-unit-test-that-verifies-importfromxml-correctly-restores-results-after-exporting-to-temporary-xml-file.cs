// Title: ImportFromXml restores barcode reader state from XML
// Description: Demonstrates exporting a BarCodeReader state to an XML file and importing it back to verify that the decoded barcode results are preserved.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on persisting and restoring reader state via XML. It showcases the use of BarcodeGenerator, BarCodeReader, ExportToXml, and ImportFromXml APIs—common tasks for developers who need to cache recognition results, share reader configurations, or implement repeatable tests across sessions.
// Prompt: Design a unit test that verifies ImportFromXml correctly restores results after exporting to a temporary XML file.
// Tags: barcode, symbology, import, export, xml, aspose.barcode, generation, recognition, unit-test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR barcode, reads it, exports the reader state to XML,
/// imports the state back, and verifies that the decoded results are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, export/import, and validation steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the barcode image and the exported XML state
        string barcodePath = Path.Combine(tempDir, "barcode.png");
        string xmlPath = Path.Combine(tempDir, "readerState.xml");

        // Variables to hold the original decoding results
        string originalCodeText = null;
        string originalCodeType = null;

        // -------------------------------------------------
        // Generate a QR barcode image and save it to disk
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Read the barcode, capture results, and export reader state to XML
        // -------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                originalCodeText = results[0].CodeText;
                originalCodeType = results[0].CodeTypeName;
            }

            // Persist the reader's internal state for later reuse
            reader.ExportToXml(xmlPath);
        }

        // -------------------------------------------------
        // Import the previously saved reader state from XML and read again
        // -------------------------------------------------
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Reassign the image and decode type to the imported reader
            importedReader.SetBarCodeImage(barcodePath);
            importedReader.SetBarCodeReadType(DecodeType.QR);

            var results = importedReader.ReadBarCodes();

            // Capture the results after import
            string importedCodeText = null;
            string importedCodeType = null;
            if (results.Length > 0)
            {
                importedCodeText = results[0].CodeText;
                importedCodeType = results[0].CodeTypeName;
            }

            // Verify that the imported results match the original ones
            bool restoredCorrectly = originalCodeText == importedCodeText && originalCodeType == importedCodeType;
            Console.WriteLine($"ImportFromXml restored correctly: {restoredCorrectly}");
        }

        // -------------------------------------------------
        // Cleanup temporary files and directory
        // -------------------------------------------------
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}