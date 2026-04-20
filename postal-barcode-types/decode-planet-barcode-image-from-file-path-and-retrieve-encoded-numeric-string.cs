using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine the image file path: use first argument or a default sample file.
        string imagePath = args.Length > 0 ? args[0] : "planet.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the Planet symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Planet))
        {
            // Perform the recognition.
            var results = reader.ReadBarCodes();

            // Output the decoded numeric strings, if any.
            if (results.Length == 0)
            {
                Console.WriteLine("No Planet barcode detected in the image.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
        }
    }
}