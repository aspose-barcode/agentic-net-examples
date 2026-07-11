// Title: Generate Australia Post barcode with Reed‑Solomon correction to a memory‑mapped file
// Description: Demonstrates creating an Australia Post barcode using Reed‑Solomon error correction and saving the PNG image into a memory‑mapped file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters (EncodeTypes, AustralianPostEncodingTable) and output the result to a non‑file storage medium. Developers working with barcode creation, custom encoding tables, and advanced storage options can use this pattern as a reference.
// Prompt: Generate an Australia Post barcode with Reed‑Solomon correction and output to a memory‑mapped file.
// Tags: australia post, barcode generation, reed-solomon, memory-mapped file, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post barcode with Reed‑Solomon correction
/// and stores the resulting PNG image in a memory‑mapped file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, writes it to a memory‑mapped file,
    /// and outputs a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode text (customer information + routing code)
        const string codeText = "5912345678ABCD";

        // Initialize the barcode generator for the Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the encoding table to CTable for the customer information part
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Render the barcode to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Define the name of the memory‑mapped file
                const string mapName = "AustraliaPostBarcodeMap";

                // Create (or open) the memory‑mapped file with the exact size of the image data
                using (var mmf = MemoryMappedFile.CreateOrOpen(mapName, imageBytes.Length, MemoryMappedFileAccess.ReadWrite))
                {
                    // Create a view accessor for writing the image bytes
                    using (var accessor = mmf.CreateViewAccessor(0, imageBytes.Length, MemoryMappedFileAccess.Write))
                    {
                        accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
                    }
                }

                // Inform the user that the operation succeeded
                Console.WriteLine($"Australia Post barcode generated ({imageBytes.Length} bytes) and stored in memory‑mapped file \"{mapName}\".");
            }
        }
    }
}