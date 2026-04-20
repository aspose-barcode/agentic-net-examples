using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string imagePath = "sample_pdf417.png";

        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "LINKED_SAMPLE_DATA"))
            {
                // Enable linked mode
                generator.Parameters.Barcode.Pdf417.IsLinked = true;

                // Add some macro metadata (optional, just for demonstration)
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 42;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 1;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 0;
                generator.Parameters.Barcode.Pdf417.MacroPdf417Addressee = "John Doe";
                generator.Parameters.Barcode.Pdf417.MacroPdf417Sender = "Acme Corp";

                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(imagePath, ImageFormat.Png);
                }
            }

            Console.WriteLine($"Sample barcode image generated at '{imagePath}'.");
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type : {result.CodeTypeName}");
                Console.WriteLine($"Code Text    : {result.CodeText}");

                var pdf417 = result.Extended.Pdf417;
                Console.WriteLine($"IsLinked                 : {pdf417.IsLinked}");
                Console.WriteLine($"IsCode128Emulation       : {pdf417.IsCode128Emulation}");
                Console.WriteLine($"IsReaderInitialization   : {pdf417.IsReaderInitialization}");
                Console.WriteLine($"MacroPdf417FileID        : {pdf417.MacroPdf417FileID}");
                Console.WriteLine($"MacroPdf417SegmentsCount : {pdf417.MacroPdf417SegmentsCount}");
                Console.WriteLine($"MacroPdf417SegmentID     : {pdf417.MacroPdf417SegmentID}");
                Console.WriteLine($"MacroPdf417Addressee     : {pdf417.MacroPdf417Addressee}");
                Console.WriteLine($"MacroPdf417Sender        : {pdf417.MacroPdf417Sender}");
                Console.WriteLine($"MacroPdf417Terminator    : {pdf417.MacroPdf417Terminator}");
                Console.WriteLine($"MacroPdf417Checksum      : {pdf417.MacroPdf417Checksum}");
                Console.WriteLine($"MacroPdf417FileName      : {pdf417.MacroPdf417FileName}");
                Console.WriteLine($"MacroPdf417FileSize      : {pdf417.MacroPdf417FileSize}");
                Console.WriteLine($"MacroPdf417TimeStamp     : {pdf417.MacroPdf417TimeStamp}");
                Console.WriteLine();
            }
        }
    }
}