using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

namespace MailmarkDecoder
{
    class Program
    {
        static void Main()
        {
            // Example: attempt to decode from a sample byte array (empty in this demo)
            byte[] sampleImageBytes = new byte[0];
            try
            {
                MailmarkCodetext result = DecodeMailmarkFromBytes(sampleImageBytes);
                if (result != null)
                {
                    Console.WriteLine("Decoded Mailmark:");
                    Console.WriteLine($"Format: {result.Format}");
                    Console.WriteLine($"VersionID: {result.VersionID}");
                    Console.WriteLine($"Class: {result.Class}");
                    Console.WriteLine($"SupplychainID: {result.SupplychainID}");
                    Console.WriteLine($"ItemID: {result.ItemID}");
                    Console.WriteLine($"DestinationPostCodePlusDPS: '{result.DestinationPostCodePlusDPS}'");
                }
                else
                {
                    Console.WriteLine("No Mailmark barcode could be decoded from the provided image bytes.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
        }

        /// <summary>
        /// Decodes a Mailmark barcode from raw image bytes and returns a populated MailmarkCodetext object.
        /// </summary>
        /// <param name="imageBytes">Raw image data containing the barcode.</param>
        /// <returns>MailmarkCodetext if decoding succeeds; otherwise, null.</returns>
        public static MailmarkCodetext DecodeMailmarkFromBytes(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                throw new ArgumentException("Image bytes must not be null or empty.", nameof(imageBytes));

            using (var stream = new MemoryStream(imageBytes))
            {
                // Use AllSupportedTypes as per guidelines
                using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
                {
                    // Read all detected barcodes
                    var barcodes = reader.ReadBarCodes();
                    foreach (var result in barcodes)
                    {
                        if (string.IsNullOrEmpty(result.CodeText))
                            continue;

                        // Attempt to decode as Mailmark codetext
                        MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (mailmark != null)
                            return mailmark;
                    }
                }
            }

            // No Mailmark barcode found
            return null;
        }
    }
}