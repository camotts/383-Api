using GamingStoreAPI.DAL;
using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
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
    }
}