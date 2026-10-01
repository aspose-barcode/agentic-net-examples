// Title: Read barcodes from a JPEG file using BarCodeReader
// Description: Demonstrates generating a QR barcode, saving it as a JPEG image, and then reading the barcode back to retrieve detection results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to detect and decode them from image files. Typical scenarios include scanning printed or digital images for QR codes, product barcodes, or other symbologies in desktop or server applications. Developers often need to combine these APIs to automate barcode processing pipelines.
// Prompt: Read barcodes from a JPEG file using BarCodeReader constructor and retrieve detection results.
// Tags: qr, barcode, read, jpeg, aspose.barcode, generation, recognition, detection

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a QR barcode image, saves it as JPEG, and reads it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, saves it as JPEG, reads it, and outputs detection details.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.jpg");

        // Generate a simple QR barcode and save it as JPEG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read barcodes from the JPEG file
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Optional: set a quality preset for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the detection
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Output details for each detected barcode
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text       : {result.CodeText}");
                    Console.WriteLine($"Symbology       : {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality : {result.ReadingQuality}");

                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit
        }
    }
}