using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

namespace AsposeBarcodeS3Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the image file containing the PDF417 barcode.
            // Replace with the actual path where the image is stored.
            string localFilePath = "downloaded_image.png";

            if (!File.Exists(localFilePath))
            {
                Console.WriteLine($"File not found: {localFilePath}");
                return;
            }

            // Read PDF417 barcode and extract linked state metadata
            try
            {
                using (BarCodeReader reader = new BarCodeReader(localFilePath, DecodeType.Pdf417))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                        Console.WriteLine("Barcode Text: " + result.CodeText);

                        // Access PDF417 extended parameters
                        if (result.Extended?.Pdf417 != null)
                        {
                            Console.WriteLine("IsLinked: " + result.Extended.Pdf417.IsLinked);
                            Console.WriteLine("IsCode128Emulation: " + result.Extended.Pdf417.IsCode128Emulation);
                            Console.WriteLine("MacroPdf417FileID: " + result.Extended.Pdf417.MacroPdf417FileID);
                            Console.WriteLine("MacroPdf417SegmentID: " + result.Extended.Pdf417.MacroPdf417SegmentID);
                            Console.WriteLine("MacroPdf417SegmentsCount: " + result.Extended.Pdf417.MacroPdf417SegmentsCount);
                        }
                        else
                        {
                            Console.WriteLine("No PDF417 extended parameters available.");
                        }

                        Console.WriteLine(new string('-', 40));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during barcode recognition: " + ex.Message);
            }
        }
    }
}