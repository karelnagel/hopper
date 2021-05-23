// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task",
        Justification = "Considered not to use it.")]

// Migrations generated code.
[assembly:
    SuppressMessage("Design", "CA1062:Validate arguments of public methods",
        Justification = "Migrations generated code", Scope = "namespaceanddescendants",
        Target = "~N:Hopper.Data.Migrations")]

// DTOs and DBOs.
[assembly:
    SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO",
        Scope = "namespaceanddescendants", Target = "~N:Hopper.DTOs")]
[assembly:
    SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DBO",
        Scope = "namespaceanddescendants", Target = "~N:Hopper.Models")]

// Identity scaffold code.
[assembly:
    SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Identity scaffold code.",
        Scope = "namespaceanddescendants", Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Design", "CA1054:URI-like parameters should not be strings",
        Justification = "Identity scaffold code.", Scope = "namespaceanddescendants",
        Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Design", "CA1056:URI-like properties should not be strings",
        Justification = "Identity scaffold code.", Scope = "namespaceanddescendants",
        Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Identity scaffold code.",
        Scope = "namespaceanddescendants", Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Style", "IDE0037:Use inferred member name", Justification = "Identity scaffold code.",
        Scope = "namespaceanddescendants", Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Usage", "CA2227:Collection properties should be read only",
        Justification = "Identity scaffold code.", Scope = "namespaceanddescendants",
        Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Performance", "CA1819:Properties should not return arrays",
        Justification = "Identity scaffold code.", Scope = "namespaceanddescendants",
        Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Identity scaffold code.",
        Scope = "namespaceanddescendants", Target = "~N:Hopper.Areas.Identity")]
[assembly:
    SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "Identity scaffold code.",
        Scope = "namespaceanddescendants", Target = "~N:Hopper.Areas.Identity")]
// Individual Identity scaffold code.
[assembly:
    SuppressMessage("Performance", "CA1834:Consider using 'StringBuilder.Append(char)' when applicable",
        Justification = "Identity scaffold code.", Scope = "member",
        Target =
            "~M:Hopper.Areas.Identity.Pages.Account.Manage.EnableAuthenticatorModel.FormatKey(System.String)~System.String")]
[assembly:
    SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Identity scaffold code.",
        Scope = "member",
        Target =
            "~M:Hopper.Areas.Identity.Pages.Account.Manage.EnableAuthenticatorModel.FormatKey(System.String)~System.String")]
[assembly:
    SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Identity scaffold code.",
        Scope = "member",
        Target =
            "~M:Hopper.Areas.Identity.Pages.Account.Manage.EnableAuthenticatorModel.GenerateQrCodeUri(System.String,System.String)~System.String")]