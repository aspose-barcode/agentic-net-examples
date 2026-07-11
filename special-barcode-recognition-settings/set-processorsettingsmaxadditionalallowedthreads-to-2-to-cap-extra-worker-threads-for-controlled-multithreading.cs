// Title: Cap barcode processor threads and generate/read a Code128 barcode
// Description: Demonstrates how to limit the number of additional worker threads used by Aspose.BarCode's processor, then creates a Code128 barcode image and reads it back.
// Category-Description: This example belongs to the Aspose.BarCode multithreading and processing category. It shows how to configure ProcessorSettings (specifically MaxAdditionalAllowedThreads) to control resource usage, a common need when running barcode operations in parallel or in constrained environments. The sample also covers basic barcode generation (BarcodeGenerator) and recognition (BarCodeReader), typical tasks for developers integrating barcode functionality into .NET applications.
// Prompt: Set ProcessorSettings.MaxAdditionalAllowedThreads to 2 to cap extra worker threads for controlled multithreading.
// Tags: code128, generation, recognition, threading, aspose.barcode, processorsettings

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting a thread limit for the barcode processor, generating a Code128 barcode,
/// and then reading the generated barcode using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures thread limits, creates a barcode image,
    /// and reads the barcode back, outputting its type and text to the console.
    /// </summary>
    static void Main()
    {
        // Limit the number of additional worker threads the barcode processor may spawn.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;

        // Define the output file path for the generated barcode image.
        const string imagePath = "sample.png";

        // Generate a Code128 barcode with the value "123456" and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath);
        }

        // Initialize a barcode reader for the saved image, specifying the expected symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Iterate through all detected barcodes (in this case, just one) and display details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode Text: {result.CodeText}");
            }
        }
    }
}