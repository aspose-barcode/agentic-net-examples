using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Recognizes barcodes from an image stream and returns the reader's XML state.
    static string RecognizeBarcodesToXml(Stream imageStream)
    {
        if (imageStream == null)
            throw new ArgumentException("Image stream cannot be null.", nameof(imageStream));

        using (var reader = new BarCodeReader())
        {
            // Detect all supported barcode types.
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Assign the image stream for recognition.
            reader.SetBarCodeImage(imageStream);

            // Perform recognition.
            reader.ReadBarCodes();

            // Export the reader's state to XML.
            using (var xmlStream = new MemoryStream())
            {
                if (!reader.ExportToXml(xmlStream))
                    throw new InvalidOperationException("Failed to export reader state to XML.");

                xmlStream.Position = 0;
                using (var sr = new StreamReader(xmlStream, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    static void Main()
    {
        // Generate a sample barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var imgStream = new MemoryStream())
            {
                generator.Save(imgStream, BarCodeImageFormat.Png);
                imgStream.Position = 0;

                // Recognize the barcode and obtain XML state.
                string xmlState = RecognizeBarcodesToXml(imgStream);

                // Output the XML.
                Console.WriteLine(xmlState);
            }
        }
    }
}