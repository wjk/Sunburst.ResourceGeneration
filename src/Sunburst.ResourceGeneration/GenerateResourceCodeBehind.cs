using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Sunburst.ResourceGeneration.ResourceModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Sunburst.ResourceGeneration
{
    public class GenerateResourceCodeBehind : Task
    {
        [Required]
        public ITaskItem ResourceXml { get; set; }
        [Required]
        public ITaskItem OutputFile { get; set; }
        public string RootNamespace { get; set; }

        public override bool Execute()
        {
            ResourceMap map = null;

            try
            {
                using (Stream stream = File.OpenRead(ResourceXml.GetMetadata("FullPath")))
                {
                    XElement elem = XElement.Load(stream);
                    map = ResourceMap.ReadXml(elem);
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.LogError("Invalid resource XML file: {0}", ex.Message);
                return false;
            }

            var properties = map.Resources.Select(res => MakeProperty(res));

            var syntax = CompilationUnit().WithMembers(
                MakeSyntaxList(
                    NamespaceDeclaration(GenericName(map.Namespace ?? RootNamespace)).WithMembers(
                        ClassDeclaration(map.ClassName ?? ResourceXml.GetMetadata("FileName")).WithMembers(

                        )
                    )
                )
            );

            throw new NotImplementedException();
        }

        private static PropertyDeclarationSyntax MakeProperty(Resource res)
        {
            return PropertyDeclaration(ParseCustomTypeName(res.CustomType));
        }

        private static NameSyntax ParseCustomTypeName(string xmlName)
        {
            if (xmlName.Equals("string", StringComparison.InvariantCultureIgnoreCase))
                return QualifiedName(IdentifierName("System"), IdentifierName("String"));
            else if (xmlName.Equals("stream", StringComparison.InvariantCultureIgnoreCase))
                return ParseCustomTypeName("System.IO.Stream");

            List<string> parts = xmlName.Split('.').ToList();
            NameSyntax retval = IdentifierName(parts[0]); parts.RemoveAt(0);
            while (parts.Count != 0)
            {
                retval = QualifiedName(retval, IdentifierName(parts[0]));
                parts.RemoveAt(0);
            }

            return retval;
        }

        private static SyntaxList<SyntaxNode> MakeSyntaxList(params SyntaxNode[] args) => new SyntaxList<SyntaxNode>(args);
    }
}
