using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for files
        string barcodeImagePath = "barcode.png";
        string generatorXmlPath = "generator_state.xml";
        string readerXmlPath = "reader_state.xml";
        string logPath = "log.txt";

        // Create a barcode generator, generate image and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Export generator settings to XML
            bool genExported = generator.ExportToXml(generatorXmlPath);
            if (!genExported)
            {
                Console.WriteLine("Failed to export generator XML.");
            }

            // Save barcode image
            generator.Save(barcodeImagePath);
        }

        // Verify that the image file exists before reading
        if (!File.Exists(barcodeImagePath))
        {
            Console.WriteLine($"Barcode image not found at path: {barcodeImagePath}");
            return;
        }

        // Create a barcode reader and set the image using the file path
        using (var reader = new BarCodeReader())
        {
            // Record the file path used in SetBarCodeImage
            string setImageInfo = $"SetBarCodeImage called with path: {barcodeImagePath}";

            // Set the image for recognition
            reader.SetBarCodeImage(barcodeImagePath);

            // Export reader settings to XML
            bool readerExported = reader.ExportToXml(readerXmlPath);
            if (!readerExported)
            {
                Console.WriteLine("Failed to export reader XML.");
            }

            // Read barcodes (optional, just to demonstrate usage)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Build log content
            string logContent = $"{setImageInfo}{Environment.NewLine}" +
                                $"Generator XML ({generatorXmlPath}):{Environment.NewLine}" +
                                $"{File.ReadAllText(generatorXmlPath)}{Environment.NewLine}" +
                                $"Reader XML ({readerXmlPath}):{Environment.NewLine}" +
                                $"{File.ReadAllText(readerXmlPath)}";

            // Write log to file
            File.WriteAllText(logPath, logContent);
        }

        Console.WriteLine("Processing completed. Log written to " + logPath);
    }
}