using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a sample barcode and store it in a memory stream.
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position before reading.
            barcodeStream.Position = 0;

            // Create a BarCodeReader, assign the image, and read barcodes to populate its state.
            using (var reader = new BarCodeReader())
            {
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                reader.SetBarCodeImage(barcodeStream);
                reader.ReadBarCodes();

                // Export the reader state to XML in a memory stream.
                using (var xmlStream = new MemoryStream())
                {
                    bool exported = reader.ExportToXml(xmlStream);
                    Console.WriteLine("Export to XML successful: " + exported);

                    // Reset the XML stream position for deserialization.
                    xmlStream.Position = 0;

                    // Import the reader state from the XML stream.
                    using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                    {
                        if (importedReader != null)
                        {
                            Console.WriteLine("Reader state imported from XML.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to import reader state from XML.");
                        }
                    }
                }
            }
        }
    }
}