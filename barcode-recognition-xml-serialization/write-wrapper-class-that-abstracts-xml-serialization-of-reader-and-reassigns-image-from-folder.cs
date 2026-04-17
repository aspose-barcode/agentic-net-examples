using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace AsposeBarcodeWrapperDemo
{
    // Wrapper that handles XML serialization and image assignment for BarCodeReader
    public class BarcodeReaderXmlWrapper : IDisposable
    {
        private BarCodeReader _reader;

        // Loads BarCodeReader settings from an XML file
        public void LoadFromXml(string xmlPath)
        {
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException("XML file not found.", xmlPath);

            _reader = BarCodeReader.ImportFromXml(xmlPath);
            if (_reader == null)
                throw new InvalidOperationException("Failed to import BarCodeReader from XML.");
        }

        // Assigns the first image found in the specified folder to the reader
        public void SetImageFromFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            var imageFile = Directory.GetFiles(folderPath, "*.*")
                .FirstOrDefault(f =>
                    f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));

            if (imageFile == null)
                throw new FileNotFoundException("No image file found in the specified folder.", folderPath);

            _reader.SetBarCodeImage(imageFile);
        }

        // Reads barcodes using the configured reader
        public BarCodeResult[] ReadBarCodes()
        {
            if (_reader == null)
                throw new InvalidOperationException("BarCodeReader is not initialized.");

            return _reader.ReadBarCodes();
        }

        // Exports current reader settings to an XML file
        public bool ExportToXml(string xmlPath)
        {
            if (_reader == null)
                throw new InvalidOperationException("BarCodeReader is not initialized.");

            return _reader.ExportToXml(xmlPath);
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            // Prepare folders
            string inputFolder = Path.Combine(Environment.CurrentDirectory, "InputImages");
            string outputFolder = Path.Combine(Environment.CurrentDirectory, "Output");
            if (!Directory.Exists(inputFolder)) Directory.CreateDirectory(inputFolder);
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

            // Paths for sample image and XML
            string sampleImagePath = Path.Combine(inputFolder, "sample.png");
            string sampleXmlPath = Path.Combine(inputFolder, "sample.xml");

            // Generate a sample barcode image and export its settings to XML
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                generator.Save(sampleImagePath);
                generator.ExportToXml(sampleXmlPath);
            }

            // Use the wrapper to load settings, assign image, and read barcodes
            using (var wrapper = new BarcodeReaderXmlWrapper())
            {
                wrapper.LoadFromXml(sampleXmlPath);
                wrapper.SetImageFromFolder(inputFolder);

                var results = wrapper.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Barcode Text: {result.CodeText}");
                }

                // Optionally export the (potentially modified) settings back to XML
                string exportedXmlPath = Path.Combine(outputFolder, "exported.xml");
                wrapper.ExportToXml(exportedXmlPath);
            }
        }
    }
}