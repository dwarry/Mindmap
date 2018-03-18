using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Microsoft.Office.Interop.OneNote;

using Mindmap.Domain;

using ReactiveUI;

namespace Mindmap.Persistence
{
    public interface ILoader
    {
        IMindMapRoot Load();
    }


    public class ActiveOneNotePageLoader : ILoader
    {
        private static readonly XNamespace
            OnenoteNamespace = "http://schemas.microsoft.com/office/onenote/2013/onenote";


        public IMindMapRoot Load()
        {
            var (pageId, doc) = GetCurrentPageData();

            IMindMapRoot result;

            if (doc != null)
            {
                result = ParsePageData(doc);
                result.PageId = pageId;
            }
            else
            {
                result = new MindMapRoot("", Enumerable.Empty<XElement>());
            }
            

            

            return result;
        }

        internal IMindMapRoot ParsePageData(XDocument doc)
        {
            var styles = doc.Root.Elements(OneNoteElements.QuickStyleDef)
                .ToDictionary(x => x.Attribute("index").Value, x => x.Attribute("name").Value);


            var headingStyles = styles.Where(x => x.Value.StartsWith("h"))
                .ToDictionary(x => x.Key, x => int.Parse(x.Value.Substring(1)));

            bool isHeading(XElement x)
            {
                var styleIndex = x.Attribute("quickStyleIndex")?.Value ?? "";

                return !headingStyles.ContainsKey(styleIndex);
            }

            var titleElement = doc.Root.Descendants().FirstOrDefault(x => x.Name == OneNoteElements.Title);

            var title = titleElement.Descendants().FirstOrDefault(x => x.Name == OneNoteElements.T).Value;

            var outline = doc.Root.Elements(OneNoteElements.Outline).FirstOrDefault();

            if (outline == null) return new MindMapRoot(title, Enumerable.Empty<XElement>());

            var children = outline.Elements(OneNoteElements.OeChildren).First().Elements().ToList();

            var rootContents = children.TakeWhile(isHeading);

            var root = new MindMapRoot(title, rootContents);

            var nodeData = ParseOutlineChildren(children.SkipWhile(x => !isHeading(x)), headingStyles);

            IMindMapData parent = root;

            var ancestors = new Stack<IMindMapData>();

            ancestors.Push(root);

            foreach (var (depth, nodeTitle, contents) in nodeData)
            {
                while (ancestors.Peek().Depth >= depth)
                {
                    parent = ancestors.Pop();
                }

                parent = ancestors.Peek();

                var element = new MindMapElement(nodeTitle, parent, new ReactiveList<XElement>(contents), depth);

                ancestors.Push(element);
            }

            return root;
        }


        private
            IEnumerable<(int indent, string title, IEnumerable<XElement> contents)> ParseOutlineChildren(
                IEnumerable<XElement> outlineChildren,
                IDictionary<string, int> headingIndexes)
        {
            var depth = 0;
            string title = null;
            var contents = new List<XElement>();

            foreach (var elem in outlineChildren)
            {
                var styleIndex = elem.Attribute("quickStyleIndex")?.Value;

                if (styleIndex != null && headingIndexes.ContainsKey(styleIndex))
                {
                    if (depth > 0)
                    {
                        yield return (depth, title, new List<XElement>(contents));
                    }

                    contents.Clear();

                    title = elem.Elements(OneNoteElements.T).First().Value;

                    depth = headingIndexes[styleIndex];
                }
                else
                {
                    var s = elem.Elements(OneNoteElements.T).FirstOrDefault()?.Value;
                    if (!string.IsNullOrWhiteSpace(s)) contents.Add(elem);
                }
            }

            yield return (depth, title, new List<XElement>(contents));
        }


        private (string pageId, XDocument doc) GetCurrentPageData()
        {
            var app = new Application2();

            var w = app.Windows.CurrentWindow;

            if (w == null) return (null,null);

            app.GetPageContent(w.CurrentPageId, out var xml);

            return (w.CurrentPageId, XDocument.Parse(xml));
        }


        private static class OneNoteElements
        {
            public static readonly XName QuickStyleDef = OnenoteNamespace + "QuickStyleDef";
            public static readonly XName Title = OnenoteNamespace + "Title";
            public static readonly XName Outline = OnenoteNamespace + "Outline";
            public static readonly XName Oe = OnenoteNamespace + "OE";
            public static readonly XName OeChildren = OnenoteNamespace + "OEChildren";
            public static readonly XName T = OnenoteNamespace + "T";
        }
    }
}