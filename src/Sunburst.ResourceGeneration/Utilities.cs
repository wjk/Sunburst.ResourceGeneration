// This file includes code from MSBuild and corefx.
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Sunburst.ResourceGeneration
{
    internal static class Utilities
    {
        private static readonly char[] CharsToReplace = new char[] { ' ',
        '\u00A0' /* non-breaking space */, '.', ',', ';', '|', '~', '@',
        '#', '%', '^', '&', '*', '+', '-', '/', '\\', '<', '>', '?', '[',
        ']', '(', ')', '{', '}', '\"', '\'', ':', '!' };
        private const char ReplacementChar = '_';

        public static string CreateValidCSharpIdentifier(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            foreach (char c in CharsToReplace)
            {
                input = input.Replace(c, ReplacementChar);
            }

            if (IsValidCSharpIdentifier(input)) return input;

            // If the identifier is invalid, try prepending an underscore.
            input = "_" + input;
            if (IsValidCSharpIdentifier(input)) return input;

            throw new BuildErrorException($"Cannot create a valid identifier for resource key '{input}'");
        }

        private static bool IsValidCSharpIdentifier(string input)
        {
            if (input.Length == 0 || input.Length > 511) return false;
            if (CSharpReservedWords.Contains(input)) return false;

            // Identifiers beginning with two underscores are reserved.
            if (input.Length > 3 && (input[0] == '_' && input[1] == '_' && input[2] != '_')) return false;

            if (!char.IsLetter(input[0]) && input[0] != '_') return false;

            return true;
        }

        private static readonly HashSet<string> CSharpReservedWords = new HashSet<string>()
        {
            "as", "do", "if", "in", "is", "for", "int", "new", "out", "ref", "try",
            "base", "bool", "byte", "case", "char", "else", "enum", "goto", "lock",
            "long", "null", "this", "true", "uint", "void", "break", "catch", "class",
            "const", "event", "false", "fixed", "float", "sbyte", "short", "throw",
            "ulong", "using", "where", "while", "yield", "double", "extern", "object",
            "params", "public", "return", "sealed", "sizeof", "static", "string",
            "struct", "switch", "typeof", "unsafe", "ushort", "checked", "decimal",
            "default", "finally", "foreach", "partial", "private", "virtual", "abstract",
            "continue", "delegate", "explicit", "implicit", "internal", "operator",
            "override", "readonly", "volatile", "__arglist", "__makeref", "__reftype",
            "interface", "namespace", "protected", "unchecked", "__refvalue", "stackalloc"
        };

        public static string CreateValidVisualBasicIdentifier(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (input.Length == 0) throw new BuildErrorException("Resource name cannot be blank");
            if (input.Length > 1023) throw new BuildErrorException("Resource name is too long (max 1023 chars)");

            foreach (char c in CharsToReplace)
            {
                input = input.Replace(c, ReplacementChar);
            }

            // Identifiers that are just a single underscore are reserved.
            if (input.Length == 1 && input[0] == '_') return "_" + input;

            // TODO: Check the identifier against a list of VB keywords here.
            return input;
        }
    }
}
