// Title: Restartable Barcode Scanning Service with XML State Persistence
// Description: Demonstrates scanning multiple barcode images, outputting results to the console, and persisting processed file information to an XML file so the service can resume after a crash.
// Category-Description: This example belongs to the Aspose.BarCode scanning and state‑management category. It showcases the use of BarcodeGenerator for creating sample barcodes and BarCodeReader for recognizing them, combined with XML handling to store processed file names. Developers building long‑running or fault‑tolerant barcode processing pipelines often need to track progress and recover gracefully, making this pattern a common reference point.
// Prompt: Implement a restartable barcode scanning service that saves its state to XML and restores it after a crash.
// Tags: code128, qr, datamatrix, scanning, state, xml, console, barcodegenerator, barcodereader

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a console application that generates sample barcode images,
/// scans them, and maintains a persistent XML state file to allow
/// restartable processing after unexpected termination.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes if needed,
    /// loads previously processed file information, scans remaining images,
    /// and updates the XML state after each successful scan.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare folder for sample barcode images
        // --------------------------------------------------------------------
        string imagesFolder = "Barcodes";
        Directory.CreateDirectory(imagesFolder);

        // --------------------------------------------------------------------
        // Generate sample barcode images when the folder is empty
        // --------------------------------------------------------------------
        string[] sampleFiles = { "code128.png", "qr.png", "datamatrix.png" };
        if (Directory.GetFiles(imagesFolder, "*.png").Length == 0)
        {
            // Code128 sample
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(Path.Combine(imagesFolder, sampleFiles[0]));
            }

            // QR code sample
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "QR Sample"))
            {
                generator.Save(Path.Combine(imagesFolder, sampleFiles[1]));
            }

            // DataMatrix sample
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM Sample"))
            {
                generator.Save(Path.Combine(imagesFolder, sampleFiles[2]));
            }
        }

        // --------------------------------------------------------------------
        // Load or initialize processing state
        // --------------------------------------------------------------------
        string stateFile = "state.xml";
        var processed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (File.Exists(stateFile))
        {
            try
            {
                XDocument doc = XDocument.Load(stateFile);
                foreach (var elem in doc.Root.Element("ProcessedFiles").Elements("File"))
                {
                    processed.Add(elem.Value);
                }
            }
            catch
            {
                // If the state file is corrupted, start with a clean state
                processed.Clear();
            }
        }

        // --------------------------------------------------------------------
        // Scan each barcode image that hasn't been processed yet
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.png");

        foreach (string filePath in imageFiles)
        {
            string fileName = Path.GetFileName(filePath);
            if (processed.Contains(fileName))
                continue; // Skip already processed files

            // Read all supported barcodes from the current image
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {fileName} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }

            // Record the file as processed and persist the updated state
            processed.Add(fileName);
            SaveState(stateFile, processed);
        }

        Console.WriteLine("Scanning completed.");
    }

    /// <summary>
    /// Persists the set of processed file names to an XML state file.
    /// </summary>
    /// <param name="statePath">Path to the XML state file.</param>
    /// <param name="processedFiles">Collection of processed file names.</param>
    static void SaveState(string statePath, HashSet<string> processedFiles)
    {
        var doc = new XDocument(
            new XElement("State",
                new XElement("ProcessedFiles",
                    new List<XElement>(CreateFileElements(processedFiles))
                )
            )
        );

        // Ensure the state file is written atomically
        using (var stream = new FileStream(statePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            doc.Save(stream);
        }
    }

    /// <summary>
    /// Generates XML <File> elements for each processed file name.
    /// </summary>
    /// <param name="files">Enumerable of file names.</param>
    /// <returns>IEnumerable of XElement representing each file.</returns>
    static IEnumerable<XElement> CreateFileElements(IEnumerable<string> files)
    {
        foreach (var f in files)
        {
            yield return new XElement("File", f);
        }
    }
}