using LibraryDAL;
using LibraryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryBLL
{
    public class LibraryItemBLL
    {
        ItemRepository itemRepository;
        public LibraryItemBLL()
        {
            itemRepository = new ItemRepository();
        }
        public async Task Initialization()
        {
            await itemRepository.Open();
        }
        public List<LibaryItem> GetAllItems()
        {
            return itemRepository.GetAllItems().Result;
        }
        public List<Employee> GetWorkers()
        {
            return itemRepository.GetAllEmployees().Result;
        }
        public async Task ShutDown()
        {
         await itemRepository.UpdateAll();
        }
        public void AddItem(LibaryItem libaryItem)
        {
            itemRepository.Add(libaryItem);
        }
        public void RemoveItem(LibaryItem libaryItem)
        {
            itemRepository.Remove(libaryItem);
        }
        public int CalculateQuantity(string name, string writer, LibaryItem item)
        {
            var ListOfItems = GetAllItems();
            int copyNumber = 1;
            if (item.GetType() == typeof(Book))
            {
                for (int i = ListOfItems.Count - 1; i >= 0; i--)
                {
                    Book b = ListOfItems[i] as Book;
                    if (name == ListOfItems[i].ItemName && writer == b.WriterName)
                    {
                        copyNumber = ListOfItems[i].CopyNumber + 1;
                        return copyNumber;
                    }
                }
                return copyNumber;
            }
            if (item.GetType() == typeof(Journal))
            {
                for (int i = ListOfItems.Count - 1; i >= 0; i--)
                {
                    Journal b = ListOfItems[i] as Journal;
                    if (name == ListOfItems[i].ItemName && writer == b.Editor)
                    {
                        copyNumber = ListOfItems[i].CopyNumber + 1;
                        return copyNumber;
                    }
                }
                return copyNumber;
            }
            return copyNumber;
        }
        public Guid IsbnGenerator(string name, string writer)
        {
            var ListOfItems = GetAllItems();
            for (int i = ListOfItems.Count - 1; i > 0; i--)
            {
                Book b = ListOfItems[i] as Book;
                if (name == ListOfItems[i].ItemName && writer == b.WriterName)
                {
                    return b.ISBN;
                }
            }
            return Guid.NewGuid();
        }
    }
}
