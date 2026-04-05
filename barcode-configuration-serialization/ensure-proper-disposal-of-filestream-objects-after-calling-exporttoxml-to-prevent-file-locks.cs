using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Export barcode properties to XML using a FileStream
            string xmlFilePath = "barcode_properties.xml";
            using (var fileStream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bool exported = generator.ExportToXml(fileStream);
                Console.WriteLine($"Export to XML {(exported ? "succeeded" : "failed")}.");
            } // FileStream disposed here, releasing the file lock

            // Save the barcode image to verify generation
            generator.Save("barcode.png");
        } // BarcodeGenerator disposed here

        // Verify that the XML file can be accessed after disposal
        using (var readStream = new FileStream("barcode_properties.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            Console.WriteLine($"XML file size: {readStream.Length} bytes.");
        }
    }
}