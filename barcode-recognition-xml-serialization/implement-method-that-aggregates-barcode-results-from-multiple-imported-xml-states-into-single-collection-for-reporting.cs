using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare sample XML files that contain barcode generator settings
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeXmlDemo");
        Directory.CreateDirectory(tempDir);

        var xmlFiles = new List<string>();

        // Create three different barcode generators and export their settings to XML
        CreateAndExportGenerator(EncodeTypes.Code128, "ABC123", Path.Combine(tempDir, "code128.xml"));
        CreateAndExportGenerator(EncodeTypes.QR, "https://example.com", Path.Combine(tempDir, "qr.xml"));
        CreateAndExportGenerator(EncodeTypes.EAN13, "5901234123457", Path.Combine(tempDir, "ean13.xml"));

        xmlFiles.Add(Path.Combine(tempDir, "code128.xml"));
        xmlFiles.Add(Path.Combine(tempDir, "qr.xml"));
        xmlFiles.Add(Path.Combine(tempDir, "ean13.xml"));

        // Aggregate barcode results from all imported XML states
        var aggregatedResults = new List<BarCodeResult>();

        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"XML file not found: {xmlPath}");
                continue;
            }

            // Import generator settings from XML
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Generate barcode image in memory
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Read barcodes from the generated image
                    using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            aggregatedResults.Add(result);
                        }
                    }
                }
            }
        }

        // Reporting: output aggregated results
        Console.WriteLine("Aggregated Barcode Results:");
        foreach (BarCodeResult result in aggregatedResults)
        {
            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
        }

        // Clean up temporary files
        try
        {
            foreach (string file in xmlFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }

    // Helper method to create a generator, set basic properties, and export its settings to XML
    private static void CreateAndExportGenerator(BaseEncodeType encodeType, string codeText, string xmlPath)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Minimal configuration – can be extended as needed
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Export settings to XML file
            generator.ExportToXml(xmlPath);
        }
    }
}