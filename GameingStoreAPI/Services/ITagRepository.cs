using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    interface ITagRepository
    {
        IEnumerable<Tags> getListOfTags();
        Tags getTagById(int id);
    }
}