## 2024-06-02 - Dependency Modernization

**Observation:**
- Main project `Tedd.LinqUtils` targets only `netstandard2.1`.
- Test project `Tedd.LinqUtils.Tests` targets only `net6.0`.
- Outdated dependencies in test project: `coverlet.collector`, `Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`.

**Strategic Action:**
- Upgrade `Tedd.LinqUtils` to multi-target `netstandard2.1;net6.0;net8.0;net9.0`.
- Upgrade `Tedd.LinqUtils.Tests` to multi-target `net6.0;net8.0;net9.0`.
- Update outdated test dependencies to their latest stable versions.
- Update outdated test dependencies to their latest stable versions.

## 2024-06-02 - Microsoft.NET.Test.Sdk Compatibility

**Observation:**
- `Microsoft.NET.Test.Sdk` version 18.6.0 does not officially support `net6.0`.
- Upgrading to it indiscriminately violates the operational boundary: "Introducing dependency versions that are unsupported by one or more declared target frameworks."

**Strategic Action:**
- Use conditional package references in the test project. Keep an older compatible version (e.g., 17.11.x or whatever was there) for `net6.0`, and upgrade to `18.6.0` for `net8.0` and `net9.0`.

## 2024-06-02 - Final Compatibility Matrix

**Observation:**
The library and test projects require proper multi-targeting. A bug was found where adding net6.0/8.0/9.0 to `Tedd.LinqUtils` was necessary to satisfy testing. Test references needed conditionals because `Microsoft.NET.Test.Sdk` dropped support for net6.0.

**Strategic Action:**

```text
Target Framework | Supported | Dependency Set | Conditional Symbols | Compatibility Risk
-----------------|-----------|----------------|---------------------|-------------------
netstandard2.1   | Yes       | Legacy-safe    |                     | High consumer reach
net6.0           | Yes       | Legacy tests   |                     | Older test runner support
net8.0           | Yes       | Current stable |                     | Modern baseline
net9.0           | Yes       | Latest stable  |                     | Requires validation
```

## 2024-05-18 - Tests Framework Compatibility

**Observation:** `Microsoft.NET.Test.Sdk` version 18.6.0 and later dropped support for `.NET 6.0`. Using the same version for all TargetFrameworks breaks testing for `net6.0` consumers.

**Strategic Action:** Multi-targeted `Tedd.LinqUtils.Tests.csproj` with a conditional `ItemGroup` to use `Microsoft.NET.Test.Sdk` 17.1.0 and `xunit.runner.visualstudio` 2.4.3 for `net6.0`, while updating `net8.0`, `net9.0`, and `net10.0` to `Microsoft.NET.Test.Sdk` 18.9.0. Updated `xunit` and `coverlet.collector` across all target frameworks to their latest stable compatible versions.

## 2024-05-18 - Main Library Target Framework Modernization

**Observation:** `Tedd.LinqUtils` did not include `.NET 10.0` as a supported target framework, limiting its utility for downstream modern `.NET 10.0` consumers without falling back to older target frameworks.

**Strategic Action:** Added `net10.0` to the `TargetFrameworks` property in `Tedd.LinqUtils.csproj` to natively support `.NET 10.0` while maintaining compatibility for legacy environments (`netstandard2.1`, `net6.0`, `net8.0`, `net9.0`).
