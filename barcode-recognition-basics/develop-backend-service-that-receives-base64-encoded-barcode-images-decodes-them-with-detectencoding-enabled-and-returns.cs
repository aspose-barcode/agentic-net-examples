using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeDecodeDemo
{
    class Program
    {
        static void Main()
        {
            // Step 1: Generate a QR code with Unicode text and obtain its Base64 representation.
            string originalText = "Привет"; // Cyrillic text to test encoding detection.
            string base64Image;

            using (var generationStream = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
                {
                    // Save the barcode image to the memory stream in PNG format.
                    generator.Save(generationStream, BarCodeImageFormat.Png);
                }

                // Convert the generated image to a Base64 string.
                base64Image = Convert.ToBase64String(generationStream.ToArray());
            }

            // Step 2: Decode the Base64 string back to an image stream.
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using (var imageStream = new MemoryStream(imageBytes))
            {
                // Step 3: Create a BarCodeReader for QR decoding.
                using (var reader = new BarCodeReader(imageStream, DecodeType.QR))
                {
                    // Enable detection of text encoding.
                    reader.BarcodeSettings.DetectEncoding = true;

                    // Read all barcodes from the image.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Output the decoded text.
                        Console.WriteLine("Decoded Text: " + result.CodeText);
                    }
                }
            }
        }
    }
}