---
name: barcode-text-customization
description: C# examples for Barcode Text Customization using Aspose.BarCode for .NET
language: csharp
framework: net9.0
parent: ../agents.md
---

# AGENTS - Barcode Text Customization

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET,
working within the **Barcode Text Customization** category.
This folder contains standalone C# examples for Barcode Text Customization operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Required Namespaces

- `using System;`
- `using System.IO;`
- `using Aspose.BarCode.Generation;`
- `using Aspose.Drawing;`

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [adjust-gap-between-barcode-and-its-text-to-4-points-for-high-density-qr-codes.cs](./adjust-gap-between-barcode-and-its-text-to-4-points-for-high-density-qr-codes.cs) | `QrParameters` | Adjust the gap between barcode and its text to 4 points for high‑density QR codes. |
| [align-barcode-text-left-for-series-of-upc-barcodes-by-setting-textalignmentleft.cs](./align-barcode-text-left-for-series-of-upc-barcodes-by-setting-textalignmentleft.cs) |  | Align barcode text left for a series of UPC‑A barcodes by setting TextAlignment.Left. |
| [align-top-caption-to-center-for-qr-codes-by-setting-captionparameterstopalignment-to-captionalignmentcenter.cs](./align-top-caption-to-center-for-qr-codes-by-setting-captionparameterstopalignment-to-captionalignmentcenter.cs) | `QrParameters`, `CaptionParameters` | Align top caption to center for QR codes by setting CaptionParameters.Top.Alignment to Cap... |
| [apply-different-fonts-to-main-text-and-caption-by-setting-codetextparametersfont-and-captionparametersfont-separately.cs](./apply-different-fonts-to-main-text-and-caption-by-setting-codetextparametersfont-and-captionparametersfont-separately.cs) | `CaptionParameters`, `FontUnit` | Apply different fonts to main text and caption by setting CodetextParameters.Font and Capt... |
| [apply-fontmodeauto-to-barcode-text-so-library-automatically-calculates-optimal-font-size-for-each-symbol.cs](./apply-fontmodeauto-to-barcode-text-so-library-automatically-calculates-optimal-font-size-for-each-symbol.cs) | `FontUnit` | Apply FontMode.Auto to barcode text so the library automatically calculates optimal font s... |
| [batch-generate-qr-codes-with-custom-texts-from-csv-file-applying-unique-font-sizes-per-row.cs](./batch-generate-qr-codes-with-custom-texts-from-csv-file-applying-unique-font-sizes-per-row.cs) | `QrParameters`, `FontUnit`, `BarcodeGenerator` | Batch generate QR codes with custom texts from a CSV file, applying unique font sizes per ... |
| [center-align-barcode-text-for-collection-of-ean-13-barcodes-by-setting-codetextparametersalignment-to-textalignmentcente.cs](./center-align-barcode-text-for-collection-of-ean-13-barcodes-by-setting-codetextparametersalignment-to-textalignmentcente.cs) |  | Center-align barcode text for a collection of EAN‑13 barcodes by setting CodetextParameter... |
| [compare-rendering-performance-between-fontmodeauto-and-manually-specified-fonts-for-large-batches-of-qr-codes.cs](./compare-rendering-performance-between-fontmodeauto-and-manually-specified-fonts-for-large-batches-of-qr-codes.cs) | `QrParameters` | Compare rendering performance between FontMode.Auto and manually specified fonts for large... |
| [create-barcodes-where-both-main-text-and-bottom-caption-are-visible-each-aligned-left-for-consistency.cs](./create-barcodes-where-both-main-text-and-bottom-caption-are-visible-each-aligned-left-for-consistency.cs) | `CaptionParameters` | Create barcodes where both main text and bottom caption are visible, each aligned left for... |
| [define-barcode-text-font-as-arial-size-6-regular-style-for-all-generated-code39-symbols.cs](./define-barcode-text-font-as-arial-size-6-regular-style-for-all-generated-code39-symbols.cs) | `FontUnit`, `BarcodeGenerator` | Define barcode text font as Arial, size 6, regular style for all generated Code39 symbols. |
| [define-caption-font-as-times-new-roman-size-9-for-all-generated-aztec-barcodes-to-match-branding.cs](./define-caption-font-as-times-new-roman-size-9-for-all-generated-aztec-barcodes-to-match-branding.cs) | `AztecParameters`, `CaptionParameters`, `FontUnit`, `BarcodeGenerator` | Define caption font as Times New Roman, size 9, for all generated Aztec barcodes to match ... |
| [develop-script-that-generates-datamatrix-barcodes-with-top-caption-padded-10-pixels-and-centered-alignment.cs](./develop-script-that-generates-datamatrix-barcodes-with-top-caption-padded-10-pixels-and-centered-alignment.cs) | `DataMatrixParameters`, `CaptionParameters`, `BarcodeGenerator` | Develop a script that generates DataMatrix barcodes with top caption padded 10 pixels and ... |
| [enable-nowrap-mode-for-barcode-text-on-datamatrix-barcodes-to-prevent-line-breaks-in-long-strings.cs](./enable-nowrap-mode-for-barcode-text-on-datamatrix-barcodes-to-prevent-line-breaks-in-long-strings.cs) | `DataMatrixParameters` | Enable NoWrap mode for barcode text on DataMatrix barcodes to prevent line breaks in long ... |
| [export-barcodes-with-customized-text-to-png-format-preserving-alignment-and-spacing-settings-in-image.cs](./export-barcodes-with-customized-text-to-png-format-preserving-alignment-and-spacing-settings-in-image.cs) |  | Export barcodes with customized text to PNG format, preserving alignment and spacing setti... |
| [export-barcodes-with-customized-text-to-svg-format-to-retain-vector-based-alignment-for-scalable-rendering.cs](./export-barcodes-with-customized-text-to-svg-format-to-retain-vector-based-alignment-for-scalable-rendering.cs) |  | Export barcodes with customized text to SVG format to retain vector‑based alignment for sc... |
| [generate-barcodes-with-hidden-main-text-and-visible-top-caption-to-display-supplemental-information-only.cs](./generate-barcodes-with-hidden-main-text-and-visible-top-caption-to-display-supplemental-information-only.cs) | `CaptionParameters`, `BarcodeGenerator` | Generate barcodes with hidden main text and visible top caption to display supplemental in... |
| [hide-all-captions-for-batch-of-code128-barcodes-by-setting-captionparametersvisible-to-false-globally.cs](./hide-all-captions-for-batch-of-code128-barcodes-by-setting-captionparametersvisible-to-false-globally.cs) |  | Hide all captions for a batch of Code128 barcodes by setting CaptionParameters.Visible to ... |
| [hide-main-barcode-text-for-batch-of-qr-code-images-by-setting-codetextparametersvisible-to-false.cs](./hide-main-barcode-text-for-batch-of-qr-code-images-by-setting-codetextparametersvisible-to-false.cs) | `QrParameters` | Hide main barcode text for a batch of QR code images by setting CodetextParameters.Visible... |
| [integrate-barcode-text-customization-into-aspnet-mvc-view-allowing-end-users-to-toggle-visibility-and-alignment.cs](./integrate-barcode-text-customization-into-aspnet-mvc-view-allowing-end-users-to-toggle-visibility-and-alignment.cs) |  | Integrate barcode text customization into an ASP.NET MVC view, allowing end users to toggl... |
| [place-barcode-text-above-datamatrix-symbols-by-assigning-codetextparameterslocation-textlocationabove.cs](./place-barcode-text-above-datamatrix-symbols-by-assigning-codetextparameterslocation-textlocationabove.cs) | `DataMatrixParameters` | Place barcode text above DataMatrix symbols by assigning CodetextParameters.Location = Tex... |
| [replace-displayed-text-of-qr-code-with-custom-url-by-setting-codetextparameterstext.cs](./replace-displayed-text-of-qr-code-with-custom-url-by-setting-codetextparameterstext.cs) | `QrParameters` | Replace displayed text of a QR code with a custom URL by setting CodetextParameters.Text. |
| [right-align-barcode-text-for-itf-14-barcodes-by-setting-codetextparametersalignment-textalignmentright.cs](./right-align-barcode-text-for-itf-14-barcodes-by-setting-codetextparametersalignment-textalignmentright.cs) |  | Right-align barcode text for ITF‑14 barcodes by setting CodetextParameters.Alignment = Tex... |
| [serialize-barcode-text-and-caption-settings-to-json-then-reload-them-to-recreate-identical-barcode-appearances.cs](./serialize-barcode-text-and-caption-settings-to-json-then-reload-them-to-recreate-identical-barcode-appearances.cs) | `CaptionParameters` | Serialize barcode text and caption settings to JSON, then reload them to recreate identica... |
| [set-barcode-text-location-to-below-for-pdf417-barcodes-using-default-textlocationbelow-setting.cs](./set-barcode-text-location-to-below-for-pdf417-barcodes-using-default-textlocationbelow-setting.cs) | `Pdf417Parameters` | Set barcode text location to below for PDF417 barcodes, using the default TextLocation.Bel... |
| [set-barcode-text-spacing-to-25-pixels-for-batch-of-aztec-barcodes-to-improve-readability.cs](./set-barcode-text-spacing-to-25-pixels-for-batch-of-aztec-barcodes-to-improve-readability.cs) | `AztecParameters` | Set barcode text spacing to 2.5 pixels for a batch of Aztec barcodes to improve readabilit... |
| [set-bottom-caption-padding-to-8-pixels-for-datamatrix-barcodes-to-create-visual-separation-from-symbol.cs](./set-bottom-caption-padding-to-8-pixels-for-datamatrix-barcodes-to-create-visual-separation-from-symbol.cs) | `DataMatrixParameters`, `CaptionParameters`, `Padding` | Set bottom caption padding to 8 pixels for DataMatrix barcodes to create visual separation... |
| [show-main-barcode-text-for-all-generated-code128-barcodes-by-setting-codetextparametersvisible-to-true.cs](./show-main-barcode-text-for-all-generated-code128-barcodes-by-setting-codetextparametersvisible-to-true.cs) | `BarcodeGenerator` | Show main barcode text for all generated Code128 barcodes by setting CodetextParameters.Vi... |
| [show-top-caption-for-pdf417-barcodes-by-setting-captionparameterstopvisible-to-true.cs](./show-top-caption-for-pdf417-barcodes-by-setting-captionparameterstopvisible-to-true.cs) | `Pdf417Parameters`, `CaptionParameters` | Show top caption for PDF417 barcodes by setting CaptionParameters.Top.Visible to true. |
| [write-unit-tests-that-verify-toggling-codetextparametersvisible-correctly-shows-and-hides-main-barcode-text.cs](./write-unit-tests-that-verify-toggling-codetextparametersvisible-correctly-shows-and-hides-main-barcode-text.cs) | `Unit` | Write unit tests that verify toggling CodetextParameters.Visible correctly shows and hides... |

## Category Statistics
- Total examples: 29
- Failed: 0
- Pass rate: 100.0%

## Key API Surface

- `BarcodeGenerator`
- `CaptionParameters`
- `FontUnit`
- `CodeTextParameters`
- `TextParameters`

## Failed Tasks

All tasks passed ✅

<!-- AUTOGENERATED:START -->
Updated: 2026-04-28 | Examples: 29
<!-- AUTOGENERATED:END -->
