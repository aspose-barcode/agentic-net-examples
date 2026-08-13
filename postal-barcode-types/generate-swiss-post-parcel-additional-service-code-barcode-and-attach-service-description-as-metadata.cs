// Title: Generate Swiss Post Parcel Additional Service Barcode with Metadata
// Description: Demonstrates how to create a Swiss Post Parcel barcode, embed an additional service code, and attach a human‑readable description as metadata.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.SwissPostParcel. It illustrates typical tasks such as setting CodeText, adding metadata via CodeTextParameters, and exporting the result to an image format. Developers working with postal barcode standards often need to generate service‑specific barcodes and embed descriptive information for downstream processing.
// Prompt: Generate a Swiss Post Parcel additional service code barcode and attach the service description as metadata.
// Tags: swisspostparcel, barcode, generation, png, metadata, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode with an additional service code and attaching a description as metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "SwissPostParcel.png";

        // Ensure the directory for the output file exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator for the Swiss Post Parcel (additional service) symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel))
        {
            // Set the additional service code (replace with actual data as needed)
            generator.CodeText = "1234567890";

            // Attach a human‑readable description as metadata to be displayed with the barcode
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Additional Service: Express Delivery";

            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}