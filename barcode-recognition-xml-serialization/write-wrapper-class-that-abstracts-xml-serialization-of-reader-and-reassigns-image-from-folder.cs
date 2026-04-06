using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace AsposeBarcodeWrapperDemo
{
    // Wrapper that handles XML serialization of BarCodeReader settings and image assignment.
    public class BarcodeReaderWrapper : IDisposable
    {
        private BarCodeReader _reader;
        private bool _disposed;

        // Private constructor used internally.
        private BarcodeReaderWrapper(BarCodeReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        // Creates a new wrapper with a fresh BarCodeReader instance.
        public BarcodeReaderWrapper()
        {
            _reader = new BarCodeReader();
        }

        // Exports the current reader settings to an XML file.
        public void ExportSettings(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("XML path must be provided.", nameof(xmlPath));

            // Ensure the directory exists.
            string dir = Path.GetDirectoryName(xmlPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            _reader.ExportToXml(xmlPath);
        }

        // Imports reader settings from an XML file and returns a new wrapper instance.
        public static BarcodeReaderWrapper ImportFromXml(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("XML path must be provided.", nameof(xmlPath));
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException("XML file not found.", xmlPath);

            // ImportFromXml is a static method that returns a BarCodeReader with the imported settings.
            BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath);
            return new BarcodeReaderWrapper(importedReader);
        }

        // Assigns an image file from a folder to the reader.
        public void SetImageFromFolder(string folderPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder path must be provided.", nameof(folderPath));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name must be provided.", nameof(fileName));

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            string fullPath = Path.Combine(folderPath, fileName);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Image file not found.", fullPath);

            _reader.SetBarCodeImage(fullPath);
        }

        // Configures the reader to decode all supported types.
        public void EnableAllDecoding()
        {
            _reader.BarCodeReadType = DecodeType.AllSupportedTypes;
        }

        // Reads barcodes and writes basic information to the console.
        public void ReadAndPrint()
        {
            foreach (BarCodeResult result in _reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Region: {result.Region.Rectangle}");
                Console.WriteLine();
            }
        }

        // IDisposable implementation.
        public void Dispose()
        {
            if (!_disposed)
            {
                _reader?.Dispose();
                _disposed = true;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // Define paths.
            string baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            string imageFileName = "sample.png";
            string imagePath = Path.Combine(baseFolder, imageFileName);
            string xmlPath = Path.Combine(baseFolder, "readerSettings.xml");

            // Ensure the folder exists.
            if (!Directory.Exists(baseFolder))
                Directory.CreateDirectory(baseFolder);

            // -------------------------------------------------
            // Step 1: Generate a sample barcode image.
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
            {
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image saved to: {imagePath}");
            }

            // -------------------------------------------------
            // Step 2: Create a reader, assign image, read, and export settings.
            // -------------------------------------------------
            using (var wrapper = new BarcodeReaderWrapper())
            {
                wrapper.SetImageFromFolder(baseFolder, imageFileName);
                wrapper.EnableAllDecoding();

                Console.WriteLine("Reading barcodes with initial reader:");
                wrapper.ReadAndPrint();

                wrapper.ExportSettings(xmlPath);
                Console.WriteLine($"Reader settings exported to XML: {xmlPath}");
            }

            // -------------------------------------------------
            // Step 3: Import settings from XML, reassign image, and read again.
            // -------------------------------------------------
            using (var importedWrapper = BarcodeReaderWrapper.ImportFromXml(xmlPath))
            {
                // Reassign the same image (simulating a different folder or later run).
                importedWrapper.SetImageFromFolder(baseFolder, imageFileName);
                importedWrapper.EnableAllDecoding();

                Console.WriteLine("Reading barcodes with imported settings:");
                importedWrapper.ReadAndPrint();
            }
        }
    }
}