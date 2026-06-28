using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating ITF-14 barcodes with different border styles,
/// embedding them into a PDF, and saving the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, adds them to a PDF document, and saves the file.
    /// </summary>
    static void Main()
    {
        // Output PDF file name
        string pdfPath = "ITF_Barcodes.pdf";

        // Text to encode in the barcode
        string codeText = "12345678901231";

        // Define the set of border styles to apply to the ITF-14 barcode
        ITF14BorderType[] borderStyles = new ITF14BorderType[]
        {
            ITF14BorderType.Frame,
            ITF14BorderType.Bar,
            ITF14BorderType.FrameOut,
            ITF14BorderType.BarOut
        };

        // Collection to hold generated image streams for later disposal
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Iterate over each border style, generate a barcode, and add it to the PDF
            for (int i = 0; i < borderStyles.Length; i++)
            {
                // Initialize barcode generator with ITF-14 type and the specified text
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
                {
                    // Disable automatic sizing; set explicit dimensions
                    generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                    generator.Parameters.Barcode.BarHeight.Point = 40f;

                    // Apply the current border style and thickness
                    generator.Parameters.Barcode.ITF.BorderType = borderStyles[i];
                    generator.Parameters.Barcode.ITF.BorderThickness.Point = 2f;

                    // Save the generated barcode image to a memory stream (PNG format)
                    MemoryStream ms = new MemoryStream();
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Keep the stream for later disposal
                    imageStreams.Add(ms);

                    // Add a new page to the PDF and place the barcode image on it
                    Page page = pdfDoc.Pages.Add();
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image { ImageStream = ms };
                    page.Paragraphs.Add(pdfImage);
                }
            }

            // Save the assembled PDF document to disk
            pdfDoc.Save(pdfPath);
        }

        // Dispose all memory streams now that the PDF has been saved
        foreach (var stream in imageStreams)
        {
            stream.Dispose();
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF with ITF14 barcodes saved to: {pdfPath}");
    }
}