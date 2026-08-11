// Title: Decode Planet barcode from image file
// Description: This example shows how to read a Planet barcode from an image file and obtain its numeric text using Aspose.BarCode.
// Category-Description: The sample belongs to the barcode decoding category of Aspose.BarCode, illustrating the use of BarCodeReader with DecodeType.Planet. It demonstrates typical scenarios such as processing scanned images to extract data from Planet symbology, a numeric‑only barcode used in logistics. Developers often need to validate or import such codes, and this example provides a concise reference for implementing the operation.
// Prompt: Decode a Planet barcode image from a file path and retrieve the encoded numeric string.
// Tags: planet, barcode, decode, image, aspose.barcode, barcodereader, decode type, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates decoding of a Planet barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the image path from arguments (or defaults), decodes any Planet barcodes, and prints the result.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image file path: use the first argument if supplied, otherwise fall back to a default file name.
        string imagePath = args.Length > 0 ? args[0] : "planet.png";

        // Ensure the specified file exists before attempting to decode.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader configured for the Planet symbology.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Planet))
        {
            // Retrieve all barcodes detected in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                // No Planet barcode was found in the supplied image.
                Console.WriteLine("No Planet barcode detected in the image.");
            }
            else
            {
                // Iterate through each detected barcode and output its decoded text.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Decoded Planet barcode text: {result.CodeText}");
                }
            }
        }
    }
}