using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeRecognitionExample
{
    class Program
    {
        static void Main()
        {
            // Generate a sample barcode image into a memory stream
            using (var imageStream = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                }

                // Reset stream position before reading
                imageStream.Position = 0;

                // Perform recognition and get XML state
                string xmlState = RecognizeAndExportXml(imageStream);
                Console.WriteLine("Exported XML State:");
                Console.WriteLine(xmlState);
            }
        }

        /// <summary>
        /// Recognizes barcodes from the provided image stream and returns the reader's XML state.
        /// </summary>
        /// <param name="imageStream">Stream containing the barcode image.</param>
        /// <returns>XML representation of the BarCodeReader settings.</returns>
        static string RecognizeAndExportXml(Stream imageStream)
        {
            // Initialize the reader with the image stream
            using (var reader = new BarCodeReader(imageStream))
            {
                // Perform barcode detection
                reader.ReadBarCodes();

                // Export the reader's settings to an XML string
                using (var xmlStream = new MemoryStream())
                {
                    reader.ExportToXml(xmlStream);
                    xmlStream.Position = 0;
                    using (var readerStream = new StreamReader(xmlStream))
                    {
                        return readerStream.ReadToEnd();
                    }
                }
            }
        }
    }
}