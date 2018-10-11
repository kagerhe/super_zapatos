using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class StoreData
    {
        ConexionB DB = new ConexionB();

        public List<Store> GetStore()
        {
            using (DB)
            {
                return DB.Stores.ToList();
            }
        }

        public Store GetStoreById(int id)
        {
            using (DB)
            {
                //DUDA
                return DB.Stores.FirstOrDefault(x => x.Id == id);
            }
        }

        //Agregar store
        public string AddStore(ref Store store)
        {
            try
            {
                using (DB) { 
                DB.Stores.Add(store);
                DB.SaveChanges();
            }
                return "Store added successfully";
            }
            catch (Exception)
            {
                return "Error adding new store";
            }
        }

        //permite modificar 
        public string EditStore(ref Store store)
        {
            try
            {
                using (DB)
                {
                    DB.Entry(store).State = EntityState.Modified;
                    DB.SaveChanges();
                }
                return ("Store"
                    + store.Name + " successfully edited");
            }
            catch (Exception)
            {
                return ("Error edited Store" + store.Name);
            }
        }

        //Eliminar
        public string DeteleStore(int id)
        {
            Store store = null;
            try
            {
                using (DB)
                {
                    store = DB.Stores.FirstOrDefault(x => x.Id == id);
                    DB.Stores.Remove(store);
                    DB.SaveChanges();
                }
                return ("successfully"
                    + store.Name + " removed article");
            }
            catch (Exception)
            {
                return ("Error removed article" + store.Name);
            }
        }

    }
}
