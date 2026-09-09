// Title: XML Serialization Wrapper for Aspose.BarCode Reader
// Description: Demonstrates exporting a BarCodeReader's configuration to XML and reloading it with a new image file.
// Category-Description: This example belongs to the Aspose.BarCode serialization and decoding category. It showcases the use of BarCodeReader.ExportToXml, BarCodeReader.ImportFromXml, and image reassignment to persist and restore reader settings. Developers working with barcode generation, recognition, and configuration persistence commonly need such patterns to store reader state, share settings across applications, or automate batch processing.
// Prompt: Write a wrapper class that abstracts XML serialization of the reader and reassigns the image from a folder.
// Tags: barcode, xml, serialization, reader, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Wrapper class that abstracts XML serialization of a <see cref="BarCodeReader"/> and
/// allows reassigning the barcode image from a specified folder.
/// </summary>
class BarcodeReaderXmlWrapper
{
    /// <summary>
    /// Exports the current state of the provided <see cref="BarCodeReader"/> to an XML file.
    /// </summary>
    /// <param name="reader">The barcode reader whose settings are to be saved.</param>
    /// <param name="xmlPath">The file path where the XML representation will be written.</param>
    public static void ExportReaderState(BarCodeReader reader, string xmlPath)
    {
        // Validate arguments
        if (reader == null) throw new ArgumentNullException(nameof(reader));
        if (string.IsNullOrEmpty(xmlPath)) throw new ArgumentException("XML path is null or empty.", nameof(xmlPath));

        // Perform the export
        reader.ExportToXml(xmlPath);
    }

    /// <summary>
    /// Imports a <see cref="BarCodeReader"/> state from an XML file and assigns a new image.
    /// </summary>
    /// <param name="xmlPath">Path to the XML file containing the saved reader state.</param>
    /// <param name="imagePath">Path to the barcode image that the reader should process.</param>
    /// <param name="decodeType">The decode type that matches the barcode symbology.</param>
    /// <returns>A configured <see cref="BarCodeReader"/> ready for decoding.</returns>
    public static BarCodeReader ImportReaderState(string xmlPath, string imagePath, BaseDecodeType decodeType)
    {
        // Ensure the required files exist
        if (!File.Exists(xmlPath)) throw new FileNotFoundException("XML file not found.", xmlPath);
        if (!File.Exists(imagePath)) throw new FileNotFoundException("Image file not found.", imagePath);
        if (decodeType == null) throw new ArgumentNullException(nameof(decodeType));

        // Restore the reader from XML and set the new image
        BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath);
        reader.SetBarCodeImage(imagePath);
        // The decode type is stored in the imported settings; no further action required.
        return reader;
    }
}

class Program
{
    /// <summary>
    /// Entry point that demonstrates generating a barcode, exporting reader settings to XML,
    /// importing them back, and decoding the barcode image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated image and the XML state file
        string imagePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "readerState.xml");

        // Generate a sample PDF417 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Initialize BarCodeReader, adjust a setting, and export its state to XML
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            reader.BarcodeSettings.StripFNC = true;
            BarcodeReaderXmlWrapper.ExportReaderState(reader, xmlPath);
        }

        // Import the reader state from XML, reassign the image, and read barcodes
        using (var importedReader = BarcodeReaderXmlWrapper.ImportReaderState(xmlPath, imagePath, DecodeType.Pdf417))
        {
            BarCodeResult[] results = importedReader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            if (File.Exists(imagePath)) File.Delete(imagePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program exit
        }
    }
}