using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for temporary files
        string imagePath = "sample.png";
        string xmlPath = "readerState.xml";

        // 1. Generate a barcode image and export its reader settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator.Save(imagePath);
        }

        // Export default reader settings to XML (for demonstration)
        using (var readerForExport = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Export the current settings to an XML file
            readerForExport.ExportToXml(xmlPath);
        }

        // 2. Import the saved XML state into a new BarCodeReader instance
        BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath);
        if (reader == null)
        {
            Console.WriteLine("Failed to import reader settings from XML.");
            return;
        }

        // 3. Set the image for recognition
        using (var bmp = new Bitmap(imagePath))
        {
            reader.SetBarCodeImage(bmp);

            // 4. Perform barcode recognition
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                // If it's a 1D barcode, also show value and checksum
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"  Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"  Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // Clean up
        reader.Dispose();

        // Optionally delete temporary files
        try
        {
            File.Delete(imagePath);
            File.Delete(xmlPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}