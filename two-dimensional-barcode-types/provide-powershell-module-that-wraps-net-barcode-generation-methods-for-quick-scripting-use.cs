// Title: Generate PowerShell module for Aspose.BarCode barcode creation
// Description: Demonstrates creating a PowerShell .psm1 file that wraps Aspose.BarCode .NET methods, enabling quick script-based barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce images for various symbologies. Developers often need to expose .NET barcode functionality to PowerShell for automation, CI pipelines, or ad‑hoc scripting. The snippet illustrates module creation, reflection‑based symbology mapping, and proper resource disposal.
// Prompt: Provide a PowerShell module that wraps .NET barcode generation methods for quick scripting use.
// Tags: barcode symbology, generation, powershell module, aspose.barcode, encode types

using System;
using System.IO;

namespace BarcodeModuleGenerator
{
    /// <summary>
    /// Generates a PowerShell module that provides a simple wrapper around Aspose.BarCode
    /// for creating barcodes from scripts.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Writes the PowerShell module file to the current directory.
        /// </summary>
        static void Main()
        {
            // Define the PowerShell module content as a verbatim string.
            string moduleContent = @"
function New-Barcode {
    param(
        [ValidateSet(""Code128"",""QRCode"",""DataMatrix"",""Pdf417"",""Aztec"")]
        [string]$Symbology,
        [string]$CodeText,
        [string]$OutputPath
    )

    # Resolve the symbology name to an EncodeTypes field via reflection
    $field = [Aspose.BarCode.Generation.EncodeTypes].GetField($Symbology)
    if ($null -eq $field) {
        throw ""Unknown symbology: $Symbology""
    }
    $encodeType = $field.GetValue($null)

    # Create the barcode generator, generate and save the image
    $generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator $encodeType, $CodeText
    try {
        $generator.Save($OutputPath)
    }
    finally {
        if ($generator -ne $null) { $generator.Dispose() }
    }
}
";

            // Determine the full path for the .psm1 file in the current working directory.
            string modulePath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeModule.psm1");

            // Write the module content to the file, overwriting if it already exists.
            File.WriteAllText(modulePath, moduleContent);

            // Inform the user where the module was created.
            Console.WriteLine($"PowerShell module generated at: {modulePath}");
        }
    }
}