// Title: Export Barcode to EMF and Embed in PowerPoint
// Description: Generates a Code128 barcode, saves it as an EMF vector image, and inserts the image into a PowerPoint slide.
// Prompt: Export a barcode as an EMF vector file and import it into a PowerPoint slide programmatically.
// Tags: barcode, code128, export, emf, powerpoint, aspose.barcode, aspose.slides

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Slides;
using Aspose.Slides.Export;

/// <summary>
/// Demonstrates how to generate a barcode, export it as an EMF vector file,
/// and embed the resulting image into a PowerPoint presentation using Aspose APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it as EMF,
    /// creates a PowerPoint slide, and inserts the EMF image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the intermediate EMF image and the final PPTX file
        string emfPath = "barcode.emf";
        string pptxPath = "barcode_presentation.pptx";

        // -----------------------------------------------------------------
        // 1. Generate a barcode and save it as an EMF vector image
        // -----------------------------------------------------------------
        try
        {
            // Initialize the barcode generator with Code128 symbology and sample data
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Let the generator automatically determine the optimal size using interpolation
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Export the barcode to an EMF file
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
        }
        catch (Exception ex)
        {
            // Provide a clear message if the evaluation version blocks EMF export
            if (ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("A valid Aspose.BarCode license is required for EMF export of this barcode type.");
                return;
            }

            // Re‑throw unexpected exceptions
            throw;
        }

        // -----------------------------------------------------------------
        // 2. Create a PowerPoint presentation and insert the EMF image
        // -----------------------------------------------------------------
        using (Presentation pres = new Presentation())
        {
            // The newly created presentation contains a single default slide
            var slide = pres.Slides[0];

            // Read the EMF file into a byte array and add it to the presentation's image collection
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            IPPImage emfImage = pres.Images.AddImage(emfBytes);

            // Insert the EMF image onto the slide as a picture frame
            // Position (0,0) and size (400x300) are arbitrary and can be adjusted as needed
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 0, 0, 400, 300, emfImage);

            // Save the populated presentation to a PPTX file
            pres.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine("Barcode exported as EMF and embedded into PowerPoint slide successfully.");
    }
}