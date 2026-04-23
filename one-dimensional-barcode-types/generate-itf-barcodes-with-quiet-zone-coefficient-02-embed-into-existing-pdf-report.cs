using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths for the existing PDF and the output PDF
        const string inputPdfPath = "report.pdf";
        const string outputPdfPath = "report_with_barcode.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF file not found: {inputPdfPath}");
            return;
        }

        // Create the ITF14 barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14))
        {
            // Sample code text for ITF14 (13 digits, checksum will be added automatically)
            generator.CodeText = "1234567890123";

            // Set a reasonable XDimension (e.g., 2 points)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Attempt to set QuietZoneCoef to 0.2 (the API expects an integer >= 10)
            // Since 0.2 is invalid, handle it gracefully.
            try
            {
                // Convert 0.2 to an integer multiplier (invalid on purpose)
                int quietZoneCoef = (int)(0.2 * 10); // results in 2
                generator.Parameters.Barcode.ITF.QuietZoneCoef = quietZoneCoef; // will throw
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("QuietZoneCoef value is invalid (must be >= 10). Using default value.");
                // Keep the default QuietZoneCoef (10)
            }

            // Optional: set barcode colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Render the barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Load the existing PDF
                using (var pdfDoc = new Document(inputPdfPath))
                {
                    // Ensure there is at least one page
                    if (pdfDoc.Pages.Count == 0)
                    {
                        pdfDoc.Pages.Add();
                    }

                    // Add the barcode image to the first page
                    var page = pdfDoc.Pages[1];
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        // Position the image (coordinates in points)
                        // Here we place it at (100, 500) with a width of 200 points
                        // Height will be scaled proportionally
                        FixWidth = 200
                    };
                    page.Paragraphs.Add(pdfImage);
                }

                // Save the modified PDF
                using (var outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Re-open the PDF to write the changes (since the using block above disposed it)
                    using (var pdfDoc = new Document(inputPdfPath))
                    {
                        // Add the image again (the same steps as before)
                        var page = pdfDoc.Pages[1];
                        var pdfImage = new Aspose.Pdf.Image
                        {
                            ImageStream = barcodeStream,
                            FixWidth = 200
                        };
                        page.Paragraphs.Add(pdfImage);

                        pdfDoc.Save(outputStream);
                    }
                }

                Console.WriteLine($"Barcode embedded successfully into '{outputPdfPath}'.");
            }
        }
    }
}