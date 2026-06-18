using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output directories with fallback defaults
        string inputDir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        string outputDir = args.Length > 1 ? args[1] : Path.Combine(inputDir, "Barcodes");

        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                XDocument doc = XDocument.Load(xmlPath);
                XElement root = doc.Root;
                if (root == null)
                {
                    Console.WriteLine($"Invalid XML structure in file: {xmlPath}");
                    continue;
                }

                string symbologyName = root.Element("Symbology")?.Value?.Trim();
                string codeText = root.Element("CodeText")?.Value?.Trim();

                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Missing Symbology or CodeText in file: {xmlPath}");
                    continue;
                }

                // Resolve symbology name to BaseEncodeType via reflection
                var field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in file: {xmlPath}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                string outputPath = Path.Combine(outputDir, outputFileName);

                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Example: set a modest resolution
                    generator.Parameters.Resolution = 300f;
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}