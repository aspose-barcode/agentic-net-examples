// Title: Decode Swiss Post Parcel domestic barcode from PNG
// Description: Demonstrates how to read a Swiss Post Parcel domestic barcode from a PNG image and verify its identifier format.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, illustrating the use of BarCodeReader with BaseDecodeType.SwissPostParcel. It shows typical steps for loading an image, scanning for barcodes, extracting code text, and performing custom validation. Developers working with postal barcode symbologies can use this pattern to integrate barcode recognition into shipping or logistics applications.
// Prompt: Decode a Swiss Post Parcel domestic barcode from a PNG file and confirm identifier validity.
// Tags: barcode, decoding, swisspost, parcel, png, validation, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program to decode a Swiss Post Parcel domestic barcode from an image and validate its identifier.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the barcode from the specified PNG file and checks identifier validity.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image containing the Swiss Post barcode
        string imagePath = "SwissPostDomestic.png";

        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Specify the decode type for Swiss Post Parcel barcodes
        BaseDecodeType decodeType = DecodeType.SwissPostParcel;

        // Initialize the barcode reader with the image and decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;

                // Output basic barcode information
                Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode data: {result.CodeText}");

                // Validate the identifier according to Swiss Post domestic rules
                if (IsValidSwissPostDomestic(result.CodeText))
                {
                    Console.WriteLine("Identifier is valid.");
                }
                else
                {
                    Console.WriteLine("Identifier is INVALID.");
                }
            }

            // Inform the user if no barcodes were detected
            if (!anyFound)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }
    }

    /// <summary>
    /// Validates the format of a Swiss Post domestic barcode identifier.
    /// </summary>
    /// <param name="codeText">The raw barcode text extracted from the image.</param>
    /// <returns>True if the identifier matches the expected pattern; otherwise, false.</returns>
    static bool IsValidSwissPostDomestic(string codeText)
    {
        // Reject null, empty, or whitespace strings
        if (string.IsNullOrWhiteSpace(codeText))
            return false;

        // Remove any dot separators that may be present in the barcode text
        string digits = codeText.Replace(".", string.Empty);

        // The identifier must be exactly 18 digits and start with 98 or 99
        if (!Regex.IsMatch(digits, @"^(98|99)\d{16}$"))
            return false;

        // Checksum validation is assumed to be handled by Aspose.BarCode during reading
        return true;
    }
}