using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the barcode image to be processed
        string imagePath = "barcode.png";

        // Path where the recognition state XML will be saved
        string xmlPath = "recognition_state.xml";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file not found at '{imagePath}'.");
            return;
        }

        // Initialize the barcode reader with the image file
        using (var reader = new BarCodeReader(imagePath))
        {
            // Perform barcode recognition
            var results = reader.ReadBarCodes();

            // Output recognized barcodes to the console (optional)
            foreach (var result in results)
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine();
            }

            // Export the recognition state to an XML file
            bool success = reader.ExportToXml(xmlPath);
            Console.WriteLine(success
                ? $"Recognition state exported successfully to '{xmlPath}'."
                : "Failed to export recognition state.");
        }
    }
}