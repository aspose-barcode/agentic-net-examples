// Title: BarCodeReader XML Serialization Wrapper Demo
// Description: Demonstrates how to export and import Aspose.BarCode reader settings to XML and reassign a barcode image from a different folder.
// Category-Description: This example belongs to the Aspose.BarCode .NET library collection that shows configuration persistence and image handling. It uses BarCodeReader, BarcodeGenerator, and related classes to illustrate exporting reader settings via ExportToXml, importing them with ImportFromXml, and reassigning images with SetBarCodeImage. Developers often need to store reader configurations, move images across directories, and reuse settings without recreating them.
// Prompt: Write a wrapper class that abstracts XML serialization of the reader and reassigns the image from a folder.
// Tags: barcode, xml-serialization, reader, aspose.barcode, code128, png, wrapper, import-export

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeXmlWrapperDemo
{
    /// <summary>
    /// Wrapper that abstracts XML serialization of <see cref="BarCodeReader"/> settings
    /// and allows reassigning the barcode image after deserialization.
    /// </summary>
    public class BarCodeReaderWrapper : IDisposable
    {
        /// <summary>
        /// Gets the underlying <see cref="BarCodeReader"/> instance.
        /// </summary>
        public BarCodeReader Reader { get; private set; }

        /// <summary>
        /// Initializes a new wrapper for the specified image file.
        /// </summary>
        /// <param name="imagePath">Full path to the barcode image.</param>
        public BarCodeReaderWrapper(string imagePath)
        {
            // Validate the image path before creating the reader.
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                throw new ArgumentException("Image file does not exist.", nameof(imagePath));

            Reader = new BarCodeReader(imagePath);
        }

        // Private constructor used internally when importing settings from XML.
        private BarCodeReaderWrapper(BarCodeReader reader)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        /// <summary>
        /// Exports the current reader configuration to an XML file.
        /// </summary>
        /// <param name="xmlPath">Destination path for the XML file.</param>
        public void ExportSettings(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("Invalid XML path.", nameof(xmlPath));

            // Ensure the target directory exists.
            string dir = Path.GetDirectoryName(xmlPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            Reader.ExportToXml(xmlPath);
        }

        /// <summary>
        /// Imports reader settings from an XML file and assigns a new image to process.
        /// </summary>
        /// <param name="xmlPath">Path to the XML settings file.</param>
        /// <param name="imagePath">Path to the barcode image to be assigned.</param>
        /// <returns>A new <see cref="BarCodeReaderWrapper"/> instance with imported settings.</returns>
        public static BarCodeReaderWrapper ImportSettings(string xmlPath, string imagePath)
        {
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException("XML settings file not found.", xmlPath);
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            // Import only the reader configuration; the image is not restored.
            BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath);
            // Assign the new image to the imported reader.
            importedReader.SetBarCodeImage(imagePath);
            return new BarCodeReaderWrapper(importedReader);
        }

        /// <summary>
        /// Reads all barcodes from the assigned image and writes the results to the console.
        /// </summary>
        public void ReadAndPrint()
        {
            foreach (BarCodeResult result in Reader.ReadBarCodes())
            {
                Console.WriteLine($"Symbology: {result.CodeTypeName}, CodeText: {result.CodeText}");
            }
        }

        /// <summary>
        /// Releases all resources used by the underlying <see cref="BarCodeReader"/>.
        /// </summary>
        public void Dispose()
        {
            Reader?.Dispose();
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates a barcode, exports reader settings,
        /// moves the image, imports settings, and reads the barcode from the new location.
        /// </summary>
        static void Main()
        {
            // Create a unique temporary working folder.
            string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(workFolder);

            // Define paths for the original image, moved image, and XML settings.
            string originalImagePath = Path.Combine(workFolder, "barcode.png");
            string movedFolder = Path.Combine(workFolder, "Moved");
            Directory.CreateDirectory(movedFolder);
            string movedImagePath = Path.Combine(movedFolder, "barcode.png");
            string xmlSettingsPath = Path.Combine(workFolder, "readerSettings.xml");

            // Generate a sample Code128 barcode image.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Default auto-sizing is sufficient for this example.
                generator.Save(originalImagePath, BarCodeImageFormat.Png);
            }

            // Wrap the reader around the generated image and export its settings to XML.
            using (var wrapper = new BarCodeReaderWrapper(originalImagePath))
            {
                wrapper.ExportSettings(xmlSettingsPath);
                Console.WriteLine("Exported reader settings to XML.");
            }

            // Simulate moving the image to another folder.
            File.Copy(originalImagePath, movedImagePath, overwrite: true);
            Console.WriteLine($"Image moved to: {movedImagePath}");

            // Import the previously saved settings and assign the moved image.
            using (var importedWrapper = BarCodeReaderWrapper.ImportSettings(xmlSettingsPath, movedImagePath))
            {
                Console.WriteLine("Imported settings and assigned new image. Detected barcodes:");
                importedWrapper.ReadAndPrint();
            }

            // Optional cleanup of the temporary working folder.
            try
            {
                Directory.Delete(workFolder, recursive: true);
            }
            catch
            {
                // Ignore any cleanup errors.
            }
        }
    }
}