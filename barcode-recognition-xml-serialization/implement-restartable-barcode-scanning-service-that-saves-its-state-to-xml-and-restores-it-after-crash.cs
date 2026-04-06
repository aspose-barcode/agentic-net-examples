using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class ServiceState
{
    public int LastProcessedIndex { get; set; }
    public static string FilePath => "state.xml";

    public void Save()
    {
        var doc = new XDocument(
            new XElement("ServiceState",
                new XElement("LastProcessedIndex", LastProcessedIndex)));
        doc.Save(FilePath);
    }

    public static ServiceState Load()
    {
        if (!File.Exists(FilePath))
            return new ServiceState { LastProcessedIndex = 0 };

        var doc = XDocument.Load(FilePath);
        var indexElement = doc.Root.Element("LastProcessedIndex");
        int index = 0;
        if (indexElement != null && int.TryParse(indexElement.Value, out int parsed))
            index = parsed;
        return new ServiceState { LastProcessedIndex = index };
    }
}

class Program
{
    static void Main()
    {
        // Prepare sample barcode images
        string[] files = { "code1.png", "code2.png", "code3.png" };
        string[] texts = { "ABC123", "DEF456", "GHI789" };

        for (int i = 0; i < files.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, texts[i]))
            {
                // Export generator settings to XML (demonstrating ExportToXml rule)
                generator.ExportToXml($"{Path.GetFileNameWithoutExtension(files[i])}_settings.xml");
                generator.Save(files[i]);
            }
        }

        // Load previous processing state
        ServiceState state = ServiceState.Load();

        // Process remaining barcodes
        for (int i = state.LastProcessedIndex; i < files.Length; i++)
        {
            using (var reader = new BarCodeReader(files[i], DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {files[i]}, Detected: {result.CodeText}");
                }
            }

            // Update and persist state after each successful read
            state.LastProcessedIndex = i + 1;
            state.Save();
        }

        Console.WriteLine("Scanning completed.");
    }
}