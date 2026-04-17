using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string barcodeFile = "sample.png";
        const string readerXmlFile = "readerSettings.xml";

        // Ensure a sample barcode image exists
        if (!File.Exists(barcodeFile))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
            {
                generator.Save(barcodeFile);
            }
        }

        // Create a BarCodeReader, configure decode types, and export its settings to XML.
        // Note: We do NOT set any image for the reader.
        using (var tempReader = new BarCodeReader())
        {
            // Example: set decode types (optional, can be omitted)
            tempReader.BarCodeReadType = DecodeType.Code128;
            // Export settings (no image) to XML file
            tempReader.ExportToXml(readerXmlFile);
        }

        // Import BarCodeReader settings from XML.
        // This reader does NOT have an image assigned yet.
        BarCodeReader reader = null;
        try
        {
            reader = BarCodeReader.ImportFromXml(readerXmlFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to import reader settings: {ex.Message}");
            return;
        }

        // Attempt to read barcodes without providing an image.
        // This should raise an exception because SetBarCodeImage was not called.
        try
        {
            var results = reader.ReadBarCodes();
            Console.WriteLine("Unexpectedly succeeded without an image.");
        }
        catch (BarCodeRecognitionException ex)
        {
            Console.WriteLine($"Recognition error (expected): {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General error (expected): {ex.Message}");
        }

        // Now provide the required barcode image.
        if (File.Exists(barcodeFile))
        {
            try
            {
                reader.SetBarCodeImage(barcodeFile);
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error after setting image: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Barcode image file not found.");
        }

        // Clean up
        reader?.Dispose();

        // Optionally delete temporary XML file
        if (File.Exists(readerXmlFile))
        {
            try { File.Delete(readerXmlFile); } catch { }
        }
    }
}