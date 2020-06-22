using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUBiblioteca.Transactions
{
    public class VideoBLL
    {

        public static void Create(Video a)
        {
            using (DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Videos.Add(a);
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

        public static Video Get(int? id)
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Videos.Find(id);
        }

        public static void Update(Video video)
        {
            using (DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Videos.Attach(video);
                        db.Entry(video).State = System.Data.Entity.EntityState.Modified;
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
                        Video video = db.Videos.Find(id);
                        db.Entry(video).State = System.Data.Entity.EntityState.Deleted;
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

        public static List<Video> List()
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Videos.ToList();
        }

        //-----------------------------------------------------------------------------------------------------------
        private static List<Video> GetVideos(string criterio)
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Videos.Where(x => x.titulo.ToLower().Contains(criterio)).ToList();
        }

        private static Video GetVideo(string titulo)
        {
            DBBIBLIOTECAEntities db = new DBBIBLIOTECAEntities();
            return db.Videos.FirstOrDefault(x => x.titulo == titulo);
        }

    }
}
