using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeRecognitionRetry
{
    class Program
    {
        static void Main()
        {
            // Create a sample barcode image to be recognized
            string imagePath = "sample.png";
            CreateSampleBarcode(imagePath);

            // Try to recognize the barcode with retry logic
            int maxRetries = 3;
            RecognizeWithRetry(imagePath, maxRetries);
        }

        // Generates a simple Code128 barcode and saves it to the specified path
        private static void CreateSampleBarcode(string path)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "RetryDemo"))
            {
                // Save the barcode image to a file
                generator.Save(path);
            }
        }

        // Attempts to read barcodes from the image, retrying when a timeout occurs
        private static void RecognizeWithRetry(string imagePath, int maxRetries)
        {
            int attempt = 0;
            bool success = false;

            while (attempt <= maxRetries && !success)
            {
                try
                {
                    using (var reader = new BarCodeReader(imagePath))
                    {
                        // Set a short timeout to force a possible timeout scenario
                        reader.Timeout = 500; // milliseconds

                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                            Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                        }

                        success = true; // Recognition succeeded
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    attempt++;
                    Console.WriteLine($"Recognition aborted due to timeout (attempt {attempt}/{maxRetries}). ExecutionTime={ex.ExecutionTime}ms");

                    if (attempt > maxRetries)
                    {
                        Console.WriteLine("Maximum retry attempts reached. Rethrowing exception.");
                        throw;
                    }

                    // Optionally adjust timeout or other settings before retrying
                    // For this example we keep the same timeout
                }
            }

            if (success)
            {
                Console.WriteLine("Barcode recognition completed successfully.");
            }
        }
    }
}