// Title: Generate RM4SCC Barcode with Custom XDimension and Export to Byte Array
// Description: Demonstrates creating an RM4SCC barcode, adjusting its X‑dimension, rendering it to a PNG image, and retrieving the image as a byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters (such as XDimension) using the BarcodeGenerator class, render the barcode to a bitmap, and obtain the image data in memory. Typical use cases include generating barcodes for shipping labels, invoices, or inventory systems where the image must be transmitted or stored without writing to disk. Developers often need to customize size, format, and retrieve the result as a byte array for further processing or API calls.
// Prompt: Create a barcode generator instance for RM4SCC, set custom XDimension, and export image to a byte array.
// Tags: rm4scc,barcode,generation,xdimension,bytearray,png,aspose.barcode,aspnet,imaging

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an RM4SCC barcode with a custom XDimension,
/// renders it to a PNG bitmap, and extracts the image as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the byte array length to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // The text to encode in the barcode.
        string codeText = "123456ASPOSE";

        // Desired XDimension (module width) in points.
        float customXDimension = 3.5f;

        // Initialize the barcode generator for RM4SCC symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Apply the custom XDimension to the barcode parameters.
            generator.Parameters.Barcode.XDimension.Point = customXDimension;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);

                    // Convert the memory stream to a byte array.
                    byte[] barcodeBytes = memoryStream.ToArray();

                    // Output the size of the resulting byte array.
                    Console.WriteLine($"Barcode byte array length: {barcodeBytes.Length}");
                }
            }
        }
    }
}