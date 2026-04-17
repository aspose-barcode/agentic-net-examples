using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the barcode image file
        string imagePath = "barcode.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the desired symbologies (example: Code128)
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Adjust the XDimension quality setting to detect small elements (≈3 pixels)
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Read all barcodes in the image and output their text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode: {result.CodeText}");
            }
        }
    }
}