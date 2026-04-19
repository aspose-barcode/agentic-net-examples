using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Slides;
using Aspose.Slides.Export;

class Program
{
    static void Main()
    {
        // Define paths
        string emfPath = Path.Combine(Path.GetTempPath(), "barcode.emf");
        string pptxPath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodePresentation.pptx");

        // Create a barcode (Code39 is required for EMF in evaluation version) and save it as EMF
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "1234567890"))
        {
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Save(emfPath, BarCodeImageFormat.Emf);
        }

        // Verify that the EMF file was created
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("Failed to create EMF file.");
            return;
        }

        // Load the EMF into a PowerPoint presentation
        using (Presentation presentation = new Presentation())
        {
            // Get the first slide (a default slide is created automatically)
            var slide = presentation.Slides[0];

            // Load EMF bytes and add to the presentation's image collection
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            IPPImage emfImage = presentation.Images.AddImage(emfBytes);

            // Define picture frame size (in points)
            float pictureWidth = 400f;
            float pictureHeight = 300f;

            // Add the EMF image as a picture frame to the slide
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 0f, 0f, pictureWidth, pictureHeight, emfImage);

            // Save the presentation
            presentation.Save(pptxPath, SaveFormat.Pptx);
        }

        // Clean up temporary EMF file
        try
        {
            File.Delete(emfPath);
        }
        catch
        {
            // Ignored – if deletion fails, the file will remain in the temp folder
        }

        Console.WriteLine($"Presentation saved to: {pptxPath}");
    }
}