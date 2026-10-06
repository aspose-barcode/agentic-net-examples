// Title: Demonstrate default checksum behavior for optional and obligatory barcode symbologies
// Description: Shows how Aspose.BarCode handles checksum defaults for Code39 (optional) and Code128 (obligatory) when generating barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the default checksum settings for different symbology types. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create PNG images. Developers often need to understand when checksums are automatically applied versus when they must be enabled manually, especially when working with optional and obligatory checksum symbologies.
// Prompt: Write documentation comments explaining default checksum behavior for obligatory and optional symbologies.
// Tags: barcode symbology, checksum, generation, png, aspose.barcode, code39, code128

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Entry point for the checksum demonstration example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcode images for optional and obligatory checksum symbologies and displays default checksum information.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images.
        string tempPath = Path.Combine(Path.GetTempPath(), "ChecksumDemo");
        Directory.CreateDirectory(tempPath);

        // ------------------------------
        // Optional checksum symbology (Code39)
        // ------------------------------
        using (var genOptional = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // By default, optional checksum is disabled (IsChecksumEnabled = EnableChecksum.No).
            genOptional.Save(Path.Combine(tempPath, "Code39_Optional.png"), BarCodeImageFormat.Png);
        }

        // ------------------------------
        // Obligatory checksum symbology (Code128)
        // ------------------------------
        using (var genObligatory = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // By default, obligatory checksum is enabled (IsChecksumEnabled = EnableChecksum.Yes).
            genObligatory.Save(Path.Combine(tempPath, "Code128_Obligatory.png"), BarCodeImageFormat.Png);
        }

        // Output the location of generated images and default checksum behaviors.
        Console.WriteLine("Generated barcode images in: " + tempPath);
        Console.WriteLine("Optional checksum default: " + ChecksumInfo.GetDefaultChecksumBehaviorOptional());
        Console.WriteLine("Obligatory checksum default: " + ChecksumInfo.GetDefaultChecksumBehaviorObligatory());
    }
}

/// <summary>
/// Provides information about default checksum behavior for barcode symbologies.
/// </summary>
static class ChecksumInfo
{
    /// <summary>
    /// Returns a description of the default checksum behavior for symbologies with optional checksum.
    /// For optional checksum symbologies (e.g., Code39, Codabar, MSI, etc.), the <see cref="BarcodeParameters.IsChecksumEnabled"/>
    /// property defaults to <see cref="EnableChecksum.No"/>, meaning the checksum is not calculated unless explicitly enabled.
    /// </summary>
    public static string GetDefaultChecksumBehaviorOptional()
    {
        return "Optional checksum symbologies default to checksum disabled (EnableChecksum.No).";
    }

    /// <summary>
    /// Returns a description of the default checksum behavior for symbologies with obligatory checksum.
    /// For obligatory checksum symbologies (e.g., Code128, EAN13, UPC-A, Code93, etc.), the <see cref="BarcodeParameters.IsChecksumEnabled"/>
    /// property defaults to <see cref="EnableChecksum.Yes"/>, meaning the checksum is always calculated.
    /// </summary>
    public static string GetDefaultChecksumBehaviorObligatory()
    {
        return "Obligatory checksum symbologies default to checksum enabled (EnableChecksum.Yes).";
    }
}