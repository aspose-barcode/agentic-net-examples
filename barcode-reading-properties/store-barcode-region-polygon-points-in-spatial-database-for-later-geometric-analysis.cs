using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class SpatialDatabase
{
    private readonly string _filePath;

    public SpatialDatabase(string filePath)
    {
        _filePath = filePath;
        // Ensure the file exists
        if (!File.Exists(_filePath))
        {
            using (var fs = File.Create(_filePath)) { }
        }
    }

    public void SavePolygon(int barcodeIndex, Point[] points)
    {
        // Store as: Index|x1,y1;x2,y2;...
        var line = $"{barcodeIndex}|{string.Join(";", Array.ConvertAll(points, p => $"{p.X},{p.Y}"))}";
        File.AppendAllLines(_filePath, new[] { line });
    }
}

class Program
{
    static void Main()
    {
        // Create a simple Code128 barcode in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate bitmap without saving to disk
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize reader for all supported types
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.AllSupportedTypes))
                {
                    // Read barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Prepare a simple spatial storage (text file)
                    var db = new SpatialDatabase("barcode_regions.txt");

                    int index = 0;
                    foreach (BarCodeResult result in results)
                    {
                        // Extract polygon points of the barcode region
                        Point[] points = result.Region.Points;

                        // Store points in the spatial database
                        db.SavePolygon(index, points);

                        // Optional: output to console for verification
                        Console.WriteLine($"Barcode {index}: CodeText={result.CodeText}, Points={points.Length}");
                        index++;
                    }
                }
            }
        }
    }
}