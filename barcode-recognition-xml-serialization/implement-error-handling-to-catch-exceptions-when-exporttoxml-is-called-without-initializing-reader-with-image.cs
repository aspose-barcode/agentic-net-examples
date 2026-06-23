using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates usage of Aspose.BarCode's BarCodeReader and BarcodeGenerator,
/// including error handling when exporting XML without initializing the reader,
/// and proper export after setting a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Attempt to export XML without initializing the reader with an image.
        //    This should raise an exception because the reader has no source image.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            try
            {
                using (var xmlStream = new MemoryStream())
                {
                    // ExportToXml is expected to fail here.
                    reader.ExportToXml(xmlStream);
                    Console.WriteLine("Exported XML without image (unexpected).");
                }
            }
            catch (Exception ex)
            {
                // Expected error handling: display the exception message.
                Console.WriteLine($"Expected error when exporting without image: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // 2. Proper usage: generate a barcode, load it into the reader,
        //    then export the reader's data to XML.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode image to a memory stream in PNG format.
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                imageStream.Position = 0;

                using (var reader = new BarCodeReader())
                {
                    // Initialize the reader with the generated barcode image.
                    reader.SetBarCodeImage(imageStream);

                    using (var xmlStream = new MemoryStream())
                    {
                        // Export the reader's settings and detected barcodes to XML.
                        reader.ExportToXml(xmlStream);
                        // Reset stream position to read the XML content.
                        xmlStream.Position = 0;
                        string xml = new StreamReader(xmlStream).ReadToEnd();

                        // Output the exported XML to the console.
                        Console.WriteLine("Exported XML after initializing reader:");
                        Console.WriteLine(xml);
                    }
                }
            }
        }
    }
}