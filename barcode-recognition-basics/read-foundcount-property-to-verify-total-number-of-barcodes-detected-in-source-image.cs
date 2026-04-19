using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image that may contain barcodes
        string imagePath = "sample.png";

        // Verify that the file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the BarCodeReader for common 1D and 2D symbologies
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix))
        {
            // Perform the recognition
            reader.ReadBarCodes();

            // Retrieve the total number of detected barcodes
            int totalBarcodes = reader.FoundCount;

            // Output the result
            Console.WriteLine($"Total barcodes detected: {totalBarcodes}");
        }
    }
}