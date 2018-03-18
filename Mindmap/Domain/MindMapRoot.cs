using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using ReactiveUI;

namespace Mindmap.Domain
{
    [DebuggerDisplay("MindMapRoot({Title})")]
    public class MindMapRoot : ReactiveObject, IMindMapRoot
    {
        private List<IDisposable> _subscriptions = new List<IDisposable>();

        public MindMapRoot(string title, IEnumerable<XElement> content)
        {
            Title = title;

            Content = new ReactiveList<XElement>(content);

            var subscription = Children.Changed.Subscribe(x =>
            {
                using (var leftSupression = LeftChildren.SuppressChangeNotifications())
                using (var rightSupression = RightChildren.SuppressChangeNotifications())
                {
                    LeftChildren.Clear();
                    RightChildren.Clear();

                    int halfway = Children.Count / 2;

                    for (int i = 0; i < Children.Count; i++)
                    {
                        var mindMapElement = Children[i];

                        if (i <= halfway)
                        {
                            RightChildren.Add(mindMapElement);
                        }
                        else
                        {
                            LeftChildren.Add(mindMapElement);
                        }
                    }
                }
            });

            _subscriptions.Add(subscription);

            var hasSelectedItem = this.WhenAnyValue(x => x.SelectedElement).Select(x => x != null);

            AddChild = ReactiveCommand.Create(DoAddChild);
            DeleteChild = ReactiveCommand.Create(DoDeleteChild, hasSelectedItem);

            var selectedElementDepth = this.WhenAnyValue(x => x.SelectedElement).Select(x => x?.Depth ?? -1);

            var canPromote = selectedElementDepth.Select(x => x > 1);
            
            PromoteSelectedElement = ReactiveCommand.Create(DoPromoteSelectedElement, canPromote);

            var canDemote = this.WhenAnyValue(x => x.SelectedElement).Select(x =>
            {
                if (x == null)
                {
                    return false;
                }

                return (x.Index > 0 && x.Index < x.Parent.Children.Count - 1);
            });

            DemoteSelectedElement = ReactiveCommand.Create(DoDemoteSelectedElement, canDemote);

            var selectedElementIndex = this.WhenAnyValue(x => x.SelectedElement).Select(x => x?.Index ?? -1);

            var canMoveUp = selectedElementIndex.Select(x => x > 0);

            MoveUpSelectedElement = ReactiveCommand.Create(DoMoveUpSelectedElement, canMoveUp);

            var canMoveDown = selectedElementIndex.Select(x => x != -1 && SelectedElement.Parent.Children.Count > x);

            MoveDownSelectedElement = ReactiveCommand.Create(DoMoveDownSelectedElement, canMoveDown);

        }


        #region IDisposable implementation

        private bool _disposed = false;

        ~MindMapRoot()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;

            }

            if (disposing)
            {
                foreach(var disp in _subscriptions) { disp.Dispose(); }
            }

            _disposed = true;
        }

        #endregion

        private string _pageId;


        public string PageId
        {
            get { return _pageId; }
            set { this.RaiseAndSetIfChanged(ref _pageId, value); }
        }

        private string _title;


        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }


        public int Depth => 0;

        public ReactiveList<XElement> Content { get; set; }

        public ReactiveList<IMindMapElement> Children { get; } = new ReactiveList<IMindMapElement>();

        public ReactiveList<IMindMapElement> LeftChildren { get; } = new ReactiveList<IMindMapElement>();

        public ReactiveList<IMindMapElement> RightChildren { get; } = new ReactiveList<IMindMapElement>();

        private IMindMapElement _selectedElement;


        public IMindMapElement SelectedElement
        {
            get => _selectedElement;
            set => this.RaiseAndSetIfChanged(ref _selectedElement, value);
        }


        public ReactiveCommand AddChild { get; }

        private void DoAddChild()
        {
        }


        public ReactiveCommand DeleteChild { get; }

        private void DoDeleteChild()
        {
        }

        public ReactiveCommand PromoteSelectedElement { get; }

        private void DoPromoteSelectedElement()
        {

        }

        public ReactiveCommand DemoteSelectedElement { get; }

        private void DoDemoteSelectedElement()
        {
        }

        public ReactiveCommand MoveUpSelectedElement { get; }

        private void DoMoveUpSelectedElement()
        {
        }

        public ReactiveCommand MoveDownSelectedElement { get; }

        private void DoMoveDownSelectedElement()
        {

        }
}
}