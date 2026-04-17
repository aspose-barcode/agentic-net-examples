using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image that may contain barcodes.
        string imagePath = "sample.png";

        // Verify that the file exists before attempting recognition.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the image.
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            // Set the timeout to 5000 milliseconds (5 seconds).
            reader.Timeout = 5000;

            try
            {
                // Perform barcode recognition.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition exceeds the timeout.
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms: {ex.Message}");
            }
        }
    }
}