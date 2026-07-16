// Title: PowerShell wrapper for Aspose.BarCode barcode generation
// Description: Demonstrates how to generate a PowerShell function that creates barcodes using Aspose.BarCode and saves the image to a specified path.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator and EncodeTypes classes to produce barcode images. Developers often need to automate barcode creation in scripts or CI pipelines; this snippet provides a reusable PowerShell function that loads the Aspose.BarCode assembly, selects a symbology, and writes the output file.
// Prompt: Create a PowerShell function that wraps barcode generation and writes output image to specified file path.
// Tags: barcode symbology generation powershell aspose.barcode image output

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a PowerShell script that defines a function for creating barcodes with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that writes the PowerShell function to a .ps1 file in the current directory.
    /// </summary>
    static void Main()
    {
        // PowerShell script defining the Invoke-GenerateBarcode function.
        string psFunction = @"
function Invoke-GenerateBarcode {
    param(
        [Parameter(Mandatory=$true)][string]$Symbology,
        [Parameter(Mandatory=$true)][string]$CodeText,
        [Parameter(Mandatory=$true)][string]$OutputPath
    )
    # Load Aspose.BarCode assembly (expects Aspose.BarCode.dll in the same directory as this script)
    $assemblyPath = Join-Path -Path (Split-Path -Parent $MyInvocation.MyCommand.Path) 'Aspose.BarCode.dll'
    if (-not (Test-Path $assemblyPath)) {
        throw ""Aspose.BarCode.dll not found at $assemblyPath""
    }
    Add-Type -Path $assemblyPath

    # Resolve symbology name to EncodeTypes enum value via reflection
    $field = [Aspose.BarCode.Generation.EncodeTypes].GetField($Symbology)
    if ($null -eq $field) {
        throw ""Unknown symbology: $Symbology""
    }
    $encodeType = $field.GetValue($null)

    # Create the barcode generator and save the image
    $generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator($encodeType, $CodeText)
    $generator.Save($OutputPath)
}
";

        // Determine the full path for the PowerShell script file.
        string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "GenerateBarcode.ps1");

        // Write the script content to the file system.
        File.WriteAllText(scriptPath, psFunction);

        // Inform the user where the script was saved.
        Console.WriteLine($"PowerShell function written to: {scriptPath}");
    }
}