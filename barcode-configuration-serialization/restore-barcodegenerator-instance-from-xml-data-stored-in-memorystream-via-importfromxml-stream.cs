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
        // Create a barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set a custom bar color
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Export the generator settings to an XML stream
            using (var xmlStream = new MemoryStream())
            {
                bool exportSuccess = generator.ExportToXml(xmlStream);
                if (!exportSuccess)
                {
                    Console.WriteLine("Failed to export barcode settings to XML.");
                    return;
                }

                // Reset stream position before reading
                xmlStream.Position = 0;

                // Import a new generator instance from the XML stream
                BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream);
                if (importedGenerator == null)
                {
                    Console.WriteLine("Failed to import barcode settings from XML.");
                    return;
                }

                // Generate the barcode image from the imported generator
                using (Bitmap barcodeImage = importedGenerator.GenerateBarCodeImage())
                {
                    // Save the image to a file
                    barcodeImage.Save("imported_barcode.png", ImageFormat.Png);
                    Console.WriteLine("Barcode image saved as 'imported_barcode.png'.");
                }

                // Dispose the imported generator (it implements IDisposable via Component)
                importedGenerator.Dispose();
            }
        }
    }
}