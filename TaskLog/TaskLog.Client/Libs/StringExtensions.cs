using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorCode.Styling;
using Markdig;
using Markdig.Extensions;
using Microsoft.AspNetCore.Components;

namespace TaskLog.Client.Libs
{
    public static class StringExtensions
    {
        public static string ToMarkdown(this string content)
        {
            var pipline = new MarkdownPipelineBuilder().UseAdvancedExtensions()
                                                       .UseSyntaxHighlighting()
                                                       .Build();
            return Markdown.ToHtml(content, pipline);
        }

        public static MarkupString ToMarkup(this string content)
            => (MarkupString)content;
    }
}
