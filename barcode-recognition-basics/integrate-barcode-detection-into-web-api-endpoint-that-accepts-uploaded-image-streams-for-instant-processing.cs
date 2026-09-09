// Title: Barcode detection from an uploaded image stream using Aspose.BarCode
// Description: Demonstrates generating a QR code, then reading it from a stream as if it were uploaded to a web API, showing instant barcode detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader with DecodeType to recognize multiple symbologies. Typical scenarios include processing uploaded images in web services, validating scanned codes, and extracting data from various barcode formats. Developers often need to quickly generate sample barcodes and then detect them from streams using these core API classes.
// Prompt: Integrate barcode detection into a web API endpoint that accepts uploaded image streams for instant processing.
// Tags: qr,code128,barcode detection,barcode recognition,aspose.barcode,web api,stream processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a QR barcode, simulate receiving it as an uploaded image stream,
/// and detect barcodes using Aspose.BarCode's recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, reads it from a stream, and outputs detected results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Simulate receiving an uploaded image stream (e.g., from a web API endpoint)
        using (FileStream stream = new FileStream(barcodePath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the barcode reader with the desired decode types (QR and Code128)
            using (var reader = new BarCodeReader(stream, DecodeType.QR, DecodeType.Code128))
            {
                Console.WriteLine("Detected barcodes:");
                // Iterate through all detected barcodes in the stream
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup may fail if files are still in use
        }
    }
}