// Title: Generate QR Code and embed into PowerPoint slide
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as EMF, and inserting it into a PowerPoint presentation using Aspose.Slides and Open XML.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category, showing how to generate vector barcode images (QR Code) and embed them into Office Open XML documents such as PowerPoint. It highlights key API classes like BarcodeGenerator, BarCodeImageFormat, Presentation, and ImageCollection, which developers commonly use for automated report generation, marketing material creation, and document automation.
// Prompt: Generate QR Code barcode and embed barcode into PowerPoint slide using Open XML.
// Tags: qr code, barcode generation, powerpoint, openxml, aspose.barcode, aspose.slides, emf, vector image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as an EMF image,
/// and embeds the image into a PowerPoint slide using Aspose.Slides.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Paths for temporary barcode image and final PowerPoint file
        const string barcodePath = "qr.emf";
        const string presentationPath = "qr_presentation.pptx";

        // -----------------------------------------------------------------
        // 1. Generate QR Code barcode and save it as EMF (vector format)
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level for better readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Let the generator decide size based on the image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save as EMF; handle evaluation‑version restriction gracefully
            try
            {
                generator.Save(barcodePath, BarCodeImageFormat.Emf);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("A valid Aspose.BarCode license is required for EMF export of this barcode type.");
                    return;
                }
                throw;
            }
        }

        // Verify that the EMF file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // ---------------------------------------------------------------
        // 2. Create a PowerPoint presentation and embed the barcode image
        // ---------------------------------------------------------------
        using (var presentation = new Presentation())
        {
            // Use the first (default) slide
            var slide = presentation.Slides[0];

            // Load the EMF image bytes
            byte[] emfBytes = File.ReadAllBytes(barcodePath);

            // Add the image to the presentation's image collection
            var image = presentation.Images.AddImage(emfBytes);

            // Insert the image as a picture frame onto the slide
            // Parameters: shape type, X, Y, width, height, image
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 50f, 50f, 300f, 300f, image);

            // Save the presentation to a PPTX file
            presentation.Save(presentationPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Presentation created successfully: {presentationPath}");
    }
}