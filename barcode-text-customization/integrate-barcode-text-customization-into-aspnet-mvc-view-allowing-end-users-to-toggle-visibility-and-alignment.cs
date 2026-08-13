// Title: Barcode Text Customization in ASP.NET MVC View
// Description: Demonstrates how to generate a barcode with customizable human‑readable text visibility and alignment, simulating user input from an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text display. Typical use cases include dynamic barcode creation in web applications where end users can choose whether to show the text and its alignment. Developers often need to adjust text location, color, and font for branding or readability, and this snippet illustrates those common tasks.
// Prompt: Integrate barcode text customization into an ASP.NET MVC view, allowing end users to toggle visibility and alignment.
// Tags: barcode, text customization, visibility, alignment, aspnet mvc, code128, aspose.barcode, generation, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with customizable human‑readable text settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates MVC view inputs and creates a barcode image with the chosen text options.
    /// </summary>
    static void Main()
    {
        // Simulated user choices that would normally come from an MVC view (e.g., form fields).
        bool showHumanReadableText = true;                     // Toggle visibility of the text
        TextAlignment textAlignment = TextAlignment.Center;   // Choose alignment for the text

        // Barcode data to encode.
        string codeText = "Sample123";

        // Determine output folder and ensure it exists.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Generate the barcode using the specified settings.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set human‑readable text visibility based on user choice.
            generator.Parameters.Barcode.CodeTextParameters.Location = showHumanReadableText
                ? CodeLocation.Below
                : CodeLocation.None;

            // Apply the selected text alignment.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = textAlignment;

            // Optional styling: set text color and font size for demonstration purposes.
            generator.Parameters.Barcode.CodeTextParameters.Color = Aspose.Drawing.Color.DarkBlue;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode image to the output path.
            generator.Save(outputPath);
        }

        // Output information to the console for verification.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
        Console.WriteLine($"Human‑readable text visible: {showHumanReadableText}");
        Console.WriteLine($"Text alignment: {textAlignment}");
    }
}