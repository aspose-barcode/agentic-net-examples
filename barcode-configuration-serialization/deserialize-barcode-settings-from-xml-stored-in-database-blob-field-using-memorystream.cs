using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates exporting and importing barcode generator settings using XML and memory streams.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Step 1: Create a barcode generator with sample settings.
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Enable checksum and set barcode color to blue.
            originalGenerator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            originalGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Step 2: Export the generator settings to XML stored in a MemoryStream.
            using (var exportStream = new MemoryStream())
            {
                originalGenerator.ExportToXml(exportStream);
                // Retrieve the XML bytes as they would be stored in a DB BLOB field.
                byte[] xmlBlob = exportStream.ToArray();

                // Step 3: Simulate reading the XML BLOB from the database.
                using (var importStream = new MemoryStream(xmlBlob))
                {
                    // Step 4: Deserialize the barcode settings from the XML.
                    using (var deserializedGenerator = BarcodeGenerator.ImportFromXml(importStream))
                    {
                        // Verify that deserialization succeeded by generating the barcode image.
                        string outputPath = "deserialized_barcode.png";
                        deserializedGenerator.Save(outputPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
                    }
                }
            }
        }
    }
}