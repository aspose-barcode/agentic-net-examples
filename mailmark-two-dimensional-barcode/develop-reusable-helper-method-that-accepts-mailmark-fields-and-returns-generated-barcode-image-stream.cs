// Title: Generate Mailmark barcode and return image stream
// Description: Demonstrates creating a Mailmark barcode using Aspose.BarCode and returning it as a MemoryStream for further processing or saving.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark symbology. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to encode required fields, a common task for developers integrating postal barcode solutions into .NET applications.
// Prompt: Develop a reusable helper method that accepts Mailmark fields and returns a generated barcode image stream.
// Tags: mailmark, barcode, generation, stream, aspose.barcode, complexbarcodegenerator, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides an example of generating a Mailmark barcode and saving it as an image file.
/// </summary>
class Program
{
    // Helper method that creates a Mailmark barcode and returns the image as a MemoryStream.
    // Parameters correspond to the required Mailmark fields.
    static MemoryStream GenerateMailmarkBarcode(int format, int versionId, string @class,
        int supplychainId, int itemId, string destinationPostCodePlusDps)
    {
        // Validate required string parameters.
        if (string.IsNullOrWhiteSpace(@class))
            throw new ArgumentException("Class cannot be null or empty.", nameof(@class));
        if (string.IsNullOrWhiteSpace(destinationPostCodePlusDps))
            throw new ArgumentException("DestinationPostCodePlusDPS cannot be null or empty.", nameof(destinationPostCodePlusDps));

        // Populate the MailmarkCodetext object with the mandatory fields.
        var mailmark = new MailmarkCodetext
        {
            Format = format,                     // 1 = Letter, 2 = Large Letter, 4 = unspecified/default
            VersionID = versionId,               // typically 1
            Class = @class,                      // e.g., "0"
            SupplychainID = supplychainId,       // up to 999999
            ItemID = itemId,                     // up to 99999999
            DestinationPostCodePlusDPS = destinationPostCodePlusDps // e.g., "EF61AH8T "
        };

        // Generate the barcode image into a memory stream.
        var stream = new MemoryStream();
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(stream, BarCodeImageFormat.Png);
        }

        // Reset stream position for callers.
        stream.Position = 0;
        return stream;
    }

    /// <summary>
    /// Entry point demonstrating the GenerateMailmarkBarcode helper and saving the result to a file.
    /// </summary>
    static void Main()
    {
        // Sample data based on the documented valid example.
        int format = 4;                     // unspecified/default (4-state)
        int versionId = 1;
        string @class = "0";
        int supplychainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T ";

        // Generate the barcode.
        using (MemoryStream barcodeStream = GenerateMailmarkBarcode(format, versionId, @class,
                                                                   supplychainId, itemId, destinationPostCodePlusDps))
        {
            // For demonstration, write the stream length and optionally save to a file.
            Console.WriteLine($"Generated Mailmark barcode image size: {barcodeStream.Length} bytes");

            // Save to a file named "mailmark.png" in the current directory.
            using (FileStream file = new FileStream("mailmark.png", FileMode.Create, FileAccess.Write))
            {
                barcodeStream.CopyTo(file);
            }

            Console.WriteLine("Barcode image saved as 'mailmark.png'.");
        }
    }
}