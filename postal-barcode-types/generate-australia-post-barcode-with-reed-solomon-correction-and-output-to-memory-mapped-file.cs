using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation; // for BarCodeImageFormat
using Aspose.BarCode; // for CustomerInformationInterpretingType

/// <summary>
/// Demonstrates generating an Australia Post barcode and storing it in a memory‑mapped file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample Australia Post barcode text. Adjust as needed.
        string codeText = "5912345678ABCde";

        // Create the barcode generator for Australia Post.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the customer information interpreting type (optional).
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Australia Post barcode uses Reed‑Solomon error correction internally.
            // No explicit property is required; the correction is applied automatically.

            // Save the barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Create or open a memory‑mapped file with the exact size of the image data.
                using (var mmf = MemoryMappedFile.CreateOrOpen("AustraliaPostBarcode", imageBytes.Length))
                {
                    // Write the image bytes into the memory‑mapped file.
                    using (var accessor = mmf.CreateViewAccessor(0, imageBytes.Length, MemoryMappedFileAccess.Write))
                    {
                        accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
                    }
                }

                Console.WriteLine("Australia Post barcode generated and written to memory‑mapped file.");
            }
        }
    }
}