using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the barcode processing application.
/// Scans a directory for image files, reads any barcodes present,
/// and writes the results to XML files alongside the images.
/// </summary>
class Program
{
    /// <summary>
    /// Application start method.
    /// Determines the target directory (from arguments or default) and initiates processing.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify a directory path.</param>
    static void Main(string[] args)
    {
        // Determine the directory to process. Use a default sample if not provided.
        string directoryPath = args.Length > 0
            ? args[0]
            : Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory does not exist: {directoryPath}");
            return;
        }

        // Process all supported image files in the directory.
        ProcessBarcodesInDirectory(directoryPath);
    }

    /// <summary>
    /// Scans the specified directory for image files, reads any barcodes,
    /// and writes detection results to XML files.
    /// </summary>
    /// <param name="directoryPath">Path of the directory containing image files.</param>
    static void ProcessBarcodesInDirectory(string directoryPath)
    {
        // Supported image extensions (lower‑case for comparison).
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tiff", ".gif" };

        // Retrieve all files in the directory (filtering occurs later).
        var files = Directory.GetFiles(directoryPath);

        foreach (var filePath in files)
        {
            // Skip files that do not have a supported image extension.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

            // Use BarCodeReader to read all supported barcode types from the image.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Perform the barcode detection.
                var results = reader.ReadBarCodes();

                // Build an XML document containing the detection results.
                var doc = new XDocument(
                    new XElement("BarCodeResults",
                        new XElement("SourceFile", Path.GetFileName(filePath)),
                        new XElement("DetectedBarCodes",
                            // Create an element for each detected barcode.
                            from result in results
                            select new XElement("BarCode",
                                new XElement("CodeText", result.CodeText),
                                new XElement("Symbology", result.CodeTypeName),
                                new XElement("Confidence", (int)result.Confidence))
                        )
                    )
                );

                // Save the XML document alongside the image (same name with .xml extension).
                string xmlPath = Path.ChangeExtension(filePath, ".xml");
                doc.Save(xmlPath);
                Console.WriteLine($"Saved XML: {Path.GetFileName(xmlPath)}");

                // Log detection results to the console.
                if (results.Length == 0)
                {
                    Console.WriteLine("  No barcodes detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Detected: {result.CodeTypeName} - {result.CodeText} (Confidence: {result.Confidence})");
                    }
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}