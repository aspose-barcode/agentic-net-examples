// Title: Generate Australia Post barcode with Reed‑Solomon correction to memory‑mapped file
// Description: Demonstrates creating an Australia Post barcode using Aspose.BarCode with Reed‑Solomon error correction and writing the PNG image to an anonymous memory‑mapped file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and advanced output handling. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and memory‑mapped file APIs to produce a barcode image and store it in memory without touching the file system. Developers working with high‑throughput or in‑memory processing scenarios often need such patterns.
// Prompt: Generate an Australia Post barcode with Reed‑Solomon correction and output to a memory‑mapped file.
// Tags: australia post barcode, reed-solomon, memory-mapped file, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates an Australia Post barcode with Reed‑Solomon correction
/// and writes the resulting PNG image to an anonymous memory‑mapped file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it to a byte array,
    /// and stores the bytes in a memory‑mapped file.
    /// </summary>
    static void Main()
    {
        // Define the barcode content: Australia Post format (FCC 62, 8‑digit sorting code, 5‑character customer info)
        string codeText = "6201234567ABCD";

        // Generate the barcode image and capture it in a byte array
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Set the encoding table for customer information (CTable) and enable Reed‑Solomon correction implicitly
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode as PNG into a memory stream, then extract the byte array
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray();
            }
        }

        // Create an anonymous memory‑mapped file sized to hold the image bytes
        using (var mmf = MemoryMappedFile.CreateNew(null, imageBytes.Length))
        {
            // Obtain a view accessor and write the image bytes into the memory‑mapped region
            using (var accessor = mmf.CreateViewAccessor())
            {
                accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
            }
        }

        Console.WriteLine("Australia Post barcode generated and written to memory‑mapped file successfully.");
    }
}