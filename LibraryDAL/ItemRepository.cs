using LibraryModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace LibraryDAL
{
    public class ItemRepository
    {   //Books
        const string _itemDir = "BooksOfLibrary";
        string filePathItems;       
        List<LibaryItem> ListOfItems;
        StorageFile fileItems;

        //Employees
        const string _employeeDir = "EmployeesOfLibrary";
        string filePathEmployees;
        List<Employee> ListOfEmployees;
        StorageFile fileEmplyees;

        public ItemRepository()
        {
            filePathItems = Path.Combine(_itemDir, "StoreItemData.Johnny");
            filePathEmployees = Path.Combine(_employeeDir, "StoreEmployeesData.Johnny");
            ListOfEmployees = new List<Employee>();
            ListOfItems = new List<LibaryItem>();
            ListOfEmployees.Add(new Employee("yoni","yoni","yoni", true));
        }
        public async Task Open()
        {
            try
            {
                fileItems = await ApplicationData.Current.LocalFolder.GetFileAsync(filePathItems);
            }
            catch
            {
                fileItems = await ApplicationData.Current.LocalFolder.CreateFileAsync(filePathItems);
            }
            try
            {
                fileEmplyees = await ApplicationData.Current.LocalFolder.GetFileAsync(filePathEmployees);
            }
            catch
            {
                fileEmplyees = await ApplicationData.Current.LocalFolder.CreateFileAsync(filePathEmployees);
            }        
        }
        private async Task ReadItems()
        {
            using (IRandomAccessStream stream = await fileItems.OpenAsync(FileAccessMode.ReadWrite))
            {
                var binFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (Stream streamNew = stream.AsStreamForWrite())
                {
                    if (streamNew.Length > 0)
                    {                      
                      ListOfItems = (List<LibaryItem>)binFormatter.Deserialize(streamNew);
                    }                
                }
            }
        }
        private async Task ReadEmployees()
        {
            Thread.Sleep(2000);
            using (IRandomAccessStream stream = await fileEmplyees.OpenAsync(FileAccessMode.ReadWrite))
            {
                var binFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (Stream streamNew = stream.AsStreamForWrite())
                {
                    if (streamNew.Length > 0)
                    {
                       ListOfEmployees = (List<Employee>)binFormatter.Deserialize(streamNew);
                    }
                }
            }
        }
        public async void UpdateAndClose(StorageFile storageFile, object obj)
        {
            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var binFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (Stream streamNew = stream.AsStreamForWrite())
                {
                    if (obj is List<LibaryItem>) binFormatter.Serialize(streamNew, ListOfItems);
                    else throw new ArgumentException("invalid list");

                    if (obj is List<Employee>) binFormatter.Serialize(streamNew, ListOfEmployees);
                    else throw new ArgumentException("invalid list");
                }
            }
        }
        public async void Add(Employee employee)
        {
            try { ListOfEmployees.Add(employee); await UpdateAll(); }
            catch { throw new Exception(); }
        }
        public async void Add(LibaryItem libaryItem)
        {
            try { ListOfItems.Add(libaryItem); await UpdateAll(); }
            catch { throw new Exception(); }
        }
        public void Remove(Employee employee)
        {
            try { ListOfEmployees.Remove(employee); }
            catch { throw new Exception(); }
        }
        public void Remove(LibaryItem libaryItem)
        {
            try { ListOfItems.Remove(libaryItem); }
            catch { throw new Exception(); }
        }
        public async Task<List<LibaryItem>> GetAllItems()
        {
            await ReadItems();
            return ListOfItems;
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            await ReadEmployees();
            return ListOfEmployees;
        }
        public async Task UpdateAll()
        {
            await ReadItems();
            await ReadEmployees();
        }

    }
}
