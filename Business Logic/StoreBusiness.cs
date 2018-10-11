
using Data_Access;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class StoreBusiness
    {
        public static List<Store> GetStore()
        {
            StoreData obj = new StoreData();
            return obj.GetStore();
        }

        public static Store GetStoreById(ref int id)
        {
            StoreData store = new StoreData();
            return store.GetStoreById(id);
        }

        public static string AddStore(Store artic)
        {
            StoreData store = new StoreData();
            return store.AddStore(ref artic);
        }

        public static string EditStore(ref Store artic)
        {
            StoreData store = new StoreData();
            return store.EditStore(ref artic);
        }

        public static string DeleteStore(int id)
        {
            StoreData store = new StoreData();
            return store.DeteleStore(id);
        }
    }
}
