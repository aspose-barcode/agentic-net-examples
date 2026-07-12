// Title: Generate Australia Post barcode with Reed‑Solomon correction to a memory‑mapped file
// Description: Demonstrates creating an Australia Post barcode using Reed‑Solomon error correction and storing the PNG image in an anonymous memory‑mapped file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters (e.g., encoding table) and output the result to non‑file storage. It uses BarcodeGenerator, EncodeTypes, and MemoryMappedFile classes, common tasks for developers needing in‑memory barcode handling for web services or high‑performance pipelines.
// Prompt: Generate an Australia Post barcode with Reed‑Solomon correction and output to a memory‑mapped file.
// Tags: barcode symbology, generation, png, memory-mapped file, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates an Australia Post barcode with Reed‑Solomon correction
/// and writes the resulting PNG image to an anonymous memory‑mapped file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and stores it in memory.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post code text (FCC 59 with 2 CTable characters)
        string codeText = "5980123456AB";

        // Initialize the barcode generator for the AustraliaPost symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure the encoding table to use the CTable customer information type
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Reed‑Solomon correction is applied automatically for AustraliaPost barcodes.
            // No explicit property needs to be set.

            // Generate the barcode image (default format is PNG) and write it to a memory stream
            using (var image = generator.GenerateBarCodeImage())
            {
                using (var ms = new MemoryStream())
                {
                    // Save the image into the memory stream using PNG encoding
                    image.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Create an anonymous memory‑mapped file sized to hold the image bytes
                    using (var mmf = MemoryMappedFile.CreateNew(null, imageBytes.Length))
                    {
                        // Obtain a view accessor to write the byte array into the memory‑mapped file
                        using (var accessor = mmf.CreateViewAccessor())
                        {
                            accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
                        }

                        // Inform the user that the operation completed successfully
                        Console.WriteLine("Australia Post barcode generated and stored in a memory‑mapped file.");
                    }
                }
            }
        }
    }
}