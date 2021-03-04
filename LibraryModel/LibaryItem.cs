using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel
{
    [Serializable]
    public abstract class LibaryItem
    {
        public int CopyNumber { get; set; }
        public string ItemName { get; set; }       
        public DateTimeOffset ReleaseYear { get; set; }       
        public decimal Price { get; set; }
        public LibaryItem(string itemName, DateTimeOffset date, decimal price)
        {
            ItemName = itemName;
            ReleaseYear = date;
            Price = price;       
        }    
    }
}
