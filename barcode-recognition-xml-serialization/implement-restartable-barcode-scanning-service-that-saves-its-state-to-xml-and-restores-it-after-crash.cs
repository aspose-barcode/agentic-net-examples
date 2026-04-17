using System;
using System.IO;
using System.Xml;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    // Path for the XML file that stores the scanning progress
    private const string StateFile = "scanstate.xml";

    // Directory to store generated barcode images
    private const string ImagesDir = "Barcodes";

    static void Main()
    {
        // Ensure the images directory exists
        if (!Directory.Exists(ImagesDir))
        {
            Directory.CreateDirectory(ImagesDir);
        }

        // Sample barcode texts to generate and scan
        string[] barcodeTexts = new[]
        {
            "123456789012",   // EAN13
            "9876543210",     // Code128
            "ABCD1234",       // QR
            "5555555555",     // Code39
            "20230615"        // DataMatrix
        };

        // Generate barcode images if they are missing
        for (int i = 0; i < barcodeTexts.Length; i++)
        {
            string filePath = GetImagePath(i);
            if (!File.Exists(filePath))
            {
                GenerateBarcodeImage(barcodeTexts[i], filePath);
            }
        }

        // Load the next index to process from the XML state file (if it exists)
        int nextIndex = LoadState();

        // Process remaining barcodes
        for (int i = nextIndex; i < barcodeTexts.Length; i++)
        {
            string imagePath = GetImagePath(i);
            Console.WriteLine($"Scanning image: {Path.GetFileName(imagePath)}");

            // Read the barcode
            using (var reader = new BarCodeReader(imagePath))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  Detected Type : {result.CodeTypeName}");
                    Console.WriteLine($"  Detected Text : {result.CodeText}");
                }
            }

            // Update state after successful processing
            SaveState(i + 1);
        }

        // All items processed – clean up the state file
        if (File.Exists(StateFile))
        {
            File.Delete(StateFile);
        }

        Console.WriteLine("Scanning completed.");
    }

    // Returns the full path for a generated barcode image based on its index
    private static string GetImagePath(int index)
    {
        return Path.Combine(ImagesDir, $"barcode_{index}.png");
    }

    // Generates a barcode image using Aspose.BarCode and saves it to the specified path
    private static void GenerateBarcodeImage(string codeText, string filePath)
    {
        // Choose a symbology based on the length/content of the code text
        BaseEncodeType encodeType = codeText.Length == 12 ? EncodeTypes.EAN13 :
                                   codeText.All(char.IsDigit) ? EncodeTypes.Code128 :
                                   EncodeTypes.QR;

        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example of setting a property (image width) using unit members
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Export generator settings to XML (optional demonstration)
            string genXml = Path.ChangeExtension(filePath, ".xml");
            generator.ExportToXml(genXml);

            // Save the barcode image
            generator.Save(filePath);
        }
    }

    // Loads the next index to process from the XML state file; returns 0 if the file is missing or malformed
    private static int LoadState()
    {
        if (!File.Exists(StateFile))
            return 0;

        try
        {
            var doc = new XmlDocument();
            doc.Load(StateFile);
            var node = doc.SelectSingleNode("/State/NextIndex");
            if (node != null && int.TryParse(node.InnerText, out int index))
                return index;
        }
        catch
        {
            // If any error occurs, start from the beginning
        }
        return 0;
    }

    // Saves the next index to process into the XML state file
    private static void SaveState(int nextIndex)
    {
        var doc = new XmlDocument();
        var decl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(decl);

        var root = doc.CreateElement("State");
        doc.AppendChild(root);

        var indexNode = doc.CreateElement("NextIndex");
        indexNode.InnerText = nextIndex.ToString();
        root.AppendChild(indexNode);

        doc.Save(StateFile);
    }
}