// Title: Read XML State from MemoryStream and Continue Barcode Processing
// Description: Demonstrates how to export a BarCodeReader's state to an XML MemoryStream, import it back, and reuse the same barcode image without reloading from disk.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to serialize and deserialize BarCodeReader settings using XML streams. It uses BarcodeGenerator, BarCodeReader, and related classes to generate a QR code, export the reader's configuration, and reapply it. Developers working with barcode recognition often need to persist reader settings across sessions or share them between components, and this snippet illustrates the typical workflow.
// Prompt: Show how to read an XML state from a MemoryStream and continue processing without reloading the image file.
// Tags: qr, barcode, read, xml, memorystream, state, export, import, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, exports the reader state to XML,
/// imports the state, and reads the barcode again without reloading the image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, state export/import,
    /// and demonstrates reading the barcode twice.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode and keep the image in a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a barcode generator for QR code with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
            {
                // Set QR error correction level to Medium
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated barcode image to the memory stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            barcodeStream.Position = 0;

            // First read: create a BarCodeReader, set image, and read barcodes
            using (var reader = new BarCodeReader())
            {
                // Assign the barcode image from the memory stream
                reader.SetBarCodeImage(barcodeStream);

                // Restrict reading to QR codes only
                reader.SetBarCodeReadType(DecodeType.QR);

                Console.WriteLine("First read results:");
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }

                // Export the reader state to an XML memory stream
                using (var stateStream = new MemoryStream())
                {
                    reader.ExportToXml(stateStream);
                    stateStream.Position = 0; // Reset for reading

                    // Import the reader state from the XML stream
                    using (var importedReader = BarCodeReader.ImportFromXml(stateStream))
                    {
                        // Reuse the same barcode image without reloading from file
                        barcodeStream.Position = 0;
                        importedReader.SetBarCodeImage(barcodeStream);
                        importedReader.SetBarCodeReadType(DecodeType.QR);

                        Console.WriteLine("Second read results after importing state:");
                        foreach (var result in importedReader.ReadBarCodes())
                        {
                            Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}