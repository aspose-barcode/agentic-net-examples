// Title: XML Serialization Wrapper for Aspose.BarCode Reader
// Description: Demonstrates exporting a BarCodeReader's state to XML and importing it later, reassigning the barcode image from a folder.
// Category-Description: This example belongs to the Aspose.BarCode serialization and image handling category. It showcases the use of BarCodeGenerator, BarCodeReader, and XML state persistence (ExportToXml, ImportFromXml). Typical scenarios include saving reader configuration for later reuse, batch processing, or decoupling barcode generation from recognition. Developers often need to serialize reader settings, store them, and later reload them with a different image source.
/// Prompt: Write a wrapper class that abstracts XML serialization of the reader and reassigns the image from a folder.
/// Tags: barcode, serialization, xml, reader, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AsposeBarCodeDemo
{
    /// <summary>
    /// Provides methods to export and import the state of a <see cref="BarCodeReader"/> using XML serialization.
    /// </summary>
    public class BarCodeReaderXmlWrapper
    {
        /// <summary>
        /// Exports the current state of the specified <see cref="BarCodeReader"/> to an XML file.
        /// </summary>
        /// <param name="reader">The barcode reader whose state will be saved.</param>
        /// <param name="xmlPath">The file path where the XML representation will be written.</param>
        public void ExportReaderState(BarCodeReader reader, string xmlPath)
        {
            // Serialize the reader's configuration and internal state to the given XML file.
            reader.ExportToXml(xmlPath);
        }

        /// <summary>
        /// Imports a <see cref="BarCodeReader"/> state from an XML file and assigns a new barcode image.
        /// </summary>
        /// <param name="xmlPath">The path to the XML file containing the saved reader state.</param>
        /// <param name="imagePath">The file path of the barcode image to associate with the imported reader.</param>
        /// <returns>A <see cref="BarCodeReader"/> instance initialized with the imported state and image.</returns>
        public BarCodeReader ImportReaderState(string xmlPath, string imagePath)
        {
            // Recreate the reader from the previously saved XML state.
            BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath);
            // Assign the barcode image that the reader should process.
            reader.SetBarCodeImage(imagePath);
            return reader;
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point demonstrating barcode generation, state export, import, and reading using the wrapper.
        /// </summary>
        static void Main()
        {
            // Create a unique temporary folder to store generated files.
            string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Define file paths for the barcode image and the XML state file.
            string barcodePath = Path.Combine(tempFolder, "sample.png");
            string xmlPath = Path.Combine(tempFolder, "readerState.xml");

            // Generate a simple Code128 barcode and save it as a PNG image.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // Initialize a reader for the generated image and export its state to XML.
            using (BarCodeReader reader = new BarCodeReader(barcodePath))
            {
                BarCodeReaderXmlWrapper wrapper = new BarCodeReaderXmlWrapper();
                wrapper.ExportReaderState(reader, xmlPath);
                Console.WriteLine($"Reader state exported to: {xmlPath}");
            }

            // Import the reader state from XML and reassign the same image.
            BarCodeReaderXmlWrapper importWrapper = new BarCodeReaderXmlWrapper();
            using (BarCodeReader importedReader = importWrapper.ImportReaderState(xmlPath, barcodePath))
            {
                // Read barcodes from the reassigned image.
                BarCodeResult[] results = importedReader.ReadBarCodes();
                Console.WriteLine($"Barcodes read after import: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }

            // Clean up temporary files and folder (optional).
            try
            {
                File.Delete(barcodePath);
                File.Delete(xmlPath);
                Directory.Delete(tempFolder);
            }
            catch
            {
                // Suppress any exceptions during cleanup to avoid breaking the demo flow.
            }
        }
    }
}