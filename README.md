# Restext Automatic Resource Generator

This repository contains the source code for a NuGet package that automatically
parses and generates strongly-typed code behinds for `*.restext` files that
contain string-based resources. It also automatically includes them in the
build so they are compiled into binary `*.resources` files (the same as with
resx files). Note that `*.restext` files can contain strings only.

## Using This Package

To use this package, add this text to your csproj file:
```xml
<ItemGroup>
  <PackageReference Include="Sunburst.ResourceGeneration" Version="1.0.0" />
</ItemGroup>
```

Note that this technique requires Visual Studio 2017 or Visual Studio for Mac;
if you are using an older version of Visual Studio, you will need to
search for **Sunburst.ResourceGeneration** in your IDE and have it add the
package to your csproj instead.

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
comments into the doc comment for hte first key-value line in the file.

## License

This repository is licensed under the [MIT License](./LICENSE.md).
