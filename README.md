# Restext Automatic Resource Generator

This repository contains the source code for a NuGet package that automatically
parses and generates strongly-typed code behind files for `*.restext` files that
contain string-based resources. It can also create strongly-typed code behind files
for `*.resx` files. It also automatically includes them in the build so that they are
compiled into binary `*.resources` files. Note that `*.restext` files can contain strings only.

## Using This Package

To use this package, add this text to your csproj file:
```xml
<ItemGroup>
  <PackageReference Include="Sunburst.ResourceGeneration" Version="3.0.0" />
</ItemGroup>
```

Then, mark the resource files that you want to have code-behinds generated for
with the `GenerateCodeBehind` metadata, like this:

```xml
<ItemGroup>
  <EmbeddedResource Include="Resources.resx" GenerateCodeBehind="true" />
  <EmbeddedResource Include="MoreResources.restext" GenerateCodeBehind="true" />
</ItemGroup>
```

This will instruct MSBuild to write the strongly-typed code behind files
into the IntermediateOutputPath during build. The files should be picked up
by Visual Studio automatically, for use while writing code. Note that if there
is already a strongly-typed code behind created by Visual Studio (it usually ends
in `.Designer.cs`), you will need to delete it first, or you will see
double-definition errors.

## Restext File Format

A `*.restext` file is a plain text file with the following format:

```text
# This is a comment.
# These comments will be emitted as doc comments into the code-behind.
Hello_String = Hello, World!

Another String=This is another string.
```

As the above example shows, you can use or omit spaces around the equals sign.
Furthermore, any comments (which can be delimited with either `;` or `#`) will
be emitted as doc comments for the next key-value line found. Note that blank
lines are ignored, which has the side-effect of merging any top-of-file
comments into the doc comment for the first key-value line in the file.

## License

This repository is licensed under the [MIT License](./LICENSE.md).
