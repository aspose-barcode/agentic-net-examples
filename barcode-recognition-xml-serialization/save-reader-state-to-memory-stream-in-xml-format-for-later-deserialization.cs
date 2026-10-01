// Title: Save BarCodeReader state to XML in a memory stream
// Description: Demonstrates generating a Code128 barcode, exporting the BarCodeReader configuration to an XML memory stream, and later importing it to perform barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and their ExportToXml/ImportFromXml methods. Typical use cases include persisting reader settings for later reuse, configuring checksum validation, and processing barcodes in memory without intermediate files. Developers often need to serialize reader state to XML for configuration management or distributed processing scenarios.
// Prompt: Save the reader state to a memory stream in XML format for later deserialization.
// Tags: barcode, code128, generation, recognition, xml, memorystream, export, import, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, exports the reader state to XML,
/// and imports it back for barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the barcode generation, state export/import, and decoding.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and keep it in a memory stream.
        using (var barcodeStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                // Save the barcode image as PNG into the memory stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading the image.
            barcodeStream.Position = 0;

            // Create a BarCodeReader for the generated image.
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
            {
                // Enable checksum validation as an example setting.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Export the reader's configuration (state) to XML in a memory stream.
                using (var xmlStream = new MemoryStream())
                {
                    reader.ExportToXml(xmlStream);

                    // Reset XML stream position for reading.
                    xmlStream.Position = 0;

                    // Import a new BarCodeReader from the exported XML.
                    using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                    {
                        // The image source is not stored in the XML, so set it again.
                        barcodeStream.Position = 0;
                        importedReader.SetBarCodeImage(barcodeStream);

                        // Perform barcode recognition using the imported reader.
                        var results = importedReader.ReadBarCodes();
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Decoded Text: {result.CodeText}");
                            Console.WriteLine($"Symbology   : {result.CodeTypeName}");
                        }
                    }
                }
            }
        }
    }
}