using GamingStoreAPI.DAL;
using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    public class TagRepository:ITagRepository
    {
        private DataContext db = new DataContext();

        public IEnumerable<Tags> getListOfTags()
        {
            return db.Tags.AsEnumerable();
        }


        public Models.Tags getTagById(int Id)
        {
            Tags searchTag = db.Tags.Find(Id);
            return searchTag;
        }
        public void createTag(Models.Tags tag)
        {
            db.Tags.Add(tag);
            db.SaveChanges();
        }


        public void putTag(int id, Models.Tags tag)
        {
            Tags modifyTag = getTagById(id);
            modifyTag.Name = tag.Name;
           

            db.Entry(modifyTag).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void deleteTag(Models.Tags tag)
        {
            db.Tags.Remove(tag);
            db.SaveChanges();

        }
    }
}