using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel
{
    [Serializable]
    public class Journal : LibaryItem
    {
        public string Title { get;}       
        public string Editor;
     
        public Journal(string name , string title ,String editor , decimal price) : base(name, new DateTimeOffset(), price)
        {          
            Title = title;
            Editor = editor;       
        }      
        public override string ToString()
        {
            return $"Journal name:  {ItemName}   release date:{ReleaseYear.ToString("dd/MM/yyyy")}   price:{Price}   Editor:{Editor}   Copy Number: {CopyNumber}\nTitle: {Title}  ";      
        }
    }
}
