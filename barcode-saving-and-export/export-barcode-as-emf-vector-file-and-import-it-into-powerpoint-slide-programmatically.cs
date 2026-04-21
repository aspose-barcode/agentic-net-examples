using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the generated EMF file and the PowerPoint presentation
        string emfPath = "barcode.emf";
        string pptxPath = "barcode_presentation.pptx";

        // Create a barcode generator for Code128 and set the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            // Save the barcode as an EMF vector file
            try
            {
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
            catch (Exception ex)
            {
                // Evaluation version of Aspose.BarCode allows only certain symbologies for EMF export
                if (ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license. Please apply a license before using this feature.");
                    return;
                }
                throw;
            }
        }

        // Verify that the EMF file was created
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("Failed to create the EMF file.");
            return;
        }

        // Create a new PowerPoint presentation and add the EMF image to the first slide
        using (var presentation = new Presentation())
        {
            var slide = presentation.Slides[0];

            // Load EMF bytes and add them to the presentation's image collection
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            IPPImage emfImage = presentation.Images.AddImage(emfBytes);

            // Define picture frame dimensions (points)
            float pictureWidth = 400f;
            float pictureHeight = 300f;

            // Add the EMF image as a picture frame on the slide
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 0f, 0f, pictureWidth, pictureHeight, emfImage);

            // Save the presentation
            presentation.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine("Barcode EMF exported and embedded into PowerPoint successfully.");
    }
}