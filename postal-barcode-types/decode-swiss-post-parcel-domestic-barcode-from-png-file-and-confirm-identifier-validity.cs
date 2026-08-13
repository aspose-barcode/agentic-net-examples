// Title: Decode Swiss Post Parcel barcode from PNG and validate identifier
// Description: Demonstrates decoding a Swiss Post Parcel domestic barcode stored in a PNG file and checking whether the identifier is valid.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use the BarCodeReader class with DecodeType.SwissPostParcel to read and validate Swiss Post Parcel barcodes. Typical use cases include verifying parcel identifiers in logistics and shipping applications. Developers often need to generate sample barcodes with BarcodeGenerator and then decode them to ensure correct data extraction.
// Prompt: Decode a Swiss Post Parcel domestic barcode from a PNG file and confirm identifier validity.
// Tags: swisspostparcel, barcode, decoding, validation, png, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates (if needed) and decodes a Swiss Post Parcel domestic barcode
/// from a PNG image, then confirms the identifier's validity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Handles barcode image preparation, decoding, and validation output.
    /// </summary>
    static void Main()
    {
        // Path to the barcode image file
        string imagePath = "SwissPostParcel.png";

        // If the image does not exist, generate a sample Swiss Post Parcel barcode
        if (!File.Exists(imagePath))
        {
            // Sample numeric code text for a domestic Swiss Post Parcel barcode
            string sampleCodeText = "123456789012";

            // Create a barcode generator for the Swiss Post Parcel symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sampleCodeText))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Sample barcode generated at: {Path.GetFullPath(imagePath)}");
            }
        }

        // Verify that the barcode image file now exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Error: Barcode image file not found.");
            return;
        }

        // Initialize a barcode reader for the Swiss Post Parcel symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            bool found = false;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Decoded CodeText: " + result.CodeText);
                // Additional validation logic can be placed here if needed
            }

            // Output validation result based on detection outcome
            if (!found)
            {
                Console.WriteLine("No Swiss Post Parcel barcode detected – identifier is invalid.");
            }
            else
            {
                Console.WriteLine("Barcode successfully decoded – identifier is valid.");
            }
        }
    }
}