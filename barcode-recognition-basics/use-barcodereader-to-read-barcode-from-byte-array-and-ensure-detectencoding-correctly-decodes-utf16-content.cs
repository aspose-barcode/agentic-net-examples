// Title: Read QR Code from Byte Array with UTF-16 Detection
// Description: Demonstrates using BarCodeReader to decode a QR code stored in a byte array, ensuring DetectEncoding correctly interprets UTF-16 encoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading category, showcasing how to generate a QR code, load it as a byte array, and read it with BarCodeReader. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and BarcodeSettings, which developers use for scanning barcodes from streams, files, or memory buffers, especially when handling different text encodings.
// Prompt: Use BarCodeReader to read a barcode from a byte array and ensure DetectEncoding correctly decodes UTF16 content.
// Tags: qr, utf-16, barcodereading, bytearray, aspose.barcode, encoding

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates reading a QR code from a byte array with UTF-16 detection using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code with UTF-16 text, reads it from a byte array, and outputs the decoded result.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr_utf16.png");

        // Generate a QR code with UTF-16 encoded text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, string.Empty))
        {
            // Set the code text using Unicode (UTF-16) encoding
            generator.SetCodeText("Привет UTF16", Encoding.Unicode);
            // Adjust the module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the generated QR code as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Load the generated image into a byte array
        byte[] imageBytes = File.ReadAllBytes(imagePath);

        // Read the barcode from the byte array with DetectEncoding enabled
        using (var memoryStream = new MemoryStream(imageBytes))
        {
            BaseDecodeType decodeType = DecodeType.QR;
            using (var reader = new BarCodeReader(memoryStream, decodeType))
            {
                // Enable automatic detection of the text encoding (UTF-16 in this case)
                reader.BarcodeSettings.DetectEncoding = true;

                // Iterate through all detected barcodes (only one expected)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}