// Title: Generate MaxiCode barcodes in GIF format using Aspose.BarCode
// Description: Demonstrates how to create MaxiCode barcodes for various modes and save them as GIF images.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCode codetext classes (e.g., MaxiCodeCodetextMode2, MaxiCodeStandardCodetext) to produce different MaxiCode modes. Developers commonly need to generate MaxiCode symbols for shipping and logistics applications, selecting appropriate modes and output image formats such as GIF.
// Prompt: Set the generator's ImageFormat property to GIF and produce a series of MaxiCode images.
// Tags: maxicode, barcode generation, gif, aspose.barcode, complexbarcode, imageformat, c#

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that creates MaxiCode barcodes in GIF format for several modes
/// using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates MaxiCode images for modes 2‑6 and saves them as GIF files.
    /// </summary>
    static void Main()
    {
        // ---------- Mode 2 with a standard second message ----------
        var mode2Standard = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Standard message" }
        };
        using (var generator = new ComplexBarcodeGenerator(mode2Standard))
        {
            generator.Save("MaxiCode_Mode2_Standard.gif");
        }

        // ---------- Mode 2 with a structured second message ----------
        var structuredMsg2 = new MaxiCodeStructuredSecondMessage();
        structuredMsg2.Add("634 ALPHA DRIVE");
        structuredMsg2.Add("PITTSBURGH");
        structuredMsg2.Add("PA");
        structuredMsg2.Year = 99;

        var mode2Structured = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = structuredMsg2
        };
        using (var generator = new ComplexBarcodeGenerator(mode2Structured))
        {
            generator.Save("MaxiCode_Mode2_Structured.gif");
        }

        // ---------- Mode 3 with a standard second message ----------
        var mode3Standard = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Standard message" }
        };
        using (var generator = new ComplexBarcodeGenerator(mode3Standard))
        {
            generator.Save("MaxiCode_Mode3_Standard.gif");
        }

        // ---------- Mode 3 with a structured second message ----------
        var structuredMsg3 = new MaxiCodeStructuredSecondMessage();
        structuredMsg3.Add("634 ALPHA DRIVE");
        structuredMsg3.Add("PITTSBURGH");
        structuredMsg3.Add("PA");
        structuredMsg3.Year = 99;

        var mode3Structured = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = structuredMsg3
        };
        using (var generator = new ComplexBarcodeGenerator(mode3Structured))
        {
            generator.Save("MaxiCode_Mode3_Structured.gif");
        }

        // ---------- Mode 4 (standard) ----------
        var mode4 = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Mode 4 message"
        };
        using (var generator = new ComplexBarcodeGenerator(mode4))
        {
            generator.Save("MaxiCode_Mode4.gif");
        }

        // ---------- Mode 5 (standard) ----------
        var mode5 = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode5,
            Message = "Mode 5 message"
        };
        using (var generator = new ComplexBarcodeGenerator(mode5))
        {
            generator.Save("MaxiCode_Mode5.gif");
        }

        // ---------- Mode 6 (standard) ----------
        var mode6 = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode6,
            Message = "Mode 6 message"
        };
        using (var generator = new ComplexBarcodeGenerator(mode6))
        {
            generator.Save("MaxiCode_Mode6.gif");
        }

        Console.WriteLine("All MaxiCode GIF images have been generated.");
    }
}