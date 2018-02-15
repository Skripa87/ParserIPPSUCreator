using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParserIPPSUCreator;
using System.IO;
using System.Xml;

namespace ParserIPPSUCreator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<User> users = new List<User>();
            var files = Directory.GetFiles("d:\\data", "*.xml");
            int count = 0;
            foreach (var f in files)
            {
                if (f != null)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(f);
                    XmlElement root = xDoc.DocumentElement;
                    XmlElement elements = root.GetElementsByTagName("elements").Cast<XmlElement>().FirstOrDefault();
                    XmlNodeList nodesf = root.GetElementsByTagName("fact");
                    List<XmlNode> facts = new List<XmlNode>();
                    foreach (XmlNode n in nodesf)
                    {
                        facts.Add(n);
                    }
                    foreach (XmlNode fact in facts)
                    {
                        XmlNode xUser = ((XmlElement)fact).GetElementsByTagName("F:MSZ_receiver").Cast<XmlNode>().FirstOrDefault();
                        string fName = ((XmlElement)xUser).GetElementsByTagName("FamilyName").Cast<XmlNode>().FirstOrDefault().InnerText;
                        string sName = ((XmlElement)xUser).GetElementsByTagName("FirstName").Cast<XmlNode>().FirstOrDefault().InnerText;
                        string mName = ((XmlElement)xUser).GetElementsByTagName("Patronymic").Cast<XmlNode>().FirstOrDefault().InnerText;
                        string gName = ((XmlElement)xUser).GetElementsByTagName("Gender").Cast<XmlNode>().FirstOrDefault().InnerText;
                        string dName = ((XmlElement)xUser).GetElementsByTagName("BirthDate").Cast<XmlNode>().FirstOrDefault().InnerText;
                        count++;
                        if (users == null || users.Select(u => u.Id).Contains(count) == false)
                        {
                            User user = new User(count, fName, sName, mName, gName, dName);
                            users.Add(user);
                        }
                        else
                        {
                            count--;
                        }
                    }
                }
                
            }
        }
    }
}
