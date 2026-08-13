// Title: Read barcode XML state from MemoryStream and continue processing
// Description: Demonstrates exporting a BarCodeReader state to XML in memory and importing it back without reloading the image file.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to use BarCodeReader.ExportToXml and BarCodeReader.ImportFromXml. Developers often need to persist reader settings or share state across processes, and these APIs let you serialize and deserialize the reader while reusing the same bitmap image.
// Prompt: Show how to read an XML state from a MemoryStream and continue processing without reloading the image file.
// Tags: barcode, xml, memorystream, import, export, read, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting and importing BarCodeReader state using XML in memory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it, exports the reader state to XML, imports it back, and reads again without reloading the image.
    /// </summary>
    static void Main()
    {
        // Generate a barcode image in memory using Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Store the generated image in a MemoryStream.
            using (var imgStream = new MemoryStream())
            {
                generator.Save(imgStream, BarCodeImageFormat.Png);
                imgStream.Position = 0; // Reset stream position for reading.

                // Load the image from the stream into a Bitmap object.
                using (var bitmap = new Bitmap(imgStream))
                {
                    // First recognition pass using a BarCodeReader.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"First read: {result.CodeText}");
                        }

                        // Export the current reader state to an XML MemoryStream.
                        using (var xmlStream = new MemoryStream())
                        {
                            reader.ExportToXml(xmlStream);
                            xmlStream.Position = 0; // Reset for import.

                            // Import a new BarCodeReader from the XML state without reloading the image file.
                            using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                            {
                                // Assign the same bitmap image to the imported reader.
                                importedReader.SetBarCodeImage(bitmap);
                                // Optionally set the decode type (default is AllSupportedTypes).
                                importedReader.BarCodeReadType = DecodeType.Code128;

                                // Second recognition pass using the imported reader.
                                foreach (var result in importedReader.ReadBarCodes())
                                {
                                    Console.WriteLine($"After import: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}