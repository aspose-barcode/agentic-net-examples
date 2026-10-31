// Title: Decode Mailmark barcode from a JPEG image
// Description: Generates a Mailmark 4‑state barcode, saves it as a JPEG, then decodes the barcode and prints the parsed fields to the console.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It demonstrates how to use ComplexBarcodeGenerator to create a Mailmark barcode, BarCodeReader to read the barcode from an image, and ComplexCodetextReader to parse the Mailmark codetext. Typical use cases include creating Mailmark labels for postal services and extracting their data in automated workflows. Developers working with Mailmark, QR, or other complex symbologies often need these APIs to handle both encoding and decoding tasks.
// Prompt: Decode a Mailmark barcode from a JPEG image and output the result to the console.
// Tags: mailmark, barcode, decode, jpeg, console, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark barcode, saving it as a JPEG,
/// and then decoding the barcode to display its components.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode image,
    /// reads it back, decodes the codetext, and writes the extracted values to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated JPEG image
        string imagePath = Path.Combine(tempFolder, "mailmark.jpg");

        // Configure the Mailmark codetext (4‑state format)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X‑dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the barcode as a JPEG file
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Open the image and attempt to read Mailmark barcodes
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No Mailmark barcode detected in the image.");
                return;
            }

            // Process each detected barcode
            foreach (var result in results)
            {
                // Try to parse the Mailmark codetext into its components
                var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Mailmark codetext.");
                    continue;
                }

                // Output the decoded fields to the console
                Console.WriteLine($"Format: {decoded.Format}");
                Console.WriteLine($"VersionID: {decoded.VersionID}");
                Console.WriteLine($"Class: {decoded.Class}");
                Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
                Console.WriteLine($"ItemID: {decoded.ItemID}");
                Console.WriteLine($"DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
            }
        }
    }
}