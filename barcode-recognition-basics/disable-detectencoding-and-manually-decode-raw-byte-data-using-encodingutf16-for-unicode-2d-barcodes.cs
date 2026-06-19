using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code with Unicode (UTF-16) data,
/// then reading it back while manually handling encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code containing Cyrillic text, saves it to a memory stream,
    /// and reads it back using Aspose.BarCode with manual UTF-16 decoding.
    /// </summary>
    static void Main()
    {
        // Sample Unicode text (Cyrillic)
        const string unicodeText = "Привет";

        // Create a QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the text as UTF-16 (Unicode) bytes
            generator.SetCodeText(unicodeText, Encoding.Unicode);

            // Save the generated barcode image to a memory stream in PNG format
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                imageStream.Position = 0;

                // Load the image from the stream into a Bitmap for recognition
                using (var bitmap = new Bitmap(imageStream))
                {
                    // Initialize a barcode reader configured for QR codes
                    using (var reader = new BarCodeReader(bitmap, DecodeType.QR))
                    {
                        // Disable automatic encoding detection to demonstrate manual decoding
                        reader.BarcodeSettings.DetectEncoding = false;

                        // Iterate over all detected barcodes (should be one in this case)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // The automatically detected CodeText may be incorrect because DetectEncoding is off
                            Console.WriteLine("Auto-detected CodeText: " + result.CodeText);

                            // Retrieve the raw byte array from the barcode
                            byte[] rawBytes = result.CodeBytes;
                            // Manually decode the bytes using UTF-16 (Unicode) encoding
                            string manualText = Encoding.Unicode.GetString(rawBytes);
                            Console.WriteLine("Manually decoded (UTF-16): " + manualText);
                        }
                    }
                }
            }
        }
    }
}