using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Simplification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWrapping
{
    public static class RoslynStringExtensions
    {
        public static IdentifierNameSyntax ToIdentifierName(this string identifier)
        {
            return SyntaxFactory.IdentifierName(identifier.ToIdentifierToken(false));
        }

        public static string EscapeIdentifier(this string identifier, bool isQueryContext = false)
        {
            int index = identifier.IndexOf('\0');
            if (index >= 0)
            {
                identifier = identifier.Substring(0, index);
            }
            if ((SyntaxFacts.GetKeywordKind(identifier) <= SyntaxKind.None) && (!isQueryContext || !SyntaxFacts.IsQueryContextualKeyword(SyntaxFacts.GetContextualKeywordKind(identifier))))
            {
                return identifier;
            }
            return ('@' + identifier);
        }

        public static SyntaxToken ToIdentifierToken(this string identifier, bool isQueryContext = false)
        {
            string text = identifier.EscapeIdentifier(isQueryContext);
            if ((text.Length == 0) || (text[0] != '@'))
            {
                return SyntaxFactory.Identifier(text);
            }
            string valueText = identifier.StartsWith("@") ? identifier.Substring(1) : identifier;
            SyntaxTriviaList leading = new SyntaxTriviaList();
            SyntaxToken token = SyntaxFactory.Identifier(leading, SyntaxKind.None, '@' + valueText, valueText, new SyntaxTriviaList());
            if (!identifier.StartsWith("@"))
            {
                SyntaxAnnotation[] annotations = new SyntaxAnnotation[] { Simplifier.Annotation };
                token = token.WithAdditionalAnnotations(annotations);
            }
            return token;
        }

    }
}
