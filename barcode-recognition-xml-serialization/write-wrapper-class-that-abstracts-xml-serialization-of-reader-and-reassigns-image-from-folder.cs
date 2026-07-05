// Title: XML Serialization Wrapper for Aspose BarCode Reader
// Description: Demonstrates a wrapper that loads BarCodeReader settings from XML and assigns an image from a folder for barcode detection.
// Prompt: Write a wrapper class that abstracts XML serialization of the reader and reassigns the image from a folder.
// Tags: barcode, xml-serialization, reader, wrapper, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace AsposeBarcodeWrapperDemo
{
    /// <summary>
    /// Wrapper class that abstracts XML serialization of <see cref="BarCodeReader"/> and handles image assignment.
    /// </summary>
    public class BarcodeReaderWrapper : IDisposable
    {
        // Underlying Aspose BarCodeReader instance.
        private readonly BarCodeReader _reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeReaderWrapper"/> class.
        /// </summary>
        public BarcodeReaderWrapper()
        {
            // Create a fresh BarCodeReader.
            _reader = new BarCodeReader();
        }

        /// <summary>
        /// Loads reader configuration from an XML file.
        /// </summary>
        /// <param name="xmlPath">Full path to the XML settings file.</param>
        public void LoadFromXml(string xmlPath)
        {
            // Verify that the XML file exists before attempting to load.
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException($"XML file not found: {xmlPath}");

            // Apply the XML settings to the current reader instance.
            BarCodeReader.ImportFromXml(xmlPath);
        }

        /// <summary>
        /// Assigns an image file to the reader for barcode detection.
        /// </summary>
        /// <param name="imagePath">Full path to the barcode image file.</param>
        public void SetImage(string imagePath)
        {
            // Ensure the image file exists.
            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image file not found: {imagePath}");

            // Set the image for the reader.
            _reader.SetBarCodeImage(imagePath);
        }

        /// <summary>
        /// Reads barcodes from the assigned image and writes their type and text to the console.
        /// </summary>
        /// <param name="maxCount">Maximum number of barcodes to process. Defaults to <see cref="int.MaxValue"/>.</param>
        public void ReadBarcodes(int maxCount = int.MaxValue)
        {
            // Retrieve all barcode results from the image.
            var results = _reader.ReadBarCodes();
            int count = 0;

            // Iterate through results, respecting the maxCount limit.
            foreach (var result in results)
            {
                if (count >= maxCount)
                    break;

                Console.WriteLine($"Detected Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                count++;
            }
        }

        /// <summary>
        /// Releases resources used by the underlying <see cref="BarCodeReader"/>.
        /// </summary>
        public void Dispose()
        {
            _reader?.Dispose();
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo application. Generates a barcode, exports its settings to XML,
        /// then uses <see cref="BarcodeReaderWrapper"/> to load the settings, assign the image,
        /// and read detected barcodes.
        /// </summary>
        static void Main()
        {
            // Define the folder that will contain the generated barcode image and XML settings.
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            string imagePath = Path.Combine(folderPath, "sample.png");
            string xmlPath   = Path.Combine(folderPath, "sampleSettings.xml");

            // Ensure the target folder exists.
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Step 1: Generate a sample barcode image and export its configuration to XML.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the barcode image to the file system.
                generator.Save(imagePath);

                // Export the generator's settings to an XML file.
                generator.ExportToXml(xmlPath);
            }

            // Step 2: Use the wrapper to load settings, assign the image, and read barcodes.
            using (var wrapper = new BarcodeReaderWrapper())
            {
                // Load reader configuration from the previously exported XML.
                wrapper.LoadFromXml(xmlPath);

                // Assign the generated barcode image to the reader.
                wrapper.SetImage(imagePath);

                // Read and display up to three detected barcodes.
                wrapper.ReadBarcodes(maxCount: 3);
            }

            // Indicate that processing has finished.
            Console.WriteLine("Processing completed.");
        }
    }
}