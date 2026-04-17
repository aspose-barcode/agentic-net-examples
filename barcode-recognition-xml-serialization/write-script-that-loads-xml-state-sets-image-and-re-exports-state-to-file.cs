using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define file paths
        string xmlInputPath = "readerState.xml";
        string imagePath = "barcode.png";
        string xmlOutputPath = "exportedState.xml";

        // Ensure a sample barcode image exists
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "Sample123";
                generator.Save(imagePath);
            }
        }

        // If the input XML does not exist, create a minimal state file
        if (!File.Exists(xmlInputPath))
        {
            using (var tempReader = new BarCodeReader())
            {
                tempReader.SetBarCodeImage(imagePath);
                tempReader.ExportToXml(xmlInputPath);
            }
        }

        // Load BarCodeReader state from XML (or create a new instance if loading fails)
        using (BarCodeReader reader = File.Exists(xmlInputPath) ? BarCodeReader.ImportFromXml(xmlInputPath) : new BarCodeReader())
        {
            // Assign the barcode image to the reader
            reader.SetBarCodeImage(imagePath);

            // Export the current state to a new XML file
            bool exported = reader.ExportToXml(xmlOutputPath);
            Console.WriteLine($"Exported state to '{xmlOutputPath}': {exported}");
        }
    }
}