using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserIPPSUCreator
{
    public class User:IComparable, IGrouping<string,User>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MidleName { get; set; }
        public string Gender { get; set; }
        public string Birthsday { get; set; }
        public List<string> MyMSZPs { get; set; }
        public string Key => CalcValueMSZPs();

        private string CalcValueMSZPs()
        {
            string bufer = "";
            foreach(var m in MyMSZPs)
            {
                bufer += m;
            }
            return bufer;
        }
        public User(string firstName, string secondName, string midleName, string gender, string birthsday)
        {
            FirstName = firstName;
            SecondName = secondName;
            MidleName = midleName;
            Gender = gender;
            Birthsday = birthsday;
            MyMSZPs = new List<string>();
        }                
        public override bool Equals(object other)
        {
            if(this.GetType() != other.GetType())
            {
                return false;
            }
            if ((this.FirstName == ((User)other).FirstName) && (this.MidleName == ((User)other).MidleName)
            && (this.SecondName == ((User)other).SecondName) && (this.Gender == ((User)other).Gender) && (this.Birthsday == ((User)other).Birthsday))
            {
                return true;
            }
            return false;
        }
        public int CompareTo(object other)
        {
            string a, b;
            a = this.CalcValueMSZPs();
            b = ((User)other).CalcValueMSZPs();
            return b.CompareTo(a); 
        }

        public IEnumerator<User> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
