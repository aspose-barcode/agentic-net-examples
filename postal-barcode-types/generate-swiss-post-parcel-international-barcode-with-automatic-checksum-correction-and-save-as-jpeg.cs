// Title: Generate Swiss Post Parcel barcode and read it back
// Description: Demonstrates creating a Swiss Post Parcel international barcode with automatic checksum correction, saving it as a JPEG, and then reading the barcode to verify its content.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.SwissPostParcel, configure visual parameters, save the image, and employ BarCodeReader with DecodeType.SwissPostParcel to extract data. Developers working with postal barcode standards often need to generate compliant barcodes and validate them programmatically.
// Prompt: Generate a Swiss Post Parcel international barcode with automatic checksum correction and save as JPEG.
// Tags: barcode generation, barcode recognition, swisspostparcel, jpeg, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and recognition of a Swiss Post Parcel barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Swiss Post Parcel barcode, saves it as JPEG, and reads it back.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output JPEG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostInternationalMail.jpeg");

        // Create a barcode generator for Swiss Post Parcel with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605017CH"))
        {
            // Configure visual parameters: X-dimension and bar height in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode image as a JPEG file
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Reuse the generator to produce an image for recognition
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605017CH"))
        {
            // Initialize a barcode reader for Swiss Post Parcel using the generated image
            using (var reader = new BarCodeReader(generator.GenerateBarCodeImage(), DecodeType.SwissPostParcel))
            {
                // Iterate through all detected barcodes and display their type and data
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected barcode type: {result.CodeTypeName}, Data: {result.CodeText}");
                }
            }
        }
    }
}