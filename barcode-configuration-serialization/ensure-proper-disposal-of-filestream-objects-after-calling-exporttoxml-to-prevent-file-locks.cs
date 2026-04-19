using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a barcode generator and set basic properties
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Sample123";

            // Export barcode properties to an XML file using a FileStream
            string xmlFilePath = "barcode_properties.xml";
            using (FileStream exportStream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bool exportResult = generator.ExportToXml(exportStream);
                Console.WriteLine($"Export to XML succeeded: {exportResult}");
            } // exportStream is disposed here, releasing the file lock

            // Import barcode properties back from the XML file using a FileStream
            using (FileStream importStream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(importStream))
                {
                    Console.WriteLine($"Imported CodeText: {importedGenerator.CodeText}");
                } // importedGenerator disposed
            } // importStream disposed

            // Save the barcode image to verify generation
            generator.Save("barcode.png");
        } // generator disposed
    }
}