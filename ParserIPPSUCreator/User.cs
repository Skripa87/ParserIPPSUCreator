using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserIPPSUCreator
{
    public class User
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MidleName { get; set; }
        public string Gender { get; set; }
        public string Birthsday { get; set; }
        public List<string> MyMSZPs { get; set; }
        public User(string firstName, string secondName, string midleName, string gender, string birthsday)
        {
            FirstName = firstName;
            SecondName = secondName;
            MidleName = midleName;
            Gender = gender;
            Birthsday = birthsday;
            MyMSZPs = new List<string>();
        }
    }
}
