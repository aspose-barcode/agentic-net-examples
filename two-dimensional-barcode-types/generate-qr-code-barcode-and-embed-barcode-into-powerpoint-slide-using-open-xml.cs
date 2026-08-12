// Title: Generate QR Code barcode and embed into PowerPoint slide
// Description: Demonstrates creating a QR Code barcode, saving it as an EMF image, and inserting the image into a PowerPoint presentation.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category. It showcases how to use the BarcodeGenerator class to produce a QR Code, the Presentation class to work with PowerPoint files via Open XML, and the IPPImage interface to embed images. Typical use cases include adding dynamic barcodes to slide decks for marketing, inventory, or event tickets. Developers often need to generate barcodes programmatically and place them into Office documents without manual steps.
// Prompt: Generate QR Code barcode and embed barcode into PowerPoint slide using Open XML.
// Tags: qr code, barcode generation, powerpoint, openxml, aspose.barcode, aspose.slides, emf, image embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode, saves it as an EMF image,
/// and embeds the image into a PowerPoint slide using Aspose.BarCode and Aspose.Slides.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for all generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Path for the EMF barcode image
        string emfPath = Path.Combine(workFolder, "qr.emf");

        // Generate a QR Code and save it as EMF
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            try
            {
                // Export the barcode to an EMF file
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
            catch (Exception ex)
            {
                // EMF export requires a licensed version of Aspose.BarCode
                if (ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("A valid Aspose.BarCode license is required for EMF export.");
                    return;
                }
                throw;
            }
        }

        // Verify that the EMF file was created
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("Failed to create the EMF barcode image.");
            return;
        }

        // Create a new PowerPoint presentation and embed the barcode image
        using (var presentation = new Presentation())
        {
            // Load the EMF image into the presentation's image collection
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            IPPImage pptImage = presentation.Images.AddImage(emfBytes);

            // Add a picture frame to the first slide (position and size are in points)
            var slide = presentation.Slides[0];
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 50, 50, 400, 400, pptImage);

            // Save the presentation to the temporary folder
            string pptxPath = Path.Combine(workFolder, "BarcodePresentation.pptx");
            presentation.Save(pptxPath, SaveFormat.Pptx);
            Console.WriteLine($"Presentation saved to: {pptxPath}");
        }
    }
}