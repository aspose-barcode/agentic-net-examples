// Title: Generate PowerShell module for Aspose.BarCode barcode creation
// Description: This example creates a PowerShell module that exposes a New-Barcode function, allowing scripts to generate barcodes with Aspose.BarCode and save them as PNG images.
// Category-Description: Demonstrates how to use the Aspose.BarCode.Generation API (BarcodeGenerator, EncodeTypes, BarCodeImageFormat) to produce barcodes programmatically. Typical scenarios include automating label creation, integrating barcode generation into CI pipelines, or providing scripting access via PowerShell. Developers often need a simple wrapper to call .NET barcode methods from PowerShell scripts.
// Prompt: Provide a PowerShell module that wraps .NET barcode generation methods for quick scripting use.
// Tags: barcode, symbology, generation, powershell, aspose.barcode, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a PowerShell module that wraps Aspose.BarCode generation methods for easy scripting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that writes the PowerShell module file and displays usage instructions.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the PowerShell module file in the current working directory
        string modulePath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeModule.psm1");

        // PowerShell script content that defines the New-Barcode function
        string moduleContent = @"
function New-Barcode {
    param(
        [Parameter(Mandatory=$true)][string]$Symbology,
        [Parameter(Mandatory=$true)][string]$CodeText,
        [Parameter(Mandatory=$true)][string]$OutputPath
    )
    $field = [Aspose.BarCode.Generation.EncodeTypes].GetField($Symbology)
    if ($null -eq $field) {
        throw ""Unknown symbology: $Symbology""
    }
    $encodeType = $field.GetValue($null)
    $generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator($encodeType, $CodeText)
    try {
        $generator.Save($OutputPath, [Aspose.BarCode.Generation.BarCodeImageFormat]::Png)
    }
    finally {
        $generator.Dispose()
    }
}
";

        // Write the module script to the file system
        File.WriteAllText(modulePath, moduleContent);

        // Output the location of the created module and a short usage example
        Console.WriteLine($"PowerShell module created at: {modulePath}");
        Console.WriteLine("Example usage in PowerShell:");
        Console.WriteLine("Import-Module -Path \"{0}\"", modulePath);
        Console.WriteLine("New-Barcode -Symbology \"Code128\" -CodeText \"Sample123\" -OutputPath \"sample.png\"");
    }
}