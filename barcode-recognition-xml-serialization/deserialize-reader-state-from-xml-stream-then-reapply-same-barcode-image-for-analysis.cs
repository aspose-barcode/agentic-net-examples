// Title: Deserialize BarCodeReader state from XML and reuse barcode image
// Description: Demonstrates exporting a BarCodeReader's configuration to XML, then importing it and applying the same barcode image for recognition.
// Category-Description: This example belongs to the Aspose.BarCode reading and configuration category. It shows how to use BarCodeReader's ExportToXml and ImportFromXml methods along with BarcodeSettings and QualitySettings to persist and restore reader state. Typical use cases include saving reader configurations for later reuse, batch processing, or sharing settings across applications. Developers often need to serialize reader settings, reapply them to new images, and perform consistent barcode decoding.
// Prompt: Deserialize the reader state from an XML stream, then reapply the same barcode image for analysis.
// Tags: qr, barcode, serialization, xml, readerstate, import, export, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a BarCodeReader's state to XML, importing it back,
/// and reusing the same barcode image for recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, saves reader state,
    /// imports it, and reads the barcode from the image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the barcode image and the exported XML state
        string imagePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "readerState.xml");

        // Generate a sample QR barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a BarCodeReader for the generated image and configure settings
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Example optional setting: ignore FNC characters
            reader.BarcodeSettings.StripFNC = true;
            // Set a small X-dimension for higher resolution decoding
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Export the current reader state to an in‑memory XML stream
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for subsequent operations

                // Optionally persist the XML to a file for inspection or debugging
                using (var fileStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    xmlStream.CopyTo(fileStream);
                }

                // Reset stream position again before importing
                xmlStream.Position = 0;

                // Import a new BarCodeReader instance from the exported XML state
                using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                {
                    // Reapply the same barcode image and decode type to the imported reader
                    importedReader.SetBarCodeImage(imagePath);
                    importedReader.SetBarCodeReadType(DecodeType.QR);

                    // Perform barcode recognition using the restored settings
                    var results = importedReader.ReadBarCodes();
                    Console.WriteLine($"Barcodes read: {results.Length}");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath)) File.Delete(imagePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}