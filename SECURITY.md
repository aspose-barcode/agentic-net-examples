# Security Policy

## Supported Versions

The following versions of the Aspose.BarCode for .NET NuGet package receive
security fixes when a vulnerability is discovered in the example code
shipped in this repository:

| Version   | Supported         |
|-----------|-------------------|
| 26.7.0    | :white_check_mark: |
| < 26.7.0 | :x:                |

Only the latest published Aspose.BarCode NuGet is tracked for issues that
originate in these examples. For vulnerabilities in the Aspose.BarCode for
.NET library itself, contact Aspose directly at
[security@aspose.com](mailto:security@aspose.com).

## Reporting a Vulnerability

**Please do not open a public GitHub issue for security vulnerabilities.**

Instead, report privately via one of:

1. **Email** — Send details to
   [security@aspose.com](mailto:security@aspose.com). Please
   include:
   - A description of the vulnerability
   - Steps to reproduce (a minimal example, if possible)
   - The affected file(s) / category folder(s)
   - Your assessment of impact
2. **GitHub Private Vulnerability Reporting** — Use the "Report a
   vulnerability" button on the Security tab of this repository.

## What to Expect

- **Acknowledgment** within 3 business days.
- **Initial assessment** within 10 business days, covering severity, scope,
  and a rough remediation timeline.
- **Fix + coordinated disclosure**: once a fix is prepared we'll agree a
  disclosure date with the reporter and credit the reporter (unless
  anonymity is requested) in the release notes.

## Scope

- **In scope**: security issues in the example `.cs` files (unsafe patterns
  that could enable injection, path traversal, insecure cryptography use,
  denial of service, etc.), issues in the generated agent docs
  (`agents.md`, `.well-known/agent.json`), or in the tooling
  that produced the examples.
- **Out of scope**: vulnerabilities in the Aspose.BarCode for .NET library
  itself. Please report those directly to Aspose at
  [security@aspose.com](mailto:security@aspose.com).

Thank you for helping keep the community safe.
