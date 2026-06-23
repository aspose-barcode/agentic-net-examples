using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, exporting reader settings to XML,
/// importing those settings, and recognizing the barcode from a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // 1. Generate a barcode and keep it in a memory stream.
        using (var imageStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the text "Sample123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning so it can be read.
            imageStream.Position = 0;

            // 2. Create a BarCodeReader, assign the image, and configure it.
            using (var reader = new BarCodeReader())
            {
                // Restrict decoding to Code128 type only.
                reader.SetBarCodeReadType(DecodeType.Code128);
                // Assign the barcode image stream to the reader.
                reader.SetBarCodeImage(imageStream);

                // Export current reader settings (including decode type) to XML in a memory stream.
                using (var xmlStream = new MemoryStream())
                {
                    reader.ExportToXml(xmlStream);
                    // Reset XML stream position for subsequent import.
                    xmlStream.Position = 0;

                    // 3. Import the reader state from the XML stream.
                    var importedReader = BarCodeReader.ImportFromXml(xmlStream);
                    if (importedReader == null)
                    {
                        Console.WriteLine("Failed to import reader state from XML.");
                        return;
                    }

                    // The image is not stored in the XML, so reassign it.
                    // Reset the image stream position before reuse.
                    imageStream.Position = 0;
                    importedReader.SetBarCodeImage(imageStream);

                    // 4. Perform barcode recognition using the imported reader.
                    foreach (var result in importedReader.ReadBarCodes())
                    {
                        // Output detected barcode type and text.
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}