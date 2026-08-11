// Title: Generate Australia Post barcode with Reed‑Solomon correction to a memory‑mapped file
// Description: Demonstrates creating an Australia Post barcode using Reed‑Solomon error correction and saving the PNG image into an anonymous memory‑mapped file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters (such as encoding tables and X‑dimension), generate a bitmap image, and write the result to a memory‑mapped file. Key API classes include BarcodeGenerator, BarcodeParameters, Bitmap, MemoryMappedFile, and related accessor classes. Developers often need to produce barcodes for printing or embedding in applications while handling the image data in memory for further processing or inter‑process communication.
// Prompt: Generate an Australia Post barcode with Reed‑Solomon correction and output to a memory‑mapped file.
// Tags: australia post, barcode generation, memory-mapped file, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post barcode with Reed‑Solomon correction
/// and stores the PNG image in an anonymous memory‑mapped file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, saves it to a memory stream, and writes the bytes to a memory‑mapped file.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post code text (FCC 59 with 2 CTable characters)
        const string codeText = "5980123456AB";

        // Initialize the barcode generator for Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Use CTable encoding for the optional customer information part
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Optional: adjust module size (X‑dimension) for better readability
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Create an anonymous memory‑mapped file sized to hold the image bytes
                    using (var mmf = MemoryMappedFile.CreateNew(null, imageBytes.Length))
                    {
                        // Write the image bytes into the memory‑mapped file
                        using (var accessor = mmf.CreateViewAccessor())
                        {
                            accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
                        }

                        Console.WriteLine($"Australia Post barcode generated ({imageBytes.Length} bytes) and stored in a memory‑mapped file.");
                    }
                }
            }
        }
    }
}