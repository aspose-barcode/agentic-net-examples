// Title: Generate QR Code barcode and embed it into a PowerPoint slide
// Description: Demonstrates how to create a QR Code barcode image with Aspose.BarCode, save it as PNG, and insert the image into a new PowerPoint presentation using Aspose.Slides (Open XML).
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category, showcasing the use of BarcodeGenerator for QR Code creation and Presentation for Open XML slide manipulation. Developers often need to embed barcodes into office documents for marketing, inventory tracking, or event ticketing; this snippet illustrates the typical workflow and key API classes (BarcodeGenerator, Presentation, ISlide, IPPImage) required for such tasks.
// Prompt: Generate QR Code barcode and embed barcode into PowerPoint slide using Open XML.
// Tags: qr code, barcode generation, powerpoint, openxml, aspose.barcode, aspose.slides, image embedding, png, presentation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;

/// <summary>
/// Example program that generates a QR Code barcode image and embeds it into a PowerPoint slide.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up temporary working directory and file paths
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "qr.png");
        string presentationPath = Path.Combine(tempDir, "BarcodePresentation.pptx");

        // --------------------------------------------------------------------
        // Generate QR Code barcode and save it as a PNG image
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Optional appearance settings: pixel size and error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to the specified PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Create a new PowerPoint presentation and embed the barcode image
        // --------------------------------------------------------------------
        using (Presentation pres = new Presentation())
        {
            // Get the first (default) slide
            ISlide slide = pres.Slides[0];

            // Load the barcode image into the presentation's image collection
            using (FileStream imgStream = new FileStream(barcodePath, FileMode.Open, FileAccess.Read))
            {
                IPPImage pptImage = pres.Images.AddImage(imgStream);

                // Add a picture frame containing the barcode image at position (50,50) with size 300x300 points
                slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 50f, 50f, 300f, 300f, pptImage);
            }

            // Save the presentation to the specified PPTX file
            pres.Save(presentationPath, SaveFormat.Pptx);
        }

        // --------------------------------------------------------------------
        // Output the locations of the generated files
        // --------------------------------------------------------------------
        Console.WriteLine($"Barcode image saved to: {barcodePath}");
        Console.WriteLine($"Presentation saved to: {presentationPath}");
    }
}