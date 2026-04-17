using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file paths
        string imagePath = "barcode_original.png";
        string xmlPath = "barcode_settings.xml";
        string importedImagePath = "barcode_imported.png";

        // Step 1: Create original barcode, save image and export settings to XML
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Example of setting a parameter (optional)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save original barcode image
            generator.Save(imagePath);

            // Export generator settings to XML
            bool exportSuccess = generator.ExportToXml(xmlPath);
            if (!exportSuccess)
            {
                Console.WriteLine("Failed to export barcode settings to XML.");
                return;
            }
        }

        // Verify XML file exists before import
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file not found: " + xmlPath);
            return;
        }

        // Step 2: Import settings from XML and generate barcode again
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import barcode settings from XML.");
            return;
        }

        using (importedGenerator)
        {
            // Save barcode generated from imported settings
            importedGenerator.Save(importedImagePath);
        }

        // Verify images exist before reading
        if (!File.Exists(imagePath) || !File.Exists(importedImagePath))
        {
            Console.WriteLine("One or both barcode images are missing.");
            return;
        }

        // Step 3: Read original barcode
        BarCodeResult[] originalResults;
        using (BarCodeReader readerOriginal = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            originalResults = readerOriginal.ReadBarCodes();
        }

        // Step 4: Read imported barcode
        BarCodeResult[] importedResults;
        using (BarCodeReader readerImported = new BarCodeReader(importedImagePath, DecodeType.Code128))
        {
            importedResults = readerImported.ReadBarCodes();
        }

        // Step 5: Compare results
        Console.WriteLine("=== Comparison Report ===");
        if (originalResults.Length != importedResults.Length)
        {
            Console.WriteLine($"Different number of barcodes detected. Original: {originalResults.Length}, Imported: {importedResults.Length}");
        }

        int count = Math.Min(originalResults.Length, importedResults.Length);
        for (int i = 0; i < count; i++)
        {
            BarCodeResult orig = originalResults[i];
            BarCodeResult imp = importedResults[i];

            Console.WriteLine($"Barcode #{i + 1}:");
            Console.WriteLine($"  Code Text - Original: {orig.CodeText}, Imported: {imp.CodeText} {(orig.CodeText == imp.CodeText ? "(Match)" : "(Mismatch)")}");
            Console.WriteLine($"  Type Name - Original: {orig.CodeTypeName}, Imported: {imp.CodeTypeName} {(orig.CodeTypeName == imp.CodeTypeName ? "(Match)" : "(Mismatch)")}");
            Console.WriteLine($"  Confidence - Original: {orig.Confidence}, Imported: {imp.Confidence} {(orig.Confidence == imp.Confidence ? "(Match)" : "(Mismatch)")}");
            Console.WriteLine($"  Reading Quality - Original: {orig.ReadingQuality}, Imported: {imp.ReadingQuality} {(orig.ReadingQuality == imp.ReadingQuality ? "(Match)" : "(Mismatch)")}");
        }

        if (count == 0)
        {
            Console.WriteLine("No barcodes were detected in either image.");
        }
    }
}