// Title: Read raw encoded text from a Swiss QR Code image with exception handling
// Description: Demonstrates how to load a Swiss QR Code image, extract its raw encoded text, and decode it using Aspose.BarCode while gracefully handling possible recognition exceptions.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, DecodeType, and ComplexCodetextReader to process QR code images. Typical use cases include extracting payment or identification data encoded in Swiss QR codes, handling timeouts, and providing robust error handling for enterprise applications. Developers often need to generate sample images, read raw code text, and decode complex codetext structures, making this pattern a common reference for QR code processing tasks.
// Prompt: Read raw encoded text from a Swiss QR Code image and gracefully handle possible decoding exceptions.
// Tags: swissqr, qr, barcode, decoding, exception-handling, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads raw encoded text from a Swiss QR Code image,
/// decodes the SwissQR specific codetext, and demonstrates robust exception handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a sample Swiss QR Code image if missing, then reads and decodes it.
    /// </summary>
    static void Main()
    {
        // Define the full path to the QR code image.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

        // ------------------------------------------------------------
        // Ensure a sample QR code image exists (generate if missing)
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Sample encoded text – placeholder for demonstration purposes.
            string sampleText = "Sample SwissQR encoded text";

            // Generate a QR code image using Aspose.BarCode.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
            {
                // Save the generated image as PNG.
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated sample QR code image at: {imagePath}");
            }
        }

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file not found at {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the image and handle possible errors
        // ------------------------------------------------------------
        try
        {
            // Initialize the barcode reader for QR codes.
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                // Optional: set a timeout (in milliseconds) to avoid hanging on large images.
                reader.Timeout = 5000;

                // Attempt to read barcodes; handle timeout aborts separately.
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected in the image.");
                        return;
                    }

                    // Assume the first result contains the QR code we need.
                    string rawCodeText = results[0].CodeText;
                    Console.WriteLine($"Raw encoded text: {rawCodeText}");

                    // Decode SwissQR specific codetext using the complex codetext reader.
                    SwissQRCodetext swissResult = ComplexCodetextReader.TryDecodeSwissQR(rawCodeText);

                    if (swissResult != null)
                    {
                        Console.WriteLine("Successfully decoded SwissQR codetext.");
                        // Example: display the constructed codetext.
                        Console.WriteLine($"Constructed codetext: {swissResult.GetConstructedCodetext()}");
                    }
                    else
                    {
                        Console.WriteLine("The encoded text is not a valid SwissQR codetext or decoding failed.");
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle a recognition timeout or abort.
                    Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // General exception handling for unexpected errors.
            Console.WriteLine($"An error occurred during barcode processing: {ex.Message}");
        }
    }
}