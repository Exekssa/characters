using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Characters
{
    internal class Character:IComparable<Character>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }
        public double Salary { get; set; }
        public string Hobby { get; set; }
        public string FavoriteAphorism { get; set; }

        public string ImagePath { get; set; }



        public Character()
        {
            Name= string.Empty;
            Surname= string.Empty;
            DateOfBirth= DateTime.MinValue;
            Profession= string.Empty;
            Salary= double.MinValue;
            Hobby= string.Empty;
            FavoriteAphorism= string.Empty;

        }
    


        public Character(string name, string surname, DateTime dateOfBirth, string profession, double salary, string hobby, string favoriteAphorism, string imagePath)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Profession = profession;
            Salary = salary;
            Hobby = hobby;
            FavoriteAphorism = favoriteAphorism;
            ImagePath = imagePath;
        }


        public override string ToString()
        {
           return $"Имя: {Name}" +
                  $"\nФамилия: {Surname}" +
                  $"\nДата рождения: {DateOfBirth.ToShortDateString()}" +
                  $"\nПрофессия: {Profession}" +
                  $"\nЗарплата: {Salary}" +
                  $"\nХобби: {Hobby}" +
                  $"\nЛюбимая цитата: {FavoriteAphorism}";
        }
   

        public int CompareTo(Character other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public static IComparer<Character> SortByName
        {
            get { return new SortName(); }
        }

        public static IComparer<Character> SortBySurname
        {
            get { return new SortSurname(); }
        }

        public static IComparer<Character> SortByYear
        {
            get { return new SortByYear(); }
        }

        public static IComparer<Character> SortByProfession
        {
            get { return new SortProfession(); }
        }
    }

   
}
