// Title: Read Barcode XML State from MemoryStream and Continue Processing
// Description: Demonstrates how to export a BarCodeReader's state to an XML MemoryStream, import it back, and read the barcode again without reloading the image file.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to use BarCodeReader.ExportToXml and BarCodeReader.ImportFromXml together with SetBarCodeImage to persist and restore reader configuration. Typical use cases include saving scanner settings, processing pipelines, and avoiding redundant image loading. Developers working with barcode recognition often need to serialize reader state for later reuse or for distributed processing.
// Prompt: Show how to read an XML state from a MemoryStream and continue processing without reloading the image file.
// Tags: qr barcode, export xml, import xml, memorystream, barcodereader, aspose.barcode, barcode recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, exports the reader state to XML,
/// imports the state back, and reads the barcode again without reloading the image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, state export/import,
    /// and demonstrates continued processing using the same image stream.
    /// </summary>
    static void Main()
    {
        const string sampleText = "HelloWorld";

        // Generate a QR barcode and keep it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode image to the memory stream in PNG format
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                imageStream.Position = 0;

                // Initial read of the barcode using BarCodeReader
                using (var reader = new BarCodeReader(imageStream, DecodeType.QR))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"First read: {result.CodeTypeName} - {result.CodeText}");
                    }

                    // Export the current reader state to an XML memory stream
                    using (var xmlStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStream);
                        // Reset XML stream position for import
                        xmlStream.Position = 0;

                        // Import the reader state from the XML stream
                        using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                        {
                            // Reassign the same image without reloading from a file
                            imageStream.Position = 0;
                            importedReader.SetBarCodeImage(imageStream);
                            importedReader.SetBarCodeReadType(DecodeType.QR);

                            // Read the barcode again using the imported reader state
                            foreach (BarCodeResult result in importedReader.ReadBarCodes())
                            {
                                Console.WriteLine($"After import: {result.CodeTypeName} - {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}