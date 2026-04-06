using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define file paths
        string imagePath = "barcode.png";
        string xmlPath = "barcode_state.xml";
        string logPath = "log.txt";

        // Create a barcode generator, set the code text and save the image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Save the barcode image
            generator.Save(imagePath);

            // Export the generator's state to XML
            generator.ExportToXml(xmlPath);
        }

        // Log the file paths and timestamp
        using (var logWriter = new StreamWriter(logPath, true))
        {
            string logEntry = $"Image saved to: {Path.GetFullPath(imagePath)}; XML exported to: {Path.GetFullPath(xmlPath)}; Timestamp: {DateTime.Now}";
            Console.WriteLine(logEntry);
            logWriter.WriteLine(logEntry);
        }
    }
}