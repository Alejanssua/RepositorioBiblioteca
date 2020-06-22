using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUBiblioteca.Transactions
{
    public class LibroBLL
    {

        public static void Create(Libro a)
        {
            using (DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Libroes.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static Libro Get(int? id)
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Libroes.Find(id);
        }

        public static void Update(Libro libro)
        {
            using (DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Libroes.Attach(libro);
                        db.Entry(libro).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static void Delete(int? id)
        {
            using (DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Libro libro = db.Libroes.Find(id);
                        db.Entry(libro).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static List<Libro> List()
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Libroes.ToList();
        }

        //-----------------------------------------------------------------------------------------------------------
        private static List<Libro> GetLibros(string criterio)
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Libroes.Where(x => x.titulo.ToLower().Contains(criterio)).ToList();
        }

        private static Libro GetLibro(string titulo)
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Libroes.FirstOrDefault(x => x.titulo == titulo);
        }

    }
}
