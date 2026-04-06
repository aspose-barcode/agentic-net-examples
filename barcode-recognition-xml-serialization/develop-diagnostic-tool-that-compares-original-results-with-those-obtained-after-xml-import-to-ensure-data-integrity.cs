using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file paths
        string originalImagePath = Path.Combine(Path.GetTempPath(), "original.png");
        string importedImagePath = Path.Combine(Path.GetTempPath(), "imported.png");
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode.xml");

        // Create original barcode, save image and export settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(originalImagePath);
            generator.ExportToXml(xmlPath);
        }

        // Recognize original barcode
        BarCodeResult originalResult = null;
        using (var reader = new BarCodeReader(originalImagePath, DecodeType.EAN13))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                originalResult = result;
                break; // Expect only one barcode
            }
        }

        // Import generator from XML, save imported image
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            importedGenerator.Save(importedImagePath);
        }

        // Recognize imported barcode
        BarCodeResult importedResult = null;
        using (var reader = new BarCodeReader(importedImagePath, DecodeType.EAN13))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                importedResult = result;
                break; // Expect only one barcode
            }
        }

        // Validate that both recognitions succeeded
        if (originalResult == null || importedResult == null)
        {
            Console.WriteLine("Failed to read one of the barcodes.");
            return;
        }

        // Compare results
        bool codeTextEqual = originalResult.CodeText == importedResult.CodeText;
        bool valueEqual = originalResult.Extended.OneD.Value == importedResult.Extended.OneD.Value;
        bool checksumEqual = originalResult.Extended.OneD.CheckSum == importedResult.Extended.OneD.CheckSum;

        Console.WriteLine("Comparison of original vs. imported barcode recognition:");
        Console.WriteLine($"CodeText equal: {codeTextEqual}");
        Console.WriteLine($"Value equal: {valueEqual}");
        Console.WriteLine($"Checksum equal: {checksumEqual}");

        // Detailed output
        Console.WriteLine("\nOriginal Result:");
        Console.WriteLine($"  CodeText: {originalResult.CodeText}");
        Console.WriteLine($"  Value: {originalResult.Extended.OneD.Value}");
        Console.WriteLine($"  Checksum: {originalResult.Extended.OneD.CheckSum}");

        Console.WriteLine("\nImported Result:");
        Console.WriteLine($"  CodeText: {importedResult.CodeText}");
        Console.WriteLine($"  Value: {importedResult.Extended.OneD.Value}");
        Console.WriteLine($"  Checksum: {importedResult.Extended.OneD.CheckSum}");
    }
}