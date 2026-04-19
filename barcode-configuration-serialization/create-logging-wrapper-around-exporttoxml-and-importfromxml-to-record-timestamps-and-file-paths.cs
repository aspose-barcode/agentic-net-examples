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
        string barcodeImagePath = "barcode.png";
        string generatorXmlPath = "generator.xml";
        string importedBarcodeImagePath = "imported.png";
        string readerXmlPath = "reader.xml";

        // Ensure clean start
        DeleteIfExists(barcodeImagePath);
        DeleteIfExists(generatorXmlPath);
        DeleteIfExists(importedBarcodeImagePath);
        DeleteIfExists(readerXmlPath);

        // Create a barcode generator, save image and export its settings to XML with logging
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(barcodeImagePath);
            ExportGeneratorToXml(generator, generatorXmlPath);
        }

        // Import generator settings from XML with logging and save a new image
        var importedGenerator = ImportGeneratorFromXml(generatorXmlPath);
        importedGenerator.Save(importedBarcodeImagePath);
        importedGenerator.Dispose();

        // Create a barcode reader for the original image, export its settings to XML with logging
        using (var reader = new BarCodeReader(barcodeImagePath, DecodeType.Code128))
        {
            ExportReaderToXml(reader, readerXmlPath);
        }

        // Import reader settings from XML with logging, set the image, and read barcodes
        var importedReader = ImportReaderFromXml(readerXmlPath);
        importedReader.SetBarCodeImage(barcodeImagePath);
        foreach (var result in importedReader.ReadBarCodes())
        {
            Log($"Read barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
        }
        importedReader.Dispose();
    }

    static void DeleteIfExists(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    static bool ExportGeneratorToXml(BarcodeGenerator generator, string xmlPath)
    {
        bool success = generator.ExportToXml(xmlPath);
        Log($"ExportGeneratorToXml - Path: {xmlPath}, Success: {success}, Time: {DateTime.Now}");
        return success;
    }

    static BarcodeGenerator ImportGeneratorFromXml(string xmlPath)
    {
        var generator = BarcodeGenerator.ImportFromXml(xmlPath);
        Log($"ImportGeneratorFromXml - Path: {xmlPath}, Time: {DateTime.Now}");
        return generator;
    }

    static bool ExportReaderToXml(BarCodeReader reader, string xmlPath)
    {
        bool success = reader.ExportToXml(xmlPath);
        Log($"ExportReaderToXml - Path: {xmlPath}, Success: {success}, Time: {DateTime.Now}");
        return success;
    }

    static BarCodeReader ImportReaderFromXml(string xmlPath)
    {
        var reader = BarCodeReader.ImportFromXml(xmlPath);
        Log($"ImportReaderFromXml - Path: {xmlPath}, Time: {DateTime.Now}");
        return reader;
    }

    static void Log(string message)
    {
        Console.WriteLine(message);
    }
}