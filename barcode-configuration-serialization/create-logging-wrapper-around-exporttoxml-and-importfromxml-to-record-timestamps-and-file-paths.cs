using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeLoggingDemo
{
    // Simple logger that writes messages with timestamps to a log file.
    static class Logger
    {
        private static readonly string LogFilePath = "barcode_log.txt";

        public static void Log(string message)
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}{Environment.NewLine}";
            File.AppendAllText(LogFilePath, entry);
        }
    }

    // Wrapper methods for ExportToXml and ImportFromXml that include logging.
    static class BarcodeXmlHelper
    {
        public static bool ExportToXmlWithLog(BarcodeGenerator generator, string xmlPath)
        {
            Logger.Log($"ExportToXml started. File: {xmlPath}");
            bool success = generator.ExportToXml(xmlPath);
            Logger.Log($"ExportToXml completed. Success: {success}. File: {xmlPath}");
            return success;
        }

        public static BarcodeGenerator ImportFromXmlWithLog(string xmlPath)
        {
            Logger.Log($"ImportFromXml started. File: {xmlPath}");
            BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath);
            bool success = generator != null;
            Logger.Log($"ImportFromXml completed. Success: {success}. File: {xmlPath}");
            return generator;
        }
    }

    class Program
    {
        static void Main()
        {
            // Paths for the barcode image, XML configuration, and the imported image.
            string imagePath = "original_barcode.png";
            string xmlPath = "barcode_settings.xml";
            string importedImagePath = "imported_barcode.png";

            // Create a barcode generator, configure it, and save the image.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "12345ABC";
                generator.Save(imagePath);
                Logger.Log($"Barcode image saved. File: {imagePath}");

                // Export generator settings to XML with logging.
                BarcodeXmlHelper.ExportToXmlWithLog(generator, xmlPath);
            }

            // Import generator settings from XML with logging.
            BarcodeGenerator importedGenerator = BarcodeXmlHelper.ImportFromXmlWithLog(xmlPath);
            if (importedGenerator != null)
            {
                using (importedGenerator)
                {
                    // Save a new barcode image using the imported settings.
                    importedGenerator.Save(importedImagePath);
                    Logger.Log($"Imported barcode image saved. File: {importedImagePath}");
                }
            }
            else
            {
                Logger.Log("Failed to import barcode settings from XML.");
            }

            Console.WriteLine("Processing completed. Check the log file for details.");
        }
    }
}