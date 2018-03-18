using Mindmap.Domain;
using Mindmap.Persistence;

using ReactiveUI;

namespace Mindmap
{
    public class MainWindowVM : ReactiveObject
    {
        private readonly ILoader _loader;
        private readonly ISaver _saver;

        public MainWindowVM(ILoader loader = null, ISaver saver = null)
        {
            _loader = loader ?? new ActiveOneNotePageLoader();
            _saver = saver ?? new OneNotePageSaver();
        }

        private IMindMapRoot _root;


        public IMindMapRoot Root
        {
            get { return _root; }
            set { this.RaiseAndSetIfChanged(ref _root, value); }
        }

        public void LoadMindMapFromCurrentlyActivePage()
        {
            Root = _loader.Load();
        }

        public void SaveMindMap()
        {
            _saver.Save(Root);
        }
    }
}