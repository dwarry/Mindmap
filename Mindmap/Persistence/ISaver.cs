using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.Office.Interop.OneNote;

using Mindmap.Domain;

namespace Mindmap.Persistence
{
    public interface ISaver
    {
        void Save(IMindMapRoot root);
    }


    public class OneNotePageSaver : ISaver
    {
        public void Save(IMindMapRoot root)
        {
            var app = new Application2();

            // Get Page
            app.GetPageContent(root.PageId, out var xml);

            if (xml == null)
            {
                return;
            }

            var doc = XDocument.Parse(xml);

            // Merge QuickStyleDefs
            var headingIndexByDepth = MergeQuickStyleDefs(doc, root);

            // Rewrite contents of first Outline
            UpdateOutline(doc, root, headingIndexByDepth);

            app.UpdatePageContent(doc.ToString(SaveOptions.OmitDuplicateNamespaces));
        }

        private IDictionary<int, int> MergeQuickStyleDefs(XDocument doc, IMindMapRoot root)
        {
            var existingStyles = doc.Elements(OneNoteElements.QuickStyleDef);

            var styles = existingStyles
                .ToDictionary(x => Int32.Parse(x.Attribute(OneNoteAttributes.Index)?.Value),
                              x => x.Attribute(OneNoteAttributes.Name)?.Value);

            var maxIndex = styles.Keys.Max();

            var headingStyles = styles.Where(x => x.Value.StartsWith("h")).ToDictionary(
                x => Int32.Parse(x.Value.Substring(1)),
                x => x.Key
            );


            void addStyle(StyleData style)
            {
                XElement newStyle = new XElement(OneNoteElements.QuickStyleDef);
                newStyle.Add(new XAttribute(OneNoteAttributes.Index, ++maxIndex));
                newStyle.Add(new XAttribute(OneNoteAttributes.Name, style.Name));
                newStyle.Add(new XAttribute(OneNoteAttributes.FontColor, style.FontColor));
                newStyle.Add(new XAttribute(OneNoteAttributes.HighlightColor, "automatic"));
                newStyle.Add(new XAttribute(OneNoteAttributes.Font, "Calibri"));
                newStyle.Add(new XAttribute(OneNoteAttributes.FontSize, style.FontSize));
                newStyle.Add(new XAttribute(OneNoteAttributes.SpaceBefore, "0.0"));
                newStyle.Add(new XAttribute(OneNoteAttributes.SpaceAfter, "0.0"));

                existingStyles.Last().AddAfterSelf(newStyle);

            }

            for (int i = 1; i <= 6; ++i)
            {
                if (!headingStyles.ContainsKey(i))
                {
                    var style = HeadingStyles[i];
                    addStyle(style);
                    headingStyles[i] = maxIndex;
                }
            }

            return headingStyles;
        }

        private void UpdateOutline(XDocument doc, IMindMapRoot root, IDictionary<int, int> headingIndexByDepth)
        {
            var outline = doc.Element(OneNoteElements.Outline);

            if (outline == null)
            {
                outline = new XElement(OneNoteElements.Outline, new XElement(OneNoteElements.OeChildren));
                doc.Root.Elements().Last().AddAfterSelf(outline);
            }

            var oes = 
            
        //}Element(OneNoteElements.OeChildren)
        //        ?.Elements(OneNoteElements.Oe)
        //        ?? Enumerable.Empty<XElement>();


        }


        private class StyleData
        {
            public StyleData(string name, string fontColor, string fontSize, bool italic)
            {
                Name = name;
                FontColor = fontColor;
                FontSize = fontSize;
                Italic = italic;
            }

            public string Name { get; }

            public string FontColor { get; }

            public string FontSize { get; }

            public bool Italic { get; }

        }

        private readonly StyleData[] HeadingStyles = new[]
        {
            null,
            new StyleData("h1", "#1E4E79", "16.0", false), 
            new StyleData("h2", "#2E75B5", "14.0", false), 
            new StyleData("h3", "#5B9BD5", "12.0", false), 
            new StyleData("h4", "#5B9BD5", "12.0", true), 
            new StyleData("h5", "#2E75B5", "11.0", false), 
            new StyleData("h6", "#2E75B5", "11.0", true)
        };


    }
}
