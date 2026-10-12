// Title: Generate Australia Post barcode with Reed‑Solomon correction to a memory‑mapped file
// Description: Demonstrates creating an Australia Post barcode using Reed‑Solomon error correction and saving the PNG image into a memory‑mapped file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters (EncodeTypes, XDimension, BarHeight, EncodingTable) and persist the resulting image using .NET memory‑mapped files. Developers working with postal symbologies, error‑correction schemes, or high‑performance in‑memory storage will find similar patterns useful across a collection of barcode generation samples.
// Prompt: Generate an Australia Post barcode with Reed‑Solomon correction and output to a memory‑mapped file.
// Tags: australia post barcode, reed-solomon, memory-mapped file, barcode generation, aspose.barcode, png

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates an Australia Post barcode with Reed‑Solomon correction
/// and writes the resulting PNG image to a memory‑mapped file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it to a stream,
    /// copies the bytes into a memory‑mapped file, and reports the operation size.
    /// </summary>
    static void Main()
    {
        // Define the barcode content: FCC 59, DPID and numeric customer info (NTable)
        string codeText = "59012345670123";

        // Initialize the barcode generator for Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;          // Width of the smallest bar
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;        // Height of the barcode
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable; // Use NTable encoding

            // Render the barcode into a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Create a new memory‑mapped file sized to hold the image bytes
                using (var mmf = MemoryMappedFile.CreateNew(null, imageBytes.Length))
                {
                    // Write the image bytes into the memory‑mapped file
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
                    }

                    // Inform the user about the successful operation
                    Console.WriteLine($"Australia Post barcode generated and written to memory-mapped file (size: {imageBytes.Length} bytes).");
                }
            }
        }
    }
}