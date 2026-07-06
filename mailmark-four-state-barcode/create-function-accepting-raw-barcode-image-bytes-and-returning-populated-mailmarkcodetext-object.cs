// Title: Decode Mailmark barcode from image bytes
// Description: Demonstrates how to read a Mailmark barcode from raw image bytes and extract its fields using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on complex barcode types such as Mailmark. It shows how to use BarCodeReader with DecodeType.Mailmark, MemoryStream for byte input, and ComplexCodetextReader to parse the codetext into a MailmarkCodetext object. Developers working with postal or logistics solutions often need to decode Mailmark symbols from scanned images or byte streams.
// Prompt: Create a function accepting raw barcode image bytes and returning a populated MailmarkCodetext object.
// Tags: mailmark, barcode, decoding, image-bytes, aspnet, aspnet-barcode, complexbarcode, codetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

namespace MailmarkDecoder
{
    /// <summary>
    /// Sample console application that decodes a Mailmark barcode from an image file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Loads a sample image, decodes the Mailmark barcode,
        /// and prints the extracted fields to the console.
        /// </summary>
        static void Main()
        {
            // Path to the sample image containing a Mailmark barcode.
            string imagePath = "mailmark.png";

            // Verify that the image file exists before attempting to read it.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Image file not found: {imagePath}");
                return;
            }

            // Read the entire image file into a byte array.
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Decode the Mailmark barcode from the raw image bytes.
            MailmarkCodetext mailmark = DecodeMailmarkFromBytes(imageBytes);

            // If decoding failed, inform the user and exit.
            if (mailmark == null)
            {
                Console.WriteLine("No Mailmark barcode could be decoded.");
                return;
            }

            // Output the decoded Mailmark fields to the console.
            Console.WriteLine($"Format: {mailmark.Format}");
            Console.WriteLine($"VersionID: {mailmark.VersionID}");
            Console.WriteLine($"Class: {mailmark.Class}");
            Console.WriteLine($"SupplychainID: {mailmark.SupplychainID}");
            Console.WriteLine($"ItemID: {mailmark.ItemID}");
            Console.WriteLine($"DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
        }

        /// <summary>
        /// Decodes a Mailmark barcode from raw image bytes.
        /// </summary>
        /// <param name="imageBytes">Byte array containing the barcode image.</param>
        /// <returns>Populated <see cref="MailmarkCodetext"/> object, or null if decoding fails.</returns>
        static MailmarkCodetext DecodeMailmarkFromBytes(byte[] imageBytes)
        {
            // Validate input.
            if (imageBytes == null || imageBytes.Length == 0)
                throw new ArgumentException("Image bytes cannot be null or empty.", nameof(imageBytes));

            // Load the image bytes into a memory stream for the reader.
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // Initialize the barcode reader for the Mailmark symbology.
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Mailmark))
                {
                    // Read all detected barcodes from the stream.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were found, return null.
                    if (results == null || results.Length == 0)
                        return null;

                    // Assume the first result contains the desired Mailmark barcode.
                    string encodedCodetext = results[0].CodeText;

                    // Convert the encoded codetext into a MailmarkCodetext object.
                    MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(encodedCodetext);

                    return mailmark;
                }
            }
        }
    }
}