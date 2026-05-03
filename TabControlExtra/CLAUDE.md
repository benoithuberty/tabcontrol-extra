# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```powershell
# Build all target frameworks (Debug)
dotnet build

# Build Release (signs assembly with Adiict.snk and generates NuGet packages)
dotnet build -c Release

# Build a specific framework
dotnet build -f net48
dotnet build -f net8.0-windows
dotnet build -f net10.0-windows
```

NuGet packages are automatically generated on every build (`GeneratePackageOnBuild=True`). Release builds sign the assembly using `Adiict.snk`. There are no test projects.

## Architecture

**TabControlExtra** is a Windows Forms replacement for `System.Windows.Forms.TabControl`, built around a strategy pattern for pluggable visual styles.

### Core components

- **`TabControl/TabControlExtra.cs`** — The main control (inherits `TabControl`). Handles owner-painting via double-buffered `OnPaint`/`CustomPaint`, mouse event routing (hover, tab closing, drag-drop), and exposes `DisplayStyle` / `DisplayStyleProvider` to switch styles at runtime.

- **`TabControl/TabStyleProvider.cs`** — Abstract base class defining the rendering pipeline and ~100 customizable properties (colors per state, padding, radius, overlap, gradients, closer button appearance). Contains a static `CreateProvider(TabStyle, TabControlExtra)` factory method.

- **`TabControl/TabStyleProviders/`** — Ten concrete implementations (`TabStyleDefaultProvider`, `TabStyleAngledProvider`, `TabStyleChromeProvider`, `TabStyleRoundedProvider`, `TabStyleRectangularProvider`, `TabStyleVisualStudioProvider`, `TabStyleVS2010Provider`, `TabStyleVS2012Provider`, `TabStyleIE8Provider`, `TabStyleNoneProvider`). Each overrides `AddTabBorder()` to define the tab shape and sets default property values in its constructor.

- **`TabControl/TabStyle.cs`** / **`TabState.cs`** / **`BlendStyle.cs`** — Enumerations for style selection, per-tab state (Unselected/Disabled/Highlighted/Selected/Focused), and gradient mode.

- **`TabControl/NativeMethods.cs`** — P/Invoke for Windows APIs (tab hit-testing, DPI, RTL layout).

- **`TabControl/RectangleUtils.cs`** — Aligns a rectangle within another using `ContentAlignment`.

- **`TabControl/ThemedColors.cs`** — Detects the active Windows visual theme.

### Key design patterns

| Pattern | Where |
|---|---|
| Factory | `TabStyleProvider.CreateProvider()` |
| Strategy | Each `TabStyleProvider` subclass |
| Template Method | `TabStyleProvider` defines rendering steps; subclasses override `AddTabBorder()`, `GetTabRect()`, `DrawTabCloser()`, etc. |
| Double Buffering | Two `Bitmap` objects in `TabControlExtra` prevent flicker |

### Project metadata

- Assembly name: `Adiict.TabControlExtra`
- Root namespace: `Adiict.UI.Forms`
- Version: `3.0.2` (set in `.csproj`)
- NuGet spec: `Adiict.TabControlExtra.nuspec`
- Strong-name key: `Adiict.snk` (Release only)
- Nullable: disabled (legacy code style)
