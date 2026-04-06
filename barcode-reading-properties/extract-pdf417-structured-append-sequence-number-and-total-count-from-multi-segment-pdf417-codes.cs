using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create two macro PDF417 barcode segments representing a multi‑segment file.
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Pdf417Segments");
        Directory.CreateDirectory(folder);

        // Common file ID for all segments.
        int fileId = 12345;
        // Total number of segments.
        int totalSegments = 2;

        // First segment (index 0)
        string segment0Path = Path.Combine(folder, "segment0.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "Segment0 data"))
        {
            generator.Parameters.Barcode.Pdf417.Pdf417MacroFileID = fileId;
            generator.Parameters.Barcode.Pdf417.Pdf417MacroSegmentsCount = totalSegments;
            generator.Parameters.Barcode.Pdf417.Pdf417MacroSegmentID = 0;
            generator.Save(segment0Path);
        }

        // Second segment (index 1)
        string segment1Path = Path.Combine(folder, "segment1.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "Segment1 data"))
        {
            generator.Parameters.Barcode.Pdf417.Pdf417MacroFileID = fileId;
            generator.Parameters.Barcode.Pdf417.Pdf417MacroSegmentsCount = totalSegments;
            generator.Parameters.Barcode.Pdf417.Pdf417MacroSegmentID = 1;
            generator.Save(segment1Path);
        }

        // Read each segment and display its sequence number and total count.
        string[] segmentFiles = { segment0Path, segment1Path };
        foreach (string file in segmentFiles)
        {
            using (var reader = new BarCodeReader(file, DecodeType.MacroPdf417))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    int segmentId = result.Extended.Pdf417.MacroPdf417SegmentID;
                    int segmentsCount = result.Extended.Pdf417.MacroPdf417SegmentsCount;
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Sequence Number (Segment ID): {segmentId}");
                    Console.WriteLine($"  Total Segments (Segments Count): {segmentsCount}");
                }
            }
        }
    }
}