// Title: Barcode padding demonstration per symbology
// Description: Shows how to apply custom padding values on each side for different barcode types using Aspose.BarCode.
// Prompt: Create a utility that applies different padding values per side for various barcode symbologies in a single workflow.
// Tags: barcode symbology, padding, aspose.barcode, image output, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates applying side‑specific padding to various barcode symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes with custom padding and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Example 1: Code128 with custom padding (left/right 5pt, top/bottom 10pt)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128_SAMPLE"))
        {
            // Set individual padding values in points
            generator.Parameters.Barcode.Padding.Left.Point   = 5f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the generated barcode image
            generator.Save("code128.png");
        }

        // Example 2: QR Code with uniform small padding (2pt on all sides)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "QR_SAMPLE"))
        {
            generator.Parameters.Barcode.Padding.Left.Point   = 2f;
            generator.Parameters.Barcode.Padding.Top.Point    = 2f;
            generator.Parameters.Barcode.Padding.Right.Point  = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

            generator.Save("qr.png");
        }

        // Example 3: DataMatrix with no padding (tight fit)
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DATAMATRIX_SAMPLE"))
        {
            generator.Parameters.Barcode.Padding.Left.Point   = 0f;
            generator.Parameters.Barcode.Padding.Top.Point    = 0f;
            generator.Parameters.Barcode.Padding.Right.Point  = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            generator.Save("datamatrix.png");
        }

        // Example 4: PDF417 with asymmetric padding (left/right 8pt, top/bottom 4pt)
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "PDF417_SAMPLE"))
        {
            generator.Parameters.Barcode.Padding.Left.Point   = 8f;
            generator.Parameters.Barcode.Padding.Top.Point    = 4f;
            generator.Parameters.Barcode.Padding.Right.Point  = 8f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 4f;

            generator.Save("pdf417.png");
        }

        // Example 5: Aztec with distinct side padding (left/right 3pt, top/bottom 6pt)
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "AZTEC_SAMPLE"))
        {
            generator.Parameters.Barcode.Padding.Left.Point   = 3f;
            generator.Parameters.Barcode.Padding.Top.Point    = 6f;
            generator.Parameters.Barcode.Padding.Right.Point  = 3f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 6f;

            generator.Save("aztec.png");
        }

        // Inform the user that generation is complete
        Console.WriteLine("Barcodes generated with custom padding.");
    }
}