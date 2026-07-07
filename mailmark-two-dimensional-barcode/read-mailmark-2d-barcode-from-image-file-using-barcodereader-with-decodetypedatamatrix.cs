// Title: Read Mailmark 2D barcode from image using BarCodeReader
// Description: Demonstrates how to load an image containing a Mailmark 2D (DataMatrix) barcode, detect it with Aspose.BarCode, and decode its structured fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on reading complex 2D symbologies such as Mailmark. It showcases the BarCodeReader with DecodeType.DataMatrix and the ComplexCodetextReader for parsing Mailmark 2D codetext into a strongly‑typed object. Developers working with postal or logistics solutions often need to extract Mailmark information from scanned images, making this pattern a common use case.
// Prompt: Read a Mailmark 2D barcode from an image file using BarCodeReader with DecodeType.DataMatrix.
// Tags: mailmark, datamatrix, barcode, reading, aspose.barcode, complexcodetext

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading a Mailmark 2D barcode from an image file and decoding its fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the image, detects Mailmark 2D barcode, and prints decoded information.
    /// </summary>
    static void Main()
    {
        // Path to the image containing the Mailmark 2D barcode
        string imagePath = "mailmark2d.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the reader for DataMatrix symbology (Mailmark 2D is encoded in a DataMatrix)
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Perform the recognition
            var results = reader.ReadBarCodes();

            // Check if any barcodes were detected
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Iterate through all detected barcodes (typically only one Mailmark 2D)
            foreach (var result in results)
            {
                // Output basic barcode information
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                // Attempt to decode the Mailmark 2D codetext into a strongly‑typed object
                var mailmark = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (mailmark != null)
                {
                    // Print each decoded field of the Mailmark 2D structure
                    Console.WriteLine("Decoded Mailmark 2D codetext:");
                    Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                    Console.WriteLine($"  InformationTypeID: {mailmark.InformationTypeID}");
                    Console.WriteLine($"  Class: {mailmark.Class}");
                    Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                    Console.WriteLine($"  DestinationPostCodeAndDPS: {mailmark.DestinationPostCodeAndDPS}");
                    Console.WriteLine($"  SupplyChainID: {mailmark.SupplyChainID}");
                    Console.WriteLine($"  RTSFlag: {mailmark.RTSFlag}");
                    Console.WriteLine($"  ReturnToSenderPostCode: {mailmark.ReturnToSenderPostCode}");
                }
                else
                {
                    // Decoding failed – inform the user
                    Console.WriteLine("Failed to decode Mailmark 2D codetext.");
                }
            }
        }
    }
}