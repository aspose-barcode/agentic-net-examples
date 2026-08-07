// Title: Read Mailmark 2D barcode from image using BarCodeReader
// Description: Demonstrates how to load an image file, detect a Mailmark 2D barcode (DataMatrix) with Aspose.BarCode, and extract its structured fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading category, showcasing the use of BarCodeReader with DecodeType.DataMatrix and ComplexCodetextReader to interpret complex symbologies such as Mailmark 2D. Developers commonly need to read postal barcodes from scanned images, extract metadata, and integrate it into mailing workflows. The key API classes include BarCodeReader, DecodeType, and ComplexCodetextReader, which together provide detection, decoding, and detailed parsing capabilities.
// Prompt: Read a Mailmark 2D barcode from an image file using BarCodeReader with DecodeType.DataMatrix.
// Tags: mailmark, datamatrix, barcode, reading, aspose.barcode, complexcodetext

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a Mailmark 2D barcode from an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the image, scans for DataMatrix barcodes, and parses Mailmark 2D details.
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

        // Create a BarCodeReader configured for DataMatrix (Mailmark 2D is based on DataMatrix)
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the raw decoded text
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // Attempt to decode the result as a Mailmark 2D codetext
                var mailmark = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (mailmark != null)
                {
                    // Output the parsed Mailmark 2D fields
                    Console.WriteLine("Mailmark 2D details:");
                    Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                    Console.WriteLine($"  InformationTypeID: {mailmark.InformationTypeID}");
                    Console.WriteLine($"  Class: {mailmark.Class}");
                    Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                    Console.WriteLine($"  DestinationPostCodeAndDPS: {mailmark.DestinationPostCodeAndDPS}");
                }
            }
        }
    }
}