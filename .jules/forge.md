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

## 2026-06-17 - Tedd.LinqUtils Dependency Modernization

**Observation:**
- `Tedd.LinqUtils.Tests` uses an outdated xUnit structure (`xunit` 2.4.x, `xunit.runner.visualstudio` 2.4.x), an outdated version of `Microsoft.NET.Test.Sdk` (17.1.x), and `coverlet.collector` (3.1.2) for `net6.0`.
- `Tedd.LinqUtils.Tests` lacks tests running against `net10.0` but `Tedd.LinqUtils.Tests.csproj` has a typo `nnet8.0;net10.0`.
- The vulnerability report shows a high-severity transitive vulnerability for `Newtonsoft.Json` 9.0.1 (via `xunit.runner.visualstudio` 2.4.3 or `Microsoft.NET.Test.Sdk` 17.1.0) under the `net6.0` target.
- `Tedd.LinqUtils` targets `netstandard2.1;net6.0;net8.0;net9.0`. To explicitly support `net10.0`, the target framework `net10.0` was added to `Tedd.LinqUtils.csproj`.

**Strategic Action:**
- `Tedd.LinqUtils.Tests.csproj` target frameworks updated to `net6.0;net8.0;net9.0;net10.0`.
- Update `Tedd.LinqUtils.Tests.csproj` test dependencies:
  - `Microsoft.NET.Test.Sdk` to 17.11.1 for `net6.0` and 18.10.1 for others.
  - `xunit` to 2.9.3 for all.
  - `xunit.runner.visualstudio` to 2.8.2 for `net6.0` and 3.1.5 for others.
  - `coverlet.collector` to 6.0.2 for `net6.0` and 10.1.0 for others.
- `Tedd.LinqUtils.Benchmarks.csproj` updated `BenchmarkDotNet` to 0.14.0.
- `Tedd.LinqUtils.csproj` added `net10.0` target framework.
