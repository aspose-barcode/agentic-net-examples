using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the TIFF image containing DataMatrix barcodes
        string imagePath = "datamatrix.tif";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for DataMatrix symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");

                // Extended DataMatrix parameters (symbol size, encoding mode, etc.)
                // The ToString() method provides a human‑readable representation.
                if (result.Extended?.DataMatrix != null)
                {
                    Console.WriteLine("DataMatrix Extended Parameters:");
                    Console.WriteLine(result.Extended.DataMatrix.ToString());
                }
                else
                {
                    Console.WriteLine("No extended DataMatrix parameters available.");
                }

                Console.WriteLine(new string('-', 40));
            }
        }
    }
}