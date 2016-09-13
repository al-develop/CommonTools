using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendIfNotNullOrWhitespace(this StringBuilder stringBuilder, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.RemoveLastNewLine();
                stringBuilder.Append(value);
            }
            return stringBuilder;
        }

        public static StringBuilder AppendLineIfNotNullOrWhitespace(this StringBuilder stringBuilder, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.RemoveLastNewLine();
                stringBuilder.AppendLine(value);
            }
            return stringBuilder;
        }
    }
}
