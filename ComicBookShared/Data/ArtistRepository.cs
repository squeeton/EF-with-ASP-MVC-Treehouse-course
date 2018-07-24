using ComicBookShared.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public class ArtistRepository : BaseRepository<Artist>
    {
        public ArtistRepository(Context context)
            :base(context)
        {

        }

        public override IList<Artist> GetList()
        {
            return Context.Artists
                .OrderBy(a=>a.Name)
                .ToList();
        }

        public override Artist Get(int id, bool includeRelatedEntities = true)
        {
            var artist = Context.Artists.AsQueryable();

            if (includeRelatedEntities)
            {
                artist = artist
                    .Include(a => a.ComicBooks.Select(s => s.ComicBook.Series))
                    .Include(a => a.ComicBooks.Select(r => r.Role));
            }
            return artist
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public Boolean isArtistUnique(int artistId, string name)
        {
            return Context.Artists
                .Any(a =>a.Name == name && a.Id !=artistId);
        }

    }
}
