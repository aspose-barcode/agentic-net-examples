using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation and decoding of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode, decodes it, and prints the extracted fields.
    /// </summary>
    static void Main()
    {
        // Create a sample MailmarkCodetext object with valid data
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate a Mailmark barcode image and obtain its raw bytes
        byte[] imageBytes;
        using (var ms = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to create the barcode from the MailmarkCodetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the memory stream contents to a byte array
            imageBytes = ms.ToArray();
        }

        // Decode the image bytes back to a MailmarkCodetext object
        var decoded = DecodeMailmarkFromBytes(imageBytes);
        if (decoded != null)
        {
            // Output each decoded property to the console
            Console.WriteLine($"Format: {decoded.Format}");
            Console.WriteLine($"VersionID: {decoded.VersionID}");
            Console.WriteLine($"Class: {decoded.Class}");
            Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
            Console.WriteLine($"ItemID: {decoded.ItemID}");
            Console.WriteLine($"DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
        }
        else
        {
            Console.WriteLine("Failed to decode Mailmark barcode.");
        }
    }

    /// <summary>
    /// Decodes a Mailmark barcode from raw image bytes and returns a populated <see cref="MailmarkCodetext"/> object.
    /// </summary>
    /// <param name="imageBytes">Raw bytes of the barcode image.</param>
    /// <returns>
    /// A <see cref="MailmarkCodetext"/> instance if decoding succeeds; otherwise, <c>null</c>.
    /// </returns>
    public static MailmarkCodetext DecodeMailmarkFromBytes(byte[] imageBytes)
    {
        // Validate input
        if (imageBytes == null || imageBytes.Length == 0)
            throw new ArgumentException("Image bytes are null or empty.", nameof(imageBytes));

        // Load the image bytes into a memory stream for reading
        using (var ms = new MemoryStream(imageBytes))
        {
            // Initialize a barcode reader configured for Mailmark decoding
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                // Read all barcodes found in the image
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    // Attempt to parse the raw codetext into a MailmarkCodetext object
                    var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (mailmark != null)
                        return mailmark; // Return the first successfully decoded Mailmark
                }
            }
        }

        // No Mailmark barcode could be decoded
        return null;
    }
}