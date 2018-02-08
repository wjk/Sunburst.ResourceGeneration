// This file includes code from MSBuild <https://github.com/Microsoft/msbuild>.
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Sunburst.ResourceGeneration
{
    internal static class Utilities
    {
        private static readonly char[] CharsToReplace = new char[] { ' ',
        '\u00A0' /* non-breaking space */, '.', ',', ';', '|', '~', '@',
        '#', '%', '^', '&', '*', '+', '-', '/', '\\', '<', '>', '?', '[',
        ']', '(', ')', '{', '}', '\"', '\'', ':', '!' };
        private const char ReplacementChar = '_';

        public static string CreateValidIdentifier(string input, CodeDomProvider codeProvider)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (codeProvider == null) throw new ArgumentNullException(nameof(input));

            foreach (char c in CharsToReplace)
            {
                input = input.Replace(c, ReplacementChar);
            }

            if (codeProvider.IsValidIdentifier(input)) return input;

            // Now try fixing up keywords like "for".
            input = codeProvider.CreateValidIdentifier(input);
            if (codeProvider.IsValidIdentifier(input)) return input;

            // Now try prepending an underscore. This fixes keys that start with a number.
            input = "_" + input;
            if (codeProvider.IsValidIdentifier(input)) return input;

            throw new BuildErrorException($"Cannot create a valid identifier for resource key '{input}'");
        }
    }
}
