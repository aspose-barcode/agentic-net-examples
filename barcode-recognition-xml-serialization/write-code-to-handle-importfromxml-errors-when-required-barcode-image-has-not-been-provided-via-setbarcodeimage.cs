using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string imagePath = "tempBarcode.png";
        string xmlPath = "tempBarcodeSettings.xml";

        // Generate a barcode and export its settings to XML
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(imagePath);
            generator.ExportToXml(xmlPath);
        }

        // Import settings and attempt to read without providing an image
        try
        {
            using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during barcode reading: " + ex.Message);
        }

        // Clean up temporary files
        try { if (File.Exists(imagePath)) File.Delete(imagePath); } catch { }
        try { if (File.Exists(xmlPath)) File.Delete(xmlPath); } catch { }
    }
}