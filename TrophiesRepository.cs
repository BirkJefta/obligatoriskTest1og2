using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblOPGBirkTrophy {
    public class TrophiesRepository {
        private readonly List<Trophy> _trophies = new List<Trophy>();
        private int nextId = 1;

        public Trophy AddTrophy(Trophy trophy)
        {
            //tilføjer et trophy objekt til listen og returnerer det.
            if (trophy != null)
            {
                trophy.Id = nextId++;
                _trophies.Add(trophy);
                return trophy;
            }
            else
            {
                throw new ArgumentNullException("Trophy cannot be null");
            }

        }
        public IEnumerable<Trophy> Get(int? trophyYearBefore = null, int? trophyYearAfter = null, string? orderBy = null)
        {
            // Return a copy of the list with trophy objects.
            IEnumerable<Trophy> result = new List<Trophy>(_trophies);

            // Filter by year. Here you have the option to choose before or after a given year.
            if (trophyYearBefore != null)
            {
                result = result.Where(t => t.Year < trophyYearBefore);
            }

            if (trophyYearAfter != null)
            {
                result = result.Where(t => t.Year > trophyYearAfter);
            }

            // Sort by competition or year.
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "competition": // fall through to next case
                    case "competition_asc":
                        result = result.OrderBy(t => t.Competition);
                        break;
                    case "competition_desc":
                        result = result.OrderByDescending(t => t.Competition);
                        break;
                    case "year_asc":
                        result = result.OrderBy(t => t.Year);
                        break;
                    case "year_desc":
                        result = result.OrderByDescending(t => t.Year);
                        break;

                }
            }

            return result;
        }


        public Trophy? GetTrophyById(int id)
        {
            Trophy? trophyfound = _trophies.Find(t => t.Id == id);
            if (trophyfound == null)
            {
                throw new ArgumentException($"Trophy with {id} not found");
            }
            return trophyfound;
        }


    }
}
