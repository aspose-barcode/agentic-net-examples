using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary file paths
        string xmlPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xml");
        string imgPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

        // Original generator with specific settings
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            originalGenerator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Export settings to XML
            bool exportResult = originalGenerator.ExportToXml(xmlPath);
            if (!exportResult)
            {
                Console.WriteLine("Export to XML failed.");
                return;
            }
        }

        // Import settings from XML into a new generator
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("Import from XML returned null.");
            return;
        }

        // Save barcode image using imported settings
        using (importedGenerator)
        {
            importedGenerator.Save(imgPath);
        }

        // Read the barcode back and verify
        bool testPassed = false;
        using (var reader = new BarCodeReader(imgPath, DecodeType.EAN13))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Verify code text matches original
                if (result.CodeText == "1234567890128")
                {
                    // Verify checksum matches last digit
                    if (result.Extended.OneD.CheckSum == "8")
                    {
                        testPassed = true;
                    }
                    else
                    {
                        Console.WriteLine($"Checksum mismatch: expected 8, got {result.Extended.OneD.CheckSum}");
                    }
                }
                else
                {
                    Console.WriteLine($"CodeText mismatch: expected 1234567890128, got {result.CodeText}");
                }
            }
        }

        // Output result
        if (testPassed)
        {
            Console.WriteLine("Test Passed: ImportFromXml restored barcode correctly.");
        }
        else
        {
            Console.WriteLine("Test Failed: Imported barcode did not match expected values.");
        }

        // Clean up temporary files
        try { File.Delete(xmlPath); } catch { }
        try { File.Delete(imgPath); } catch { }
    }
}