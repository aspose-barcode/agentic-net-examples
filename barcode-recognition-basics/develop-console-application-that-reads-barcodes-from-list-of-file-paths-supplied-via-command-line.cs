using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least one file path is provided.
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("Please provide one or more image file paths as command‑line arguments.");
            return;
        }

        // Process each supplied file path.
        foreach (var filePath in args)
        {
            Console.WriteLine($"Processing: {filePath}");

            // Verify the file exists before attempting to read.
            if (!File.Exists(filePath))
            {
                Console.WriteLine("  Error: File not found.");
                continue;
            }

            try
            {
                // Create a BarCodeReader for the image file.
                using (var reader = new BarCodeReader(filePath))
                {
                    // Read all barcodes present in the image.
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("  No barcodes detected.");
                    }
                    else
                    {
                        // Output each detected barcode's type and decoded text.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any exceptions that occur during reading.
                Console.WriteLine($"  Exception: {ex.Message}");
            }
        }
    }
}