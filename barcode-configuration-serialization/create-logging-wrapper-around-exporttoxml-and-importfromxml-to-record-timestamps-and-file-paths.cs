// Title: Logging wrapper for barcode XML export/import
// Description: Demonstrates how to log timestamps and file paths when exporting barcode settings to XML and importing them back, using Aspose.BarCode.
// Prompt: Create a logging wrapper around ExportToXml and ImportFromXml to record timestamps and file paths.
// Tags: barcode symbology, export, import, xml, logging, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeXmlLoggingDemo
{
    /// <summary>
    /// Provides static methods that wrap Aspose.BarCode XML export/import operations with console logging.
    /// </summary>
    public static class BarcodeXmlLogger
    {
        /// <summary>
        /// Exports the settings of a <see cref="BarcodeGenerator"/> to an XML file and logs the operation.
        /// </summary>
        /// <param name="generator">The barcode generator whose settings are to be exported.</param>
        /// <param name="xmlFilePath">The full path of the XML file to write.</param>
        /// <returns>True if the export succeeded; otherwise false.</returns>
        public static bool ExportToXmlWithLog(BarcodeGenerator generator, string xmlFilePath)
        {
            // Validate input arguments
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (string.IsNullOrWhiteSpace(xmlFilePath)) throw new ArgumentException("XML file path must be provided.", nameof(xmlFilePath));

            // Log start of export
            Console.WriteLine($"[{DateTime.Now:O}] Exporting barcode settings to XML: {xmlFilePath}");
            bool result = false;
            try
            {
                // Perform the actual export
                result = generator.ExportToXml(xmlFilePath);
                // Log successful completion
                Console.WriteLine($"[{DateTime.Now:O}] Export completed. Success: {result}");
            }
            catch (Exception ex)
            {
                // Log any exception and rethrow
                Console.WriteLine($"[{DateTime.Now:O}] Export failed: {ex.Message}");
                throw;
            }
            return result;
        }

        /// <summary>
        /// Imports barcode settings from an XML file, creates a <see cref="BarcodeGenerator"/>, and logs the operation.
        /// </summary>
        /// <param name="xmlFilePath">The full path of the XML file to read.</param>
        /// <returns>A new <see cref="BarcodeGenerator"/> initialized with the imported settings.</returns>
        public static BarcodeGenerator ImportFromXmlWithLog(string xmlFilePath)
        {
            // Validate input arguments
            if (string.IsNullOrWhiteSpace(xmlFilePath)) throw new ArgumentException("XML file path must be provided.", nameof(xmlFilePath));
            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException("XML file not found.", xmlFilePath);

            // Log start of import
            Console.WriteLine($"[{DateTime.Now:O}] Importing barcode settings from XML: {xmlFilePath}");
            BarcodeGenerator generator = null;
            try
            {
                // Perform the actual import
                generator = BarcodeGenerator.ImportFromXml(xmlFilePath);
                // Log successful creation with symbology info
                Console.WriteLine($"[{DateTime.Now:O}] Import completed. Generator created for symbology: {generator.BarcodeType.TypeName}");
            }
            catch (Exception ex)
            {
                // Log any exception and rethrow
                Console.WriteLine($"[{DateTime.Now:O}] Import failed: {ex.Message}");
                throw;
            }
            return generator;
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo. Exports barcode settings to XML, imports them back, generates an image, and cleans up temporary files.
        /// </summary>
        static void Main()
        {
            // Define temporary file paths for XML settings and barcode image
            string tempDir = Path.GetTempPath();
            string xmlPath = Path.Combine(tempDir, "barcode_settings.xml");
            string imagePath = Path.Combine(tempDir, "barcode_image.png");

            // Create a barcode generator, export its settings, then import them back
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export settings to XML with logging
                BarcodeXmlLogger.ExportToXmlWithLog(generator, xmlPath);
            }

            // Import settings from XML with logging and generate the barcode image
            using (var importedGenerator = BarcodeXmlLogger.ImportFromXmlWithLog(xmlPath))
            {
                // Save the generated barcode image to verify the imported settings
                importedGenerator.Save(imagePath);
                Console.WriteLine($"[{DateTime.Now:O}] Barcode image saved to: {imagePath}");
            }

            // Optional cleanup of temporary files
            try
            {
                if (File.Exists(xmlPath)) File.Delete(xmlPath);
                if (File.Exists(imagePath)) File.Delete(imagePath);
                Console.WriteLine($"[{DateTime.Now:O}] Temporary files cleaned up.");
            }
            catch (Exception cleanupEx)
            {
                Console.WriteLine($"[{DateTime.Now:O}] Cleanup error: {cleanupEx.Message}");
            }
        }
    }
}