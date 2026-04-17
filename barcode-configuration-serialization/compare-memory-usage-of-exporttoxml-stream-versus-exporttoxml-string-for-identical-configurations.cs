using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator with a sample configuration
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            // Measure memory usage for ExportToXml(string)
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long beforeString = GC.GetTotalMemory(true);
            string xmlFilePath = Path.Combine(Path.GetTempPath(), "barcode_config_string.xml");
            bool successString = generator.ExportToXml(xmlFilePath);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long afterString = GC.GetTotalMemory(true);
            long diffString = afterString - beforeString;

            // Measure memory usage for ExportToXml(Stream)
            using (MemoryStream ms = new MemoryStream())
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                long beforeStream = GC.GetTotalMemory(true);
                bool successStream = generator.ExportToXml(ms);
                // Reset position if the stream will be used later
                ms.Position = 0;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                long afterStream = GC.GetTotalMemory(true);
                long diffStream = afterStream - beforeStream;

                Console.WriteLine($"ExportToXml(string) succeeded: {successString}, memory delta: {diffString} bytes");
                Console.WriteLine($"ExportToXml(Stream) succeeded: {successStream}, memory delta: {diffStream} bytes");
            }

            // Clean up temporary XML file
            if (File.Exists(xmlFilePath))
            {
                try { File.Delete(xmlFilePath); } catch { }
            }
        }
    }
}