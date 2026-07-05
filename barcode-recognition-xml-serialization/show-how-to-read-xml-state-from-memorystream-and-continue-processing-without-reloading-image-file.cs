// Title: Read XML State from MemoryStream and Continue Barcode Processing
// Description: Demonstrates exporting a BarCodeReader's state to XML stored in a MemoryStream, then importing it to continue recognition without reloading the image file.
// Prompt: Show how to read an XML state from a MemoryStream and continue processing without reloading the image file.
// Tags: qr, barcode, xml, memorystream, import, export, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, exports the reader state to XML,
/// imports it back, and continues processing without reloading the image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, reads it, exports the reader state,
    /// imports the state, and reads the barcode again using the same bitmap.
    /// </summary>
    static void Main()
    {
        // Generate a QR barcode and keep it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            using (var imgStream = new MemoryStream())
            {
                // Save the generated barcode image to the memory stream in PNG format
                generator.Save(imgStream, BarCodeImageFormat.Png);
                imgStream.Position = 0; // Reset stream position for reading

                // Load the image from the memory stream into a bitmap
                using (var bitmap = new Bitmap(imgStream))
                {
                    // First recognition pass using a BarCodeReader
                    using (var reader = new BarCodeReader(bitmap, DecodeType.QR))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"First read: {result.CodeText}");
                        }

                        // Export the reader's internal state to XML stored in a memory stream
                        using (var xmlStream = new MemoryStream())
                        {
                            reader.ExportToXml(xmlStream);
                            xmlStream.Position = 0; // Reset for reading the XML

                            // Import a new BarCodeReader instance from the XML state
                            var importedReader = BarCodeReader.ImportFromXml(xmlStream);
                            using (importedReader)
                            {
                                // Reassign the same bitmap (the image itself is not stored in the XML)
                                importedReader.SetBarCodeImage(bitmap);

                                // Continue processing without reloading the image file
                                foreach (var result in importedReader.ReadBarCodes())
                                {
                                    Console.WriteLine($"After import read: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}