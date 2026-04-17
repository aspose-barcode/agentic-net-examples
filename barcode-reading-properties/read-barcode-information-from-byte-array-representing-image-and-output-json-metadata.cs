using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeReaderSample
{
    class Program
    {
        static void Main()
        {
            // Generate a sample barcode image and obtain its byte array
            byte[] imageBytes = GenerateSampleBarcode();

            // Read barcodes from the byte array
            List<object> barcodeData = ReadBarcodesFromBytes(imageBytes);

            // Serialize the metadata to JSON and output
            string json = JsonSerializer.Serialize(barcodeData, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }

        private static byte[] GenerateSampleBarcode()
        {
            // Sample text to encode
            const string sampleText = "1234567890";

            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
            {
                // Generate the bitmap image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
        }

        private static List<object> ReadBarcodesFromBytes(byte[] imageBytes)
        {
            var results = new List<object>();

            // Load the image bytes into a memory stream
            using (var ms = new MemoryStream(imageBytes))
            {
                // Initialize the reader for all supported barcode types
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Perform recognition
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Build a simple metadata object
                        var metadata = new
                        {
                            TypeName = result.CodeTypeName,
                            CodeText = result.CodeText,
                            Confidence = result.Confidence.ToString(),
                            ReadingQuality = result.ReadingQuality,
                            Angle = result.Region.Angle,
                            Rectangle = new
                            {
                                X = result.Region.Rectangle.X,
                                Y = result.Region.Rectangle.Y,
                                Width = result.Region.Rectangle.Width,
                                Height = result.Region.Rectangle.Height
                            }
                        };
                        results.Add(metadata);
                    }
                }
            }

            return results;
        }
    }
}