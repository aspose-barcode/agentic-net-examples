using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and exporting the recognition state to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Handles barcode generation (if needed), reads the barcode, prints details,
    /// and exports the full recognition state to an XML file.
    /// </summary>
    static void Main()
    {
        // Define file paths for the barcode image and the exported XML.
        string imagePath = "barcode.png";
        string xmlPath = "recognition_state.xml";

        // If the barcode image does not exist, generate a sample Code128 barcode.
        if (!File.Exists(imagePath))
        {
            // Create a BarcodeGenerator with the desired encoding and data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode image to the specified path.
                generator.Save(imagePath);
                Console.WriteLine($"Sample barcode image created at '{imagePath}'.");
            }
        }

        // Initialize a BarCodeReader to detect all supported barcode types in the image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition and retrieve all detected barcodes.
            var results = reader.ReadBarCodes();

            // Iterate through each recognition result and output its details.
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine();
            }

            // Export the complete recognition state (including all detected barcodes) to an XML file.
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Recognition state exported to '{xmlPath}'.");
        }
    }
}