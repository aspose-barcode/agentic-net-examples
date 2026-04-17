using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing; // Required for Bitmap type if needed

class Program
{
    static void Main()
    {
        // Path to an image file with an unsupported format (e.g., a text file)
        string imagePath = "sample.txt";

        // Verify the file exists before attempting to load it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the barcode reader
            using (var reader = new BarCodeReader())
            {
                // Attempt to set the image for recognition.
                // This will throw an exception for unsupported image formats.
                reader.SetBarCodeImage(imagePath);

                // If the image were valid, read barcodes (not expected here)
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
        catch (BarCodeException ex)
        {
            // Specific Aspose.BarCode exception handling
            Console.WriteLine($"BarCodeException: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General exception handling for unsupported formats or other errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}