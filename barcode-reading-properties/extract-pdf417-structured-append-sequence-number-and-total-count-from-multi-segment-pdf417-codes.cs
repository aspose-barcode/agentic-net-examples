using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image containing the multi‑segment PDF417 barcode
        string imagePath = "pdf417_multi.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for PDF417 type
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            bool found = false;

            // Iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access extended parameters that hold Macro PDF417 metadata
                var pdf417Ext = result.Extended?.Pdf417;
                if (pdf417Ext != null)
                {
                    Console.WriteLine($"Segment ID (sequence number): {pdf417Ext.MacroPdf417SegmentID}");
                    Console.WriteLine($"Total Segments: {pdf417Ext.MacroPdf417SegmentsCount}");
                }
                else
                {
                    Console.WriteLine("Extended PDF417 parameters not available.");
                }
            }

            if (!found)
            {
                Console.WriteLine("No PDF417 barcodes detected in the image.");
            }
        }
    }
}