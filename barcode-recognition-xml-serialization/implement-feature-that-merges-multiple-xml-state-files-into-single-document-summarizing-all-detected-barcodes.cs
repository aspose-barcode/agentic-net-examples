using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample XML state files to be created and later merged
        string[] xmlFiles = { "barcode1.xml", "barcode2.xml", "barcode3.xml" };

        // Create sample barcode generators and export their state to XML files
        CreateSampleXmlStates(xmlFiles);

        // List to hold summary lines for each detected barcode
        List<string> summaryLines = new List<string>();

        // Process each XML state file: import, generate image, read barcodes, collect info
        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"Warning: XML file not found: {xmlPath}");
                continue;
            }

            // Import generator settings from XML
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Generate barcode image in memory
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Prepare reader to detect any barcode type
                    using (BarCodeReader reader = new BarCodeReader())
                    {
                        reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                        reader.SetBarCodeImage(bitmap);

                        // Read all barcodes from the image
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            string line = $"{Path.GetFileName(xmlPath)}: Type={result.CodeTypeName}, Text={result.CodeText}";
                            summaryLines.Add(line);
                        }
                    }
                }
            }
        }

        // Write the summary to a text file
        string summaryPath = "summary.txt";
        File.WriteAllLines(summaryPath, summaryLines);
        Console.WriteLine($"Summary written to {summaryPath}");
    }

    // Helper method to create sample barcode generators and export their state to XML files
    private static void CreateSampleXmlStates(string[] xmlPaths)
    {
        // Define sample barcode configurations
        var configs = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.EAN13, "1234567890128")
        };

        for (int i = 0; i < xmlPaths.Length && i < configs.Length; i++)
        {
            var (type, text) = configs[i];
            using (BarcodeGenerator generator = new BarcodeGenerator(type, text))
            {
                // Export generator settings to XML file
                bool exported = generator.ExportToXml(xmlPaths[i]);
                if (!exported)
                {
                    Console.WriteLine($"Failed to export XML for {xmlPaths[i]}");
                }
            }
        }
    }
}