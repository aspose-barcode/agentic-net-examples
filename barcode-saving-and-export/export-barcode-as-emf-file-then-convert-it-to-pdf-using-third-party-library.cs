using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define file paths
        string emfPath = "barcode.emf";
        string pdfPath = "barcode.pdf";

        // Create a barcode and export it as EMF
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Optional: set resolution or other parameters here if needed
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
        }
        catch (Exception ex)
        {
            // Evaluation version of Aspose.BarCode allows only Code39 to be saved as EMF
            if (ex.Message != null && ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("EMF export requires a valid Aspose.BarCode license. Please apply a license before using this feature.");
                return;
            }
            // Re‑throw unexpected exceptions
            throw;
        }

        // Verify that the EMF file was created
        if (!File.Exists(emfPath))
        {
            Console.WriteLine($"Failed to create EMF file at '{emfPath}'.");
            return;
        }

        // Convert the EMF file to PDF using Aspose.Pdf
        using (var pdfDoc = new Document())
        {
            // Add a single page (evaluation mode restriction: max 4 elements)
            var page = pdfDoc.Pages.Add();

            // Load EMF bytes into a memory stream
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            using (var ms = new MemoryStream(emfBytes))
            {
                // Create an image object from the EMF stream
                var image = new Image
                {
                    ImageStream = ms
                };

                // Add the image to the page
                page.Paragraphs.Add(image);
            }

            // Save the PDF document
            pdfDoc.Save(pdfPath);
        }

        // Verify that the PDF file was created
        if (File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file successfully created at '{pdfPath}'.");
        }
        else
        {
            Console.WriteLine("Failed to create PDF file.");
        }
    }
}