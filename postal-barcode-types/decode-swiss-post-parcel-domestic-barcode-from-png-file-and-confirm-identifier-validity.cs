// Title: Decode Swiss Post Parcel barcode from PNG
// Description: Demonstrates how to read a Swiss Post Parcel domestic barcode stored in a PNG image and verify its identifier.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, illustrating the use of BarCodeReader with DecodeType.SwissPostParcel. It shows typical steps such as loading an image, setting quality and checksum validation, and extracting the CodeText. Developers working with postal barcode symbologies often need to validate identifiers in shipping and logistics applications.
// Prompt: Decode a Swiss Post Parcel domestic barcode from a PNG file and confirm identifier validity.
// Tags: swisspostparcel, decode, barcode, console, barcodereader, qualitysettings, checksumvalidation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes a Swiss Post Parcel barcode from a PNG file
/// and confirms the validity of the extracted identifier.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs barcode detection and validation.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image containing the Swiss Post Parcel barcode
        const string imagePath = "SwissPostParcel.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for the Swiss Post Parcel symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            // Apply a standard quality preset for balanced performance and accuracy
            reader.QualitySettings = QualitySettings.NormalQuality;

            // Enable checksum validation to ensure barcode integrity
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Execute the recognition process and retrieve all detected barcodes
            var results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No Swiss Post Parcel barcode detected.");
                return;
            }

            // Iterate through each detected barcode (typically only one)
            foreach (var result in results)
            {
                // A valid barcode should have a non‑empty CodeText
                if (!string.IsNullOrEmpty(result.CodeText))
                {
                    Console.WriteLine($"Valid Swiss Post Parcel barcode detected: {result.CodeText}");
                }
                else
                {
                    Console.WriteLine("Detected barcode but CodeText is empty – invalid.");
                }
            }
        }
    }
}