using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Get Page

            // Merge QuickStyleDefs

            // Rewrite contents of first Outline


        }
    }
}
