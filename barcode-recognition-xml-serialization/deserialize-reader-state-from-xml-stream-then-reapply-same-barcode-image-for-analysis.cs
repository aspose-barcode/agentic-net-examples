using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, exporting and importing reader settings via XML, and recognizing the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and keep it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable checksum (required for Code128)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Stream to hold the generated barcode image
            using (var imageStream = new MemoryStream())
            {
                // Save barcode image to the stream in PNG format
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position for reading
                imageStream.Position = 0;

                // Create a reader for the generated image
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Enable checksum validation to demonstrate state persistence
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Export the reader's current state to an XML stream
                    using (var xmlStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStream);
                        // Reset XML stream position for import
                        xmlStream.Position = 0;

                        // Import the previously saved state into a new reader instance
                        BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlStream);

                        // Reapply the same barcode image to the imported reader
                        imageStream.Position = 0;
                        importedReader.SetBarCodeImage(imageStream);

                        // Perform recognition using the imported settings
                        foreach (var result in importedReader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}