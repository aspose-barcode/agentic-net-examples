// Title: Read FoundCount Property After Barcode Detection
// Description: Demonstrates generating a Code128 barcode, saving it as an image, then reading the image to detect barcodes and output the total count using the FoundCount property.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to detect them in images. Developers commonly use these APIs for tasks such as inventory labeling, document processing, and automated data capture, where verifying the number of detected barcodes is essential.
// Prompt: Read the FoundCount property to verify the total number of barcodes detected in the source image.
// Tags: barcode generation, barcode recognition, code128, foundcount, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode image, reads it back, and displays the number of detected barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader to detect Code128 barcodes in the generated image
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Perform the barcode detection
            reader.ReadBarCodes();

            // Output the total number of barcodes found
            Console.WriteLine($"FoundCount: {reader.FoundCount}");

            // Iterate through each detected barcode and display its type and text
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Attempt to clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}