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
        string imagePath = "sample_barcode.png";
        string xmlPath = "barcode_config.xml";

        // Generate a barcode image and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Save the barcode image
            generator.Save(imagePath);
            // Export generator settings to XML
            generator.ExportToXml(xmlPath);
        }

        // Verify that the generated files exist
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML configuration not found at '{xmlPath}'.");
            return;
        }

        // Load the XML, add extra metadata with a different namespace
        string originalXml = File.ReadAllText(xmlPath);
        // Insert extra metadata element just before the closing root tag
        string extraMetadata = @"<extra:Metadata xmlns:extra=""http://example.com/extra"">AdditionalInfo</extra:Metadata>";
        string modifiedXml = originalXml.Replace("</BarCodeGenerator>", extraMetadata + Environment.NewLine + "</BarCodeGenerator>");
        File.WriteAllText(xmlPath, modifiedXml);

        // Import the XML configuration into a BarCodeReader instance
        using (var reader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Assign the barcode image to the reader
            reader.SetBarCodeImage(imagePath);

            // Perform barcode recognition
            var results = reader.ReadBarCodes();

            // Output recognition results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Barcode Text: {result.CodeText}");
                }
            }
        }
    }
}