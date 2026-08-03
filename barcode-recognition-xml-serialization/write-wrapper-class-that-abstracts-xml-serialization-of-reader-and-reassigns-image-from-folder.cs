// Title: XML Serialization Wrapper for BarCodeReader
// Description: Demonstrates how to load BarCodeReader settings from an XML file, reassign an image from a folder, and read barcodes.
// Category-Description: This example belongs to the Aspose.BarCode reading and configuration category, showcasing the use of BarCodeReader, BarcodeGenerator, and XML import/export APIs. Developers often need to persist reader settings, reuse them across sessions, and dynamically assign images for batch processing. The snippet serves as a searchable reference for implementing wrapper classes that handle serialization and image management.
// Prompt: Write a wrapper class that abstracts XML serialization of the reader and reassigns the image from a folder.
// Tags: barcode, xml-serialization, reader, image-assignment, aspnet, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Wrapper for Aspose.BarCode <see cref="BarCodeReader"/> that abstracts XML
/// serialization of the reader settings and reassigns the barcode image from a folder.
/// </summary>
class BarcodeReaderWrapper : IDisposable
{
    private BarCodeReader _reader;

    /// <summary>
    /// Loads reader configuration from an XML file exported previously by <see cref="BarCodeReader.ExportToXml"/>.
    /// </summary>
    /// <param name="xmlPath">Full path to the XML configuration file.</param>
    public void LoadFromXml(string xmlPath)
    {
        if (!File.Exists(xmlPath))
            throw new FileNotFoundException($"XML file not found: {xmlPath}");

        // Import reader settings from XML
        _reader = BarCodeReader.ImportFromXml(xmlPath);
    }

    /// <summary>
    /// Assigns the first image matching <paramref name="searchPattern"/> from <paramref name="folderPath"/>
    /// to the internal <see cref="BarCodeReader"/> instance.
    /// </summary>
    /// <param name="folderPath">Folder containing barcode images.</param>
    /// <param name="searchPattern">Search pattern for image files (default: "*.png").</param>
    public void SetImageFromFolder(string folderPath, string searchPattern = "*.png")
    {
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

        string[] files = Directory.GetFiles(folderPath, searchPattern);
        if (files.Length == 0)
            throw new FileNotFoundException($"No image files matching pattern '{searchPattern}' found in folder.");

        // Load the first image and assign it to the reader
        using (Bitmap bmp = new Bitmap(files[0]))
        {
            _reader.SetBarCodeImage(bmp);
        }
    }

    /// <summary>
    /// Reads barcodes from the assigned image, writing up to <paramref name="maxCount"/> results to the console.
    /// </summary>
    /// <param name="maxCount">Maximum number of barcode results to display (default: 5).</param>
    public void ReadBarcodes(int maxCount = 5)
    {
        if (_reader == null)
            throw new InvalidOperationException("BarCodeReader is not initialized. Call LoadFromXml first.");

        int count = 0;
        foreach (var result in _reader.ReadBarCodes())
        {
            Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            count++;
            if (count >= maxCount)
                break;
        }

        if (count == 0)
            Console.WriteLine("No barcodes detected.");
    }

    /// <summary>
    /// Disposes the underlying <see cref="BarCodeReader"/> instance.
    /// </summary>
    public void Dispose()
    {
        _reader?.Dispose();
    }
}

/// <summary>
/// Demonstrates generation of a barcode image, exporting reader settings to XML,
/// and using <see cref="BarcodeReaderWrapper"/> to reload settings and read the barcode.
/// </summary>
class Program
{
    static void Main()
    {
        // Prepare directory and file paths
        string baseDir = Directory.GetCurrentDirectory();
        string imageFolder = Path.Combine(baseDir, "Barcodes");
        string imagePath = Path.Combine(imageFolder, "sample.png");
        string xmlPath = Path.Combine(baseDir, "reader.xml");

        // Ensure the image folder exists
        if (!Directory.Exists(imageFolder))
            Directory.CreateDirectory(imageFolder);

        // 1. Generate a sample barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // 2. Create a BarCodeReader for the generated image and export its settings to XML
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.ExportToXml(xmlPath);
        }

        // 3. Use the wrapper to load settings from XML, reassign the image from the folder, and read barcodes
        using (var wrapper = new BarcodeReaderWrapper())
        {
            wrapper.LoadFromXml(xmlPath);
            wrapper.SetImageFromFolder(imageFolder, "*.png");
            wrapper.ReadBarcodes(3);
        }

        // Optional cleanup (commented out)
        // File.Delete(imagePath);
        // File.Delete(xmlPath);
        // Directory.Delete(imageFolder);
    }
}