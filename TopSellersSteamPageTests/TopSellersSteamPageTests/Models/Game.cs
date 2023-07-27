using System;

namespace TopSellersSteamPageTests
{
    public class Game
    {
        public string _name;
        public string _releaseDate;
        public string _price;

        public Game(string name, string releaseDate, string price)
        {
            _name = name;
            _releaseDate = releaseDate;
            _price = price;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Game))
            {
                return false;
            }

            Game other = (Game)obj;

            // Сравниваем все три поля для определения эквивалентности объектов
            return string.Equals(_name, other._name) && string.Equals(_price, other._price) && string.Equals(_releaseDate, other._releaseDate);
        }

        public override int GetHashCode()
        {
            // Возвращает хэш-код, основанный на всех трех полях
            return (_name?.GetHashCode() ?? 0) ^ (_price?.GetHashCode() ?? 0) ^ (_releaseDate?.GetHashCode() ?? 0);
        }
    }
}
