using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a Code128 barcode and save it as BMP into a memory stream
        using (var bmpStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(bmpStream, BarCodeImageFormat.Bmp);
            }

            // Create a second stream that contains the same BMP data plus dummy metadata
            byte[] bmpData = bmpStream.ToArray();
            byte[] dummyMetadata = Encoding.UTF8.GetBytes("METADATA");
            using (var metadataStream = new MemoryStream())
            {
                metadataStream.Write(bmpData, 0, bmpData.Length);
                metadataStream.Write(dummyMetadata, 0, dummyMetadata.Length);
                metadataStream.Position = 0;

                // Measure recognition time for the plain BMP
                long plainTime = MeasureRecognitionTime(bmpData);
                // Measure recognition time for the BMP with metadata
                long metaTime = MeasureRecognitionTime(metadataStream.ToArray());

                Console.WriteLine($"Recognition time (plain BMP): {plainTime} ms");
                Console.WriteLine($"Recognition time (BMP with metadata): {metaTime} ms");
                Console.WriteLine("Metadata does not affect recognition speed if times are comparable.");
            }
        }
    }

    static long MeasureRecognitionTime(byte[] imageData)
    {
        using (var stream = new MemoryStream(imageData))
        using (var reader = new BarCodeReader())
        {
            // Set the decode type to Code128
            reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);
            // Assign the image to the reader
            reader.SetBarCodeImage(stream);
            // Measure the time taken to read barcodes
            var stopwatch = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Output detected barcode text (if any)
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeText}");
            }

            return stopwatch.ElapsedMilliseconds;
        }
    }
}