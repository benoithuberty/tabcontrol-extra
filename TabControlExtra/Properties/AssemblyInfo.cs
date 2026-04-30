#region Using directives

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#endif

#endregion

#if NET5_0_OR_GREATER
[assembly: SupportedOSPlatform("windows")]
#endif

[assembly: AssemblyTitle("Adiict.TabControlExtra")]
[assembly: AssemblyDescription("Replacement for the Microsoft .Net TabControl with advanced features")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Adiict")]
[assembly: AssemblyProduct("Adiict.TabControlExtra")]
[assembly: AssemblyCopyright("Copyright © Mark Jackson 2010; Richard L King 2012-2018; Adiict 2026")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en-GB")]
[assembly: CLSCompliant(true)]

[assembly: ComVisible(false)]

[assembly: AssemblyVersion("3.0.2")]
[assembly: Guid("8C5AD640-CEDA-49F7-A709-610779ADB760")]
