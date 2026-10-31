// Title: Save PDF417 barcode as EMF and embed in Word document
// Description: Demonstrates generating a PDF417 barcode, exporting it to an EMF vector file, and inserting the image into a Word document using Aspose APIs.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Words integration category, showcasing how to combine barcode generation (BarcodeGenerator, EncodeTypes) with document creation (Document, DocumentBuilder). Typical use cases include embedding high‑resolution barcodes in reports, invoices, or labels where vector graphics are required. Developers often need to export barcodes to vector formats (EMF, SVG) and place them precisely within Word or PDF files.
// Prompt: Save a PDF417 barcode as an EMF vector file and embed it into a Word document.
// Tags: pdf417, barcode, export, emf, vector, word, aspose.barcode, aspose.words, document generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the PDF417 barcode to EMF and Word embedding example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a PDF417 barcode, saves it as an EMF file, and inserts it into a Word document.
    /// </summary>
    static void Main()
    {
        // Define barcode generation parameters
        const int resolution = 300;          // DPI for the generated barcode image
        const int leftPosition = 10;         // Horizontal offset (points) for the image in the Word document
        const int topPosition = 20;          // Vertical offset (points) for the image in the Word document
        string barcodeText = "Aspose.Barcode Pdf417 Example";

        // Determine file system paths for the EMF image and the resulting Word document
        string emfPath = Path.Combine(Directory.GetCurrentDirectory(), "Pdf417Barcode.emf");
        string docPath = Path.Combine(Directory.GetCurrentDirectory(), "Pdf417BarcodeDoc.docx");

        // Generate PDF417 barcode and save it as an EMF vector image
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, barcodeText))
        {
            generator.Parameters.Resolution = resolution;

            // Generate a bitmap first to obtain the image dimensions
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var emfStream = new MemoryStream())
                {
                    try
                    {
                        // Export the barcode directly to EMF format
                        generator.Save(emfStream, BarCodeImageFormat.Emf);
                    }
                    catch (Exception ex)
                    {
                        // EMF export requires a licensed version of Aspose.BarCode
                        if (ex.Message.Contains("evaluation"))
                        {
                            Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                            return;
                        }
                        throw;
                    }

                    // Optionally write the EMF file to disk for inspection
                    File.WriteAllBytes(emfPath, emfStream.ToArray());

                    // Create a new Word document and prepare to insert the EMF image
                    Document wordDoc = new Document();
                    DocumentBuilder builder = new DocumentBuilder(wordDoc);

                    // Convert bitmap dimensions from pixels to points (1 point = 1/72 inch)
                    float widthPoints = (bitmap.Width * 72f) / resolution;
                    float heightPoints = (bitmap.Height * 72f) / resolution;

                    // Add content before the barcode image
                    builder.Write("First Sentence.");

                    // Insert the EMF image at the specified position with square text wrapping
                    builder.InsertImage(
                        emfStream.ToArray(),
                        RelativeHorizontalPosition.Page,
                        leftPosition,
                        RelativeVerticalPosition.Page,
                        topPosition,
                        widthPoints,
                        heightPoints,
                        WrapType.Square);

                    // Add content after the barcode image
                    builder.Write("Second Sentence.");

                    // Save the Word document to the file system
                    wordDoc.Save(docPath, SaveFormat.Docx);
                }
            }
        }

        // Inform the user where the output files are located
        Console.WriteLine($"EMF file saved to: {emfPath}");
        Console.WriteLine($"Word document saved to: {docPath}");
    }
}