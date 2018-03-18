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

using ReactiveUI.Events;

namespace Mindmap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainWindowVM _vm = new MainWindowVM();

        private  MindMapLinkAdorner _leftAdorner;

        private  MindMapLinkAdorner _rightAdorner;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _vm;

            this.Events().Loaded.Subscribe(args =>
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(_leftTree);

                _leftAdorner = new MindMapLinkAdorner(_leftTree, MainLabel, true);

                adornerLayer.Add(_leftAdorner); 

                adornerLayer = AdornerLayer.GetAdornerLayer(_rightTree);

                adornerLayer.Add(new MindMapLinkAdorner(_rightTree, MainLabel, false));
            });


            this.Events().Activated.Subscribe(_ =>
            {
                _vm.LoadMindMapFromCurrentlyActivePage();
                _leftTree.InvalidateVisual();
                _rightTree.InvalidateVisual();
            });

            this.Events().Deactivated.Subscribe(_ => _vm.SaveMindMap());
        }


    }
}