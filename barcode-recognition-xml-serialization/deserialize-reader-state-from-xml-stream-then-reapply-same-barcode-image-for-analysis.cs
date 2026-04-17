using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image (Code128 with text "12345")
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a temporary BarCodeReader to configure and export its state to XML
                using (var tempReader = new BarCodeReader())
                {
                    // Set the decode type to Code128 (using MultiDecodeType)
                    tempReader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);

                    // Export the reader's state to an in‑memory XML stream
                    using (var xmlStream = new MemoryStream())
                    {
                        tempReader.ExportToXml(xmlStream);
                        xmlStream.Position = 0; // Reset for reading

                        // Import a new BarCodeReader instance from the XML stream
                        using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlStream))
                        {
                            // Assign the previously generated barcode image to the reader
                            reader.SetBarCodeImage(barcodeImage);

                            // Perform barcode recognition
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                                Console.WriteLine($"Barcode Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}