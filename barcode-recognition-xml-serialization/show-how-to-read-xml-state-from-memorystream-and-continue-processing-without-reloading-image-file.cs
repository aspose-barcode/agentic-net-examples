// Title: Read Barcode Configuration XML from MemoryStream and Process Image Without Reloading
// Description: Demonstrates exporting a barcode generator's settings to XML, loading the XML from a stream, and using it with a BarCodeReader attached to an in‑memory image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and methods ExportToXml / ImportFromXml. Typical use cases include persisting barcode settings, transferring configuration between processes, and reading barcodes from images held in memory without reloading from disk. Developers often need to serialize generator state, reuse it later, and avoid unnecessary I/O for performance.
// Prompt: Show how to read an XML state from a MemoryStream and continue processing without reloading the image file.
// Tags: barcode symbology, generation, xml import, memorystream, reading, aspose.barcode, barcodereader, barcodegenerator

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that shows how to export barcode generator settings to XML,
/// import them into a <see cref="BarCodeReader"/>, and read barcodes from an image kept in memory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves its image to a <see cref="MemoryStream"/>,
    /// exports the generator configuration to an XML file, imports the XML into a reader,
    /// attaches the in‑memory image, and reads the barcode without reloading the file.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text.
        using (var barcodeGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Store the generated barcode image in a memory stream.
            using (var imageStream = new MemoryStream())
            {
                barcodeGenerator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Rewind the stream for later reading.

                // Export the generator's configuration to a temporary XML file.
                string xmlPath = Path.Combine(Path.GetTempPath(),
                    "barcodeConfig_" + Guid.NewGuid().ToString("N") + ".xml");
                barcodeGenerator.ExportToXml(xmlPath);

                // Open the XML file as a stream for importing.
                using (var xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    // Import the saved settings into a BarCodeReader instance.
                    using (var reader = BarCodeReader.ImportFromXml(xmlStream))
                    {
                        // Attach the previously saved image stream to the reader.
                        reader.SetBarCodeImage(imageStream);

                        // Iterate through all detected barcodes in the image.
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"CodeText: {result.CodeText}");
                            Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        }
                    }
                }

                // Delete the temporary XML configuration file.
                if (File.Exists(xmlPath))
                {
                    File.Delete(xmlPath);
                }
            }
        }
    }
}