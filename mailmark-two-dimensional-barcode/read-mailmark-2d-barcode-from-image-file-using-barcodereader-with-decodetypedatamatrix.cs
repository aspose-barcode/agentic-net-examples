using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates how to read and decode a Mailmark 2D barcode from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the image containing the Mailmark 2D barcode.
        string imagePath = "mailmark2d.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader configured for DataMatrix (Mailmark 2D uses DataMatrix).
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Perform the recognition and retrieve all detected barcodes.
            var results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Iterate through each detected barcode result.
            foreach (var result in results)
            {
                // Output basic barcode information.
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                // Attempt to decode the Mailmark 2D complex codetext into its constituent fields.
                var mailmark = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (mailmark != null)
                {
                    // Display each decoded field of the Mailmark 2D barcode.
                    Console.WriteLine("Decoded Mailmark2D fields:");
                    Console.WriteLine($"VersionID: {mailmark.VersionID}");
                    Console.WriteLine($"InformationTypeID: {mailmark.InformationTypeID}");
                    Console.WriteLine($"Class: {mailmark.Class}");
                    Console.WriteLine($"RTSFlag: {mailmark.RTSFlag}");
                    Console.WriteLine($"SupplyChainID: {mailmark.SupplyChainID}");
                    Console.WriteLine($"ItemID: {mailmark.ItemID}");
                    Console.WriteLine($"DestinationPostCodeAndDPS: {mailmark.DestinationPostCodeAndDPS}");
                    Console.WriteLine($"ReturnToSenderPostCode: {mailmark.ReturnToSenderPostCode}");
                    Console.WriteLine($"UPUCountryID: {mailmark.UPUCountryID}");
                }
                else
                {
                    // Notify the user if decoding the complex codetext failed.
                    Console.WriteLine("Failed to decode Mailmark2D complex codetext.");
                }

                Console.WriteLine(); // Blank line between results for readability.
            }
        }
    }
}