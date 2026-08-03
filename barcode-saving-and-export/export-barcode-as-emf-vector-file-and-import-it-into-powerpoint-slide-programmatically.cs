// Title: Export barcode to EMF and embed in PowerPoint
// Description: Demonstrates generating a Code128 barcode, saving it as an EMF vector file, and programmatically inserting it into a PowerPoint slide.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category, showcasing how to use BarcodeGenerator to create vector images and Presentation to embed them. Typical use cases include automated report generation, batch creation of slide decks with barcodes, and dynamic document assembly. Developers often need to combine barcode creation with Office document manipulation, using classes like BarcodeGenerator, BarCodeImageFormat, Presentation, and ImageCollection.
// Prompt: Export a barcode as an EMF vector file and import it into a PowerPoint slide programmatically.
// Tags: barcode, code128, emf, vector, powerpoint, aspose.barcode, aspose.slides, generation, import

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode as an EMF file and embedding it into a PowerPoint presentation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it as EMF, and creates a PPTX with the barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file paths
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        string pptxPath = Path.Combine(outputDir, "BarcodePresentation.pptx");

        // -----------------------------------------------------------------
        // 1. Generate a barcode and export it as an EMF vector file
        // -----------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Optional visual settings
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode in EMF format
                generator.Save(emfPath, BarCodeImageFormat.Emf);
                Console.WriteLine($"Barcode saved as EMF: {emfPath}");
            }
        }
        catch (Exception ex)
        {
            // EMF export requires a licensed version; handle evaluation limitation gracefully
            if (ex.Message != null && ex.Message.Contains("evaluation", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("A valid Aspose.BarCode license is required for EMF export.");
                return;
            }

            Console.WriteLine($"Error generating barcode: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Create a PowerPoint presentation and embed the EMF image
        // -----------------------------------------------------------------
        try
        {
            using (var presentation = new Presentation())
            {
                // Use the first (default) slide
                var slide = presentation.Slides[0];

                // Load EMF image bytes and add to the presentation's image collection
                byte[] emfBytes = File.ReadAllBytes(emfPath);
                var image = presentation.Images.AddImage(emfBytes);

                // Define picture frame position and size (in points)
                float x = 50f;
                float y = 50f;
                float width = 400f;
                float height = 150f;

                // Insert the EMF image as a picture frame
                slide.Shapes.AddPictureFrame(ShapeType.Rectangle, x, y, width, height, image);

                // Save the presentation
                presentation.Save(pptxPath, SaveFormat.Pptx);
                Console.WriteLine($"Presentation saved: {pptxPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating presentation: {ex.Message}");
        }
    }
}