using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template10.SampleData.Generator.Paragraphs
{
    public class Paragraph
    {
        internal Paragraph(string paragraph)
        {
            Text = paragraph?.Trim();
        }
        public string Text { get; }
        public override string ToString()
        {
            return Text;
        }
    }
}

