// Title: Generate Swiss Post Parcel International barcode with checksum correction
// Description: Demonstrates creating a Swiss Post Parcel barcode, automatically correcting its checksum, and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.SwissPostParcel. It shows how to configure automatic checksum handling and export the result to a JPEG file, a common requirement for integrating barcode images into documents, web pages, or printing workflows.
// Prompt: Generate a Swiss Post Parcel international barcode with automatic checksum correction and save as JPEG.
// Tags: swisspostparcel, barcode, generation, jpeg, checksum, aspnet, aspnetcore, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss Post Parcel International barcode,
/// lets the library automatically correct the checksum, and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the raw code text; the library will adjust the checksum if needed.
        const string codeText = "1234567890123456";

        // Specify the output file path for the generated JPEG image.
        const string outputPath = "SwissPostParcel.jpeg";

        // Choose the Swiss Post Parcel symbology for barcode generation.
        BaseEncodeType symbology = EncodeTypes.SwissPostParcel;

        // Initialize the barcode generator with the selected symbology and code text.
        using (var generator = new BarcodeGenerator(symbology, codeText))
        {
            // Automatic checksum correction is enabled by default.
            // If you need to enforce strict validation, you could set:
            // generator.Parameters.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode image as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}