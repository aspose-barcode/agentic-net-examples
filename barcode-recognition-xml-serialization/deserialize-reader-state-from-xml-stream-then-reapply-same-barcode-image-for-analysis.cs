using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image (Code128) in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Create the bitmap image.
            var barcodeBitmap = generator.GenerateBarCodeImage();

            // Export the reader settings to an XML stream.
            using (var initialReader = new BarCodeReader(barcodeBitmap, DecodeType.Code128))
            {
                using (var xmlStream = new MemoryStream())
                {
                    // Export current reader configuration to XML.
                    initialReader.ExportToXml(xmlStream);
                    xmlStream.Position = 0; // Reset for reading.

                    // Create a new reader instance and import the previously saved settings.
                    using (var importedReader = new BarCodeReader())
                    {
                        // Import settings from the XML stream.
                        BarCodeReader.ImportFromXml(xmlStream);

                        // Reapply the same barcode image for analysis.
                        importedReader.SetBarCodeImage(barcodeBitmap);

                        // Perform barcode recognition.
                        foreach (var result in importedReader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Detected Text: {result.CodeText}");
                        }
                    }
                }
            }

            // Dispose the bitmap after all operations are complete.
            barcodeBitmap.Dispose();
        }
    }
}