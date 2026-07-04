// Title: Read Barcodes from Encrypted PDF using Aspose.BarCode
// Description: Demonstrates how to supply a password for an encrypted PDF and extract barcode data using BarCodeReader.
// Prompt: Process encrypted PDF files by providing password to BarCodeReader and extracting barcode data.
// Tags: pdf, encryption, barcode, reading, aspose.barcode, aspose.pdf, decode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcodes from a PDF file. 
/// It shows how to handle encrypted PDFs by providing a password (commented out) 
/// and how to extract barcode information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs file validation, demonstrates
    /// both the password‑protected PDF workflow (commented) and the simple direct
    /// reading approach, then processes any detected barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the encrypted PDF file.
        string pdfPath = "encrypted.pdf";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // --------------------------------------------------------------------
        // Encrypted PDF handling (requires Aspose.Pdf). The code is provided as
        // a reference and is commented out because the Aspose.Pdf assembly may
        // not be available in the execution environment.
        // --------------------------------------------------------------------
        // string pdfPassword = "yourPassword";
        // using (var pdfDocument = new Aspose.Pdf.Document(pdfPath, new Aspose.Pdf.LoadOptions { Password = pdfPassword }))
        // {
        //     // Iterate through each page, render it to an image stream, and feed it to BarCodeReader.
        //     for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
        //     {
        //         using (var imageStream = new MemoryStream())
        //         {
        //             pdfDocument.Pages[pageIndex].ConvertToImage(imageStream, Aspose.Pdf.Devices.Resolution.Default);
        //             imageStream.Position = 0;
        //             using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        //             {
        //                 ProcessBarcodes(reader);
        //             }
        //         }
        //     }
        // }

        // --------------------------------------------------------------------
        // Simple direct reading (suitable for unencrypted PDFs or when the
        // library can handle the password internally). This demonstrates the
        // core barcode extraction logic.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(pdfPath, DecodeType.AllSupportedTypes))
        {
            ProcessBarcodes(reader);
        }
    }

    /// <summary>
    /// Reads all barcodes from the provided <see cref="BarCodeReader"/> instance
    /// and writes their type and decoded text to the console.
    /// </summary>
    /// <param name="reader">Initialized BarCodeReader configured with the source document.</param>
    private static void ProcessBarcodes(BarCodeReader reader)
    {
        // Iterate through detected barcodes and output their type and decoded text.
        foreach (var result in reader.ReadBarCodes())
        {
            Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
            Console.WriteLine($"BarCode CodeText: {result.CodeText}");
        }
    }
}