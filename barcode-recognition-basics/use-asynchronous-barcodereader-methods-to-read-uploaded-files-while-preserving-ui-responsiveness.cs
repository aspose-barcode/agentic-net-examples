using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static async Task Main(string[] args)
    {
        // Define a temporary file path for the generated barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a sample barcode and save it to the temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Optional: set image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image
            generator.Save(tempFile);
        }

        // Asynchronously read barcodes from the generated image
        await ReadBarcodesAsync(tempFile);

        // Clean up the temporary file
        if (File.Exists(tempFile))
        {
            File.Delete(tempFile);
        }
    }

    // Asynchronous method that reads barcodes from an image file
    private static async Task ReadBarcodesAsync(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Run the blocking barcode reading operation on a background thread
        await Task.Run(() =>
        {
            // Initialize the reader for all supported barcode types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Optionally set a timeout (e.g., 5 seconds)
                reader.Timeout = 5000;

                // Perform the recognition
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}");
                    Console.WriteLine($"Text: {result.CodeText}");
                    Console.WriteLine($"Confidence: {result.Confidence}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                    // Access the barcode region rectangle
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");

                    // Angle of the detected barcode
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        });
    }
}