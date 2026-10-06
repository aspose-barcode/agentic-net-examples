// Title: Export Barcode to EMF and Insert into PowerPoint
// Description: Demonstrates generating a Code128 barcode, saving it as an EMF vector file, and embedding the image into a PowerPoint slide using Aspose.BarCode and Aspose.Slides.
// Category-Description: This example belongs to the Aspose.BarCode image export and Aspose.Slides presentation creation category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeImageFormat for vector image export, and the Presentation class to build PowerPoint files. Developers often need to programmatically generate barcodes and include them in documents or slides for reporting, labeling, or marketing purposes.
// Prompt: Export a barcode as an EMF vector file and import it into a PowerPoint slide programmatically.
// Tags: barcode, code128, generation, export, emf, vector, powerpoint, slides, aspose.barcode, aspose.slides

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;
using Aspose.Drawing;

/// <summary>
/// Generates a barcode, saves it as an EMF file, and inserts it into a PowerPoint presentation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a Code128 barcode,
    /// exports it to EMF, embeds the EMF image into a new PowerPoint slide, and saves the files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output folder
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the EMF image and the PowerPoint presentation
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        string pptxPath = Path.Combine(outputDir, "BarcodePresentation.pptx");

        // Generate the barcode and save it as an EMF vector image
        try
        {
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");
            generator.Save(emfPath, BarCodeImageFormat.Emf);
        }
        catch (Exception ex)
        {
            // Handle licensing or other generation errors
            if (ex.Message != null && ex.Message.IndexOf("evaluation", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine("EMF export requires a valid Aspose.BarCode license. Please apply a license before using this feature.");
                return;
            }
            Console.WriteLine("Error generating EMF barcode: " + ex.Message);
            return;
        }

        // Create a new PowerPoint presentation and add the EMF image to the first slide
        using (Presentation pres = new Presentation())
        {
            // Access the first slide (created by default)
            ISlide slide = pres.Slides[0];

            // Load the EMF file into a byte array and add it to the presentation's image collection
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            IPPImage image = pres.Images.AddImage(emfBytes);

            // Define picture position and size (in points)
            float x = 0f;
            float y = 0f;
            float width = 400f;
            float height = 300f;

            // Insert the EMF image as a picture frame on the slide
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, x, y, width, height, image);

            // Save the presentation to the specified PPTX file
            pres.Save(pptxPath, SaveFormat.Pptx);
        }

        // Output the locations of the generated files
        Console.WriteLine("Barcode EMF saved to: " + emfPath);
        Console.WriteLine("PowerPoint presentation saved to: " + pptxPath);
    }
}