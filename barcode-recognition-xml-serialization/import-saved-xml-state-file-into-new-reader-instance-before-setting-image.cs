using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a barcode image, exporting its settings to XML,
/// and then importing those settings to read the barcode from the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Paths for the barcode image and the XML settings file.
        string xmlPath = "readerSettings.xml";
        string imagePath = "barcode.png";

        // If either the barcode image or the XML settings file is missing,
        // generate a sample barcode and export its configuration.
        if (!File.Exists(imagePath) || !File.Exists(xmlPath))
        {
            // Create a barcode generator for Code128 with sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode image to the specified path.
                generator.Save(imagePath);

                // Export the generator's configuration to an XML file for later import.
                generator.ExportToXml(xmlPath);
            }
        }

        // Ensure the XML settings file exists before attempting to import it.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML settings file not found: {xmlPath}");
            return;
        }

        // Import reader settings from the XML file.
        using (var reader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Assign the barcode image to the reader.
            if (File.Exists(imagePath))
            {
                reader.SetBarCodeImage(imagePath);
            }
            else
            {
                Console.WriteLine($"Barcode image file not found: {imagePath}");
                return;
            }

            // Iterate through all recognized barcodes and display their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}