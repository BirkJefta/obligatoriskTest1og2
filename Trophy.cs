namespace OblOPGBirkTrophy
{
    public class Trophy
    {
        public int Id { get; set; }
        string _competition;
        int _year;

        //competition skal være mere end 3 tegn og ikke være null.
        public string Competition
        {
            get => _competition;
            set {
                if (value == null) 
                {
                    throw new ArgumentNullException("Competition cannot be null");
                }
                else if (value.Trim().Length < 3)
                {
                    throw new ArgumentException("Competition must be more than 3 characters");
                }
                _competition = value;
            }
        }
        //year skal være større eller lig med 1970 og mindre eller lig med 2025.
        public int Year
        {
            get => _year;
            set {
                if (value < 1970 || value > 2025)
                {
                    throw new ArgumentOutOfRangeException("Year must be between 1970 and 2015");
                }
                _year = value;
            }
        }


        public Trophy(int id, string competition, int year)
        {
            Id = id;
            Competition = competition;
            Year = year;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Competition: {_competition}, Year: {_year}";
        }
    }
}
