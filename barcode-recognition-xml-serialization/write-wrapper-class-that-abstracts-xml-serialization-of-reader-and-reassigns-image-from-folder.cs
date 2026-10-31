// Title: XML Serialization Wrapper for Aspose.BarCode Reader
// Description: Demonstrates how to serialize a BarCodeReader to XML, then deserialize it and reassign a different image source.
// Category-Description: This example belongs to the Aspose.BarCode serialization and image handling category. It showcases the use of BarCodeReader.ExportToXml, BarCodeReader.ImportFromXml, and SetBarCodeImage to persist reader settings and switch image sources. Developers working with barcode scanning automation often need to store reader configurations, transfer them between environments, or change the source image without recreating the reader instance.
// Prompt: Write a wrapper class that abstracts XML serialization of the reader and reassigns the image from a folder.
// Tags: barcode, xml-serialization, reader, aspose.barcode, code128, image-handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Wrapper that abstracts XML serialization of a <see cref="BarCodeReader"/> and allows reassigning the image source.
/// </summary>
class BarcodeReaderXmlWrapper
{
    /// <summary>
    /// Serializes the state of a <see cref="BarCodeReader"/> to an XML file.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image to be read.</param>
    /// <param name="decodeType">The barcode symbology to decode.</param>
    /// <param name="xmlPath">Destination path for the generated XML file.</param>
    public void SerializeReader(string imagePath, BaseDecodeType decodeType, string xmlPath)
    {
        // Verify that the source image exists before attempting to read it.
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image file not found: {imagePath}");

        // Create a reader for the specified image and symbology.
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Example setting – can be customized per requirements.
            reader.BarcodeSettings.StripFNC = true;

            // Export the reader's configuration and state to an XML file.
            reader.ExportToXml(xmlPath);
        }
    }

    /// <summary>
    /// Deserializes a <see cref="BarCodeReader"/> from an XML file and assigns a new image source.
    /// </summary>
    /// <param name="xmlPath">Path to the XML file containing the serialized reader state.</param>
    /// <param name="newImagePath">Path to the new barcode image to be associated with the reader.</param>
    /// <returns>A <see cref="BarCodeReader"/> instance configured with the imported settings and new image.</returns>
    public BarCodeReader DeserializeReader(string xmlPath, string newImagePath)
    {
        // Ensure the XML file and the new image both exist.
        if (!File.Exists(xmlPath))
            throw new FileNotFoundException($"XML file not found: {xmlPath}");
        if (!File.Exists(newImagePath))
            throw new FileNotFoundException($"Image file not found: {newImagePath}");

        // Import the reader configuration from XML.
        var reader = BarCodeReader.ImportFromXml(xmlPath);

        // Assign the new image to the imported reader.
        reader.SetBarCodeImage(newImagePath);
        return reader; // Caller is responsible for disposing the reader.
    }
}

class Program
{
    /// <summary>
    /// Entry point that demonstrates generating a barcode, serializing the reader, and deserializing with a different image.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder to hold generated files.
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original image, XML state, and a copy of the image.
        string barcodeImagePath = Path.Combine(workFolder, "sample.png");
        string xmlPath = Path.Combine(workFolder, "readerState.xml");
        string newImagePath = Path.Combine(workFolder, "sample_copy.png");

        // Generate a sample Code128 barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // Copy the image to simulate a different source location.
        File.Copy(barcodeImagePath, newImagePath, true);

        // Instantiate the wrapper that handles XML serialization.
        var wrapper = new BarcodeReaderXmlWrapper();

        // Serialize the reader state using the original image.
        wrapper.SerializeReader(barcodeImagePath, DecodeType.Code128, xmlPath);

        // Deserialize the reader and assign the copied image.
        using (var reader = wrapper.DeserializeReader(xmlPath, newImagePath))
        {
            // Read barcodes from the newly assigned image.
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Optional cleanup of the temporary folder.
        try { Directory.Delete(workFolder, true); } catch { }
    }
}