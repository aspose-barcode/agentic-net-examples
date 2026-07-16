// Title: Generate GS1 Composite barcode with GS1 Code128 linear and CC_B 2D components
// Description: Demonstrates creating a GS1 Composite barcode where the linear component is GS1 Code128 and the 2D component is CC_B (MicroPDF417). The resulting image is saved as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on composite barcode creation. It showcases the use of BarcodeGenerator, EncodeTypes, and TwoDComponentType classes to combine linear and 2D symbologies. Developers often need to generate GS1 Composite barcodes for retail and logistics applications, requiring precise control over component types and visual parameters.
// Prompt: Generate a GS1 Composite barcode using GS1 Code128 as linear component and CC_B as 2D component.
// Tags: gs1 composite, gs1code128, cc_b, barcode generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode (GS1 Code128 + CC_B) and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates and configures a BarcodeGenerator to produce the desired composite barcode.
    /// </summary>
    static void Main()
    {
        // Define the composite barcode text: linear part | 2D part
        string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Initialize the generator for a GS1 Composite barcode with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure the linear component to use GS1 Code128 symbology
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component to use CC_B (MicroPDF417) symbology
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

            // Optional: adjust the aspect ratio of the 2D component for better visual balance
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

            // Set the X-dimension (module width) for both components in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Define the height of the linear component in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated barcode image to a file
            generator.Save("gs1_composite.png");
        }
    }
}