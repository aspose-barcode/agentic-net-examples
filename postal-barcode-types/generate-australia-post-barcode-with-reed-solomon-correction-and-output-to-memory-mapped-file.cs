// Title: Generate Australia Post barcode with Reed‑Solomon correction to a memory‑mapped file
// Description: Demonstrates creating an Australia Post barcode using Reed‑Solomon error correction and saving the PNG image into an anonymous memory‑mapped file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters, apply Reed‑Solomon correction, and output the result to a non‑file storage medium. It showcases the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, which developers commonly use for custom barcode creation, format selection, and advanced error‑correction settings. Ideal for scenarios where barcodes must be generated on‑the‑fly and shared via memory‑mapped files or other in‑memory transports.
// Prompt: Generate an Australia Post barcode with Reed‑Solomon correction and output to a memory‑mapped file.
// Tags: barcode symbology, australia post, reed-solomon, memory-mapped file, png, aspose.barcode, generation

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates an Australia Post barcode with Reed‑Solomon correction
/// and stores the resulting PNG image in an anonymous memory‑mapped file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, writes it to a memory stream,
    /// then copies the image bytes into a newly created memory‑mapped file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text: FCC 62, 8‑digit sorting code, 2‑character customer info.
        string codeText = "6201234567AB";

        // Initialize the barcode generator for Australia Post symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set visual parameters: module size and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Configure Reed‑Solomon error correction via the encoding table.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Render the barcode into a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Create an anonymous memory‑mapped file sized to hold the image bytes.
                using (var mmf = MemoryMappedFile.CreateNew(null, imageBytes.Length))
                {
                    // Obtain a view accessor and write the image data into the memory‑mapped file.
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
                    }
                }
            }
        }

        Console.WriteLine("Australia Post barcode generated and stored in a memory‑mapped file.");
    }
}