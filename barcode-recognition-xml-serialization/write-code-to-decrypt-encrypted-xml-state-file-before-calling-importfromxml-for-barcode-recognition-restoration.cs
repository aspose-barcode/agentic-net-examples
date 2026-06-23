using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to import a barcode reader configuration from an XML file
/// and use it to read barcodes from an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Paths to the required input files.
        const string xmlPath = "encrypted_state.xml";
        const string barcodeImagePath = "barcode.png";

        // Verify that the XML configuration file exists.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Verify that the barcode image file exists.
        if (!File.Exists(barcodeImagePath))
        {
            Console.WriteLine($"Barcode image file not found: {barcodeImagePath}");
            return;
        }

        BarCodeReader reader;

        // Load the XML configuration into a memory stream and create the reader.
        using (FileStream fs = File.OpenRead(xmlPath))
        using (MemoryStream xmlStream = new MemoryStream())
        {
            // Copy file contents to the memory stream.
            fs.CopyTo(xmlStream);
            // Reset stream position to the beginning before importing.
            xmlStream.Position = 0;
            // Import the barcode reader configuration from the XML stream.
            reader = BarCodeReader.ImportFromXml(xmlStream);
        }

        // Open the barcode image and associate it with the reader.
        using (Bitmap bitmap = new Bitmap(barcodeImagePath))
        using (reader)
        {
            // Set the image that the reader will process.
            reader.SetBarCodeImage(bitmap);
            // Perform barcode detection.
            var results = reader.ReadBarCodes();

            // Check if any barcodes were detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Output details for each detected barcode.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Confidence: {result.Confidence}");
                }
            }
        }
    }
}