using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the generated barcode image and the exported XML state
        string imagePath = "barcode.png";
        string xmlPath = "barcode.xml";

        // -----------------------------------------------------------------
        // Step 1: Create a sample barcode and export its settings to XML
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "12345";
            generator.Save(imagePath);
            generator.ExportToXml(xmlPath);
        }

        // Verify that the files were created
        if (!File.Exists(imagePath) || !File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create required files.");
            return;
        }

        // ---------------------------------------------------------------
        // Step 2: Import the saved XML state into a new BarCodeReader
        // ---------------------------------------------------------------
        using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath))
        {
            if (reader == null)
            {
                Console.WriteLine("ImportFromXml returned null – unable to load settings.");
                return;
            }

            // Set the image that will be processed
            reader.SetBarCodeImage(imagePath);

            // Read and display detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}