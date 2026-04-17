using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a barcode and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0;

                // First read using BarCodeReader
                using (var reader = new BarCodeReader())
                {
                    reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);
                    reader.SetBarCodeImage(imageStream);
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"First read - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }

                // Export reader state to XML in another memory stream
                using (var readerForExport = new BarCodeReader())
                {
                    readerForExport.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);
                    readerForExport.SetBarCodeImage(imageStream);
                    // Perform a read to ensure internal state is populated
                    readerForExport.ReadBarCodes();

                    using (var xmlStream = new MemoryStream())
                    {
                        readerForExport.ExportToXml(xmlStream);
                        xmlStream.Position = 0; // Reset before import

                        // Import from XML without reloading the image file
                        using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                        {
                            // Reuse the same image stream (reset position first)
                            imageStream.Position = 0;
                            importedReader.SetBarCodeImage(imageStream);
                            foreach (var result in importedReader.ReadBarCodes())
                            {
                                Console.WriteLine($"After import - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}