using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as an EMF file,
/// and converting that EMF to a PDF using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode, converts it to PDF,
    /// and outputs the result paths or error messages.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths for the intermediate EMF and final PDF.
        string emfPath = Path.Combine(Path.GetTempPath(), "barcode.emf");
        string pdfPath = Path.Combine(Path.GetTempPath(), "barcode.pdf");

        // ------------------------------------------------------------
        // Generate a Code128 barcode and save it directly as an EMF file.
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode image in EMF format.
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate EMF barcode: {ex.Message}");
            return;
        }

        // Verify that the EMF file was successfully created.
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("EMF file was not created.");
            return;
        }

        // ------------------------------------------------------------
        // Convert the EMF file to a PDF document using Aspose.Pdf.
        // ------------------------------------------------------------
        try
        {
            // Aspose.Pdf.Document does NOT implement IDisposable (rule 119).
            var pdfDoc = new Document();

            // Add a new page to the PDF document.
            var page = pdfDoc.Pages.Add();

            // Open the EMF file as a read-only stream.
            // The stream must remain open until after the PDF is saved.
            var imageStream = new FileStream(emfPath, FileMode.Open, FileAccess.Read);

            // Create an Aspose.Pdf.Image object from the EMF stream.
            var pdfImage = new Image { ImageStream = imageStream };

            // Insert the image into the page's paragraph collection.
            page.Paragraphs.Add(pdfImage);

            // Save the PDF document to the specified path.
            pdfDoc.Save(pdfPath);

            // Dispose the image stream now that the PDF has been saved.
            imageStream.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to convert EMF to PDF: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Report the outcome of the PDF creation.
        // ------------------------------------------------------------
        if (File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF successfully created at: {pdfPath}");
        }
        else
        {
            Console.WriteLine("PDF file was not created.");
        }
    }
}