using System.Xml.Linq;

namespace Mindmap.Persistence
{
    public static class OneNoteElements
    {
        public static readonly XNamespace OneNoteNamespace = "http://schemas.microsoft.com/office/onenote/2013/onenote";

        public static readonly XName QuickStyleDef = OneNoteNamespace + "QuickStyleDef";
        public static readonly XName Title = OneNoteNamespace + "Title";
        public static readonly XName Outline = OneNoteNamespace + "Outline";
        public static readonly XName Oe = OneNoteNamespace + "OE";
        public static readonly XName OeChildren = OneNoteNamespace + "OEChildren";
        public static readonly XName T = OneNoteNamespace + "T";
    }


    public static class OneNoteAttributes
    {
        public const string Index = "index";

        public const string Name = "name";
        
        public const string QuickStyleIndex = "quickStyleIndex";

        public const string FontColor = "fontColor";

        public const string HighlightColor = "highlightColor";

        public const string Font = "font";

        public const string FontSize = "fontSize";

        public const string SpaceBefore = "spaceBefore";

        public const string SpaceAfter = "spaceAfter";

        public const string ObjectId = "objectId";

    }
}