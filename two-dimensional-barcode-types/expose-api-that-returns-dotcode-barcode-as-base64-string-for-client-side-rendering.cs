// Title: Generate DotCode barcode and return as Base64 string
// Description: Demonstrates creating a DotCode barcode with Aspose.BarCode, encoding it to PNG, and converting the image to a Base64 string for client‑side rendering.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DotCode. Typical use cases include generating machine‑readable symbols for inventory, tracking, or mobile scanning, where developers need to deliver the barcode image to web clients without writing files to disk. The snippet shows configuring ECI encoding, rendering to a Bitmap, and converting the result to a Base64 string—common steps for API‑driven barcode services.
// Prompt: Expose an API that returns DotCode barcode as base64 string for client‑side rendering.
// Tags: dotcode, barcode, generation, base64, aspose.barcode, image, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console entry point that generates a DotCode barcode and outputs its Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DotCode barcode, encodes it as PNG, converts the image to a Base64 string, and writes the string to the console.
    /// </summary>
    static void Main()
    {
        // Sample codetext for DotCode barcode
        const string codeText = "Sample DotCode";

        // Initialize a barcode generator for DotCode with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Optional: set ECI (Extended Channel Interpretation) encoding to UTF‑8 for Unicode support
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

            // Generate the barcode image as a Bitmap object
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare a memory stream to hold the PNG data
                using (var ms = new MemoryStream())
                {
                    // Save the bitmap into the memory stream in PNG format
                    bitmap.Save(ms, ImageFormat.Png);

                    // Retrieve the raw image bytes from the stream
                    byte[] imageBytes = ms.ToArray();

                    // Convert the image bytes to a Base64-encoded string
                    string base64 = Convert.ToBase64String(imageBytes);

                    // Output the Base64 string; it can be embedded directly in HTML <img> tags on the client side
                    Console.WriteLine(base64);
                }
            }
        }
    }
}