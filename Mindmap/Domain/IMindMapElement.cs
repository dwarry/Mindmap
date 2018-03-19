using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

using ReactiveUI;

namespace Mindmap.Domain
{
    public interface IMindMapData: IReactiveObject
    {

        string Title { get; set; }

        ReactiveList<XElement> Content { get; set; }

        ReactiveList<IMindMapElement> Children { get; }

        int Depth { get; }
    }


    public interface IMindMapRoot : IMindMapData, IDisposable
    {
        string PageId { get; set; }

        ReactiveList<IMindMapElement> LeftChildren { get; }

        ReactiveList<IMindMapElement> RightChildren { get; } 

        IMindMapElement SelectedElement { get; set; }

        ReactiveCommand AddChild { get; }

        ReactiveCommand DeleteChild { get; }

        ReactiveCommand PromoteSelectedElement { get; }

        ReactiveCommand DemoteSelectedElement { get; }

        ReactiveCommand MoveUpSelectedElement { get; }


        ReactiveCommand MoveDownSelectedElement { get; }
    }

    public interface IMindMapElement : IMindMapData
    {
        IMindMapData Parent { get; }


        int Index { get; }


        string ObjectId { get; set; }
    }
}