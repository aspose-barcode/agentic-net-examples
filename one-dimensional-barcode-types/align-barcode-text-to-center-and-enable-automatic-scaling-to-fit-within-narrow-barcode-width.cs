// Title: Center barcode text and enable auto scaling for narrow width
// Description: Demonstrates how to center the human‑readable text of a Code128 barcode and automatically scale the image to fit a specified narrow width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as CodeTextParameters.Alignment and AutoSizeMode. Developers often need to adjust text alignment and automatically resize barcodes for tight layout constraints in documents, labels, or web pages. The snippet shows typical steps for configuring alignment, scaling mode, and image dimensions before saving the barcode image.
// Prompt: Align barcode text to center and enable automatic scaling to fit within narrow barcode width.
// Tags: code128, barcode, text alignment, autoscaling, image size, aspnet, aspose.barcode, generation, png

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with centered human‑readable text and
/// automatically scales the image to fit a narrow width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode parameters,
    /// applies text alignment and auto‑scaling, then saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Center the human‑readable text beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Enable automatic scaling to fit a narrow width using interpolation
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired image width (points); adjust as needed for layout constraints
            generator.Parameters.ImageWidth.Point = 150f;

            // Set the desired image height (points); optional—omit to preserve aspect ratio
            generator.Parameters.ImageHeight.Point = 50f;

            // Save the generated barcode image; format inferred from file extension
            generator.Save("centered_scaled.png");
        }
    }
}