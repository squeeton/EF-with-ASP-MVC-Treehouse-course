using ComicBookShared.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public class ComicBookSeriesRepository : BaseRepository<Series>
    {
        public ComicBookSeriesRepository(Context context)
            : base(context)
        {


        }

        public override Series Get(int id, bool includeRelatedEntities = true)
        {
            var series = Context.Series.AsQueryable();

            if (includeRelatedEntities)
            {
                series = series
                        .Include(s=>s.ComicBooks);
            }
            return series
                .Where(cb => cb.Id == id)
                .SingleOrDefault();
        }

        public override IList<Series> GetList()
        {
            return Context.Series
                .OrderBy(s=>s.Title)
                .ToList();
        }

        public Boolean HasTitleBeenUsed(int seriedID, string title)
        {
            return Context.Series
                .Any(s => s.Id == seriedID && s.Title == title);
        }
    }
}
