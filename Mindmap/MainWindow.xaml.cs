using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;

using Mindmap.Domain;
using Mindmap.Persistence;

using ReactiveUI;
using ReactiveUI.Events;

namespace Mindmap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainWindowVM _vm = new MainWindowVM();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _vm;

            this.Events().Loaded.Subscribe(args =>
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(_leftTree);

                adornerLayer.Add(new MindMapLinkAdorner(_leftTree, MainLabel, true));

                adornerLayer = AdornerLayer.GetAdornerLayer(_rightTree);

                adornerLayer.Add(new MindMapLinkAdorner(_rightTree, MainLabel, false));
            });


            this.Events().Activated.Subscribe(_ => _vm.LoadMindMapFromCurrentlyActivePage());

            this.Events().Deactivated.Subscribe(_ => _vm.SaveMindMap());
        }


    }


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