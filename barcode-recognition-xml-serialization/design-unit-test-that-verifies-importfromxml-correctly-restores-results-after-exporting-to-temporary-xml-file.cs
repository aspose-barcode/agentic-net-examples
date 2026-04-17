using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary file paths
        string tempXml = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xml");
        string originalImage = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
        string importedImage = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

        // Expected barcode text
        const string expectedText = "Test123";

        // Create original barcode generator and export its settings to XML
        using (BarcodeGenerator originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, expectedText))
        {
            // Export generator settings to XML file
            bool exportSuccess = originalGenerator.ExportToXml(tempXml);
            if (!exportSuccess)
            {
                Console.WriteLine("Failed to export generator settings to XML.");
                return;
            }

            // Save original barcode image
            originalGenerator.Save(originalImage);
        }

        // Import generator settings from XML
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(tempXml);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import generator settings from XML.");
            return;
        }

        // Save imported barcode image
        using (importedGenerator)
        {
            importedGenerator.Save(importedImage);
        }

        // Read barcode from original image
        string originalReadText;
        using (BarCodeReader reader = new BarCodeReader(originalImage, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in original image.");
                return;
            }
            originalReadText = results[0].CodeText;
        }

        // Read barcode from imported image
        string importedReadText;
        using (BarCodeReader reader = new BarCodeReader(importedImage, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in imported image.");
                return;
            }
            importedReadText = results[0].CodeText;
        }

        // Verify that both readings match the expected text and each other
        bool testPassed = originalReadText == expectedText && importedReadText == expectedText && originalReadText == importedReadText;
        Console.WriteLine(testPassed ? "ImportFromXml test passed." : "ImportFromXml test failed.");

        // Clean up temporary files
        try { File.Delete(tempXml); } catch { }
        try { File.Delete(originalImage); } catch { }
        try { File.Delete(importedImage); } catch { }
    }
}