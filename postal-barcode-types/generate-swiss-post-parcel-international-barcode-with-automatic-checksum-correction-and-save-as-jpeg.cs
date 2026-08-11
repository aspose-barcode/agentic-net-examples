// Title: Generate Swiss Post Parcel Barcode and Save as JPEG
// Description: Demonstrates creating a Swiss Post Parcel international barcode with automatic checksum correction and exporting it to a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel. Typical use cases include creating shipping labels for Swiss Post parcels where the barcode must include a valid checksum. Developers often need to enable checksum generation and handle incorrect code text gracefully, then save the result in common image formats such as JPEG.
// Prompt: Generate a Swiss Post Parcel international barcode with automatic checksum correction and save as JPEG.
// Tags: barcode, swisspostparcel, checksum, jpeg, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode,
/// automatically corrects the checksum, and saves the image as JPEG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Calls the barcode generation routine.
    /// </summary>
    static void Main()
    {
        // Generate a Swiss Post Parcel barcode and save it as JPEG.
        GenerateSwissPostParcelBarcode();
    }

    /// <summary>
    /// Creates a Swiss Post Parcel barcode with checksum enabled,
    /// suppresses exceptions for incorrect code text, and writes the result to a JPEG file.
    /// </summary>
    static void GenerateSwissPostParcelBarcode()
    {
        // Sample code text; Aspose will correct checksum automatically.
        const string codeText = "1234567890123";

        // Initialize the barcode generator for Swiss Post Parcel symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Allow automatic correction of incorrect code text (no exception thrown).
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Define the output file path.
            const string outputPath = "SwissPostParcel.jpg";

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}