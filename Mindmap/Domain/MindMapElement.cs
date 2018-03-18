using System;
using System.Diagnostics;
using System.Xml.Linq;

using ReactiveUI;

namespace Mindmap.Domain
{
    [DebuggerDisplay("MindMapElement({Title}, {Depth})")]
    public class MindMapElement : ReactiveObject, IMindMapElement
    {
        private ReactiveList<XElement> _content;

        private int _depth;

        private int _index;

        private string _title;

        public MindMapElement(string title, IMindMapData parent, ReactiveList<XElement> content, int depth)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));

            Content = content ?? throw new ArgumentNullException(nameof(content));

            _title = title ?? throw new ArgumentNullException(nameof(title));

            if (depth < 1) throw new ArgumentOutOfRangeException(nameof(depth), "must be positive");

            _depth = depth;

            _index = parent.Children.Count;

            parent.Children.Add(this);
        }

        public IMindMapData Parent { get; }


        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }


        public ReactiveList<XElement> Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }


        public int Depth
        {
            get => _depth;
            set => this.RaiseAndSetIfChanged(ref _depth, value);
        }


        public int Index
        {
            get => _index;
            set => this.RaiseAndSetIfChanged(ref _index, value);
        }


        public ReactiveList<IMindMapElement> Children { get; } = new ReactiveList<IMindMapElement>();

    }
}