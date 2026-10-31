// Title: Deserialize BarCodeReader state from XML and reuse barcode image
// Description: Demonstrates exporting a BarCodeReader's configuration to an XML stream, then importing it and applying the same barcode image for decoding.
// Category-Description: This example belongs to the Aspose.BarCode reading and configuration management category. It shows how to use BarCodeReader, BarcodeSettings, QualitySettings, ExportToXml, and ImportFromXml to persist reader state. Typical use cases include saving reader configurations, sharing settings across services, and reusing them without reconfiguring each time. Developers often need to serialize reader settings for diagnostics, caching, or distributed processing.
/// Prompt: Deserialize the reader state from an XML stream, then reapply the same barcode image for analysis.
/// Tags: barcode, qr, serialization, xml, reader, import, export, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, exports the reader's state to XML,
/// imports the state back, reassigns the same image, and decodes the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, state serialization,
    /// state import, and barcode decoding.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample QR barcode and keep its image bytes
        // ------------------------------------------------------------
        byte[] barcodeImageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into a memory stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Capture the raw image bytes for later reuse
                barcodeImageBytes = imageStream.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Create a BarCodeReader, configure its settings, and export state to XML
        // ------------------------------------------------------------
        MemoryStream xmlStateStream = new MemoryStream();
        using (var initialReader = new BarCodeReader(new MemoryStream(barcodeImageBytes), DecodeType.QR))
        {
            // Example configuration adjustments
            initialReader.BarcodeSettings.StripFNC = true;
            initialReader.QualitySettings.XDimension = XDimensionMode.Small;

            // Serialize the reader's configuration (settings only) to the XML stream
            initialReader.ExportToXml(xmlStateStream);
        }

        // Reset the XML stream position so it can be read from the beginning
        xmlStateStream.Position = 0;

        // ------------------------------------------------------------
        // 3. Import the reader state from XML, reapply the same image, and decode
        // ------------------------------------------------------------
        using (var importedReader = BarCodeReader.ImportFromXml(xmlStateStream))
        {
            // The image itself is not stored in the XML, so we set it manually
            using (var imageStream = new MemoryStream(barcodeImageBytes))
            {
                importedReader.SetBarCodeImage(imageStream);
            }

            // Decode type must be set again because it is not persisted in XML
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Perform the actual barcode reading
            BarCodeResult[] results = importedReader.ReadBarCodes();

            // Output the decoding results
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // 4. Clean up resources
        // ------------------------------------------------------------
        xmlStateStream.Dispose();
    }
}