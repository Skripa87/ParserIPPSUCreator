﻿using System;
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

        static private string directoryPathUsersMSZP;
        static private string filePathLMSZ;
        private List<User> GetAllUsersMSZPs(string pathDirectory)
        {
            List<User> users = new List<User>();
            var files = Directory.GetFiles(pathDirectory);
            foreach (var f in files)
            {
                if (f != null)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(f);
                    XmlElement root = xDoc.DocumentElement;
                    //XmlElement elements = root.GetElementsByTagName("elements").Cast<XmlElement>().FirstOrDefault();
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
                        User user = new User(fName, sName, mName, gName, dName);
                        if (users.Count == 0)
                        {
                            users.Add(user);
                        }
                        else if (users.Where(u => u.Birthsday.Equals(user.Birthsday) && u.FirstName.Equals(user.FirstName) && u.Gender.Equals(user.Gender) && u.MidleName.Equals(user.MidleName) && u.SecondName.Equals(user.SecondName)).FirstOrDefault() == null)
                        {
                            users.Add(user);
                        }
                        XmlNode xlmsz = ((XmlElement)fact).GetElementsByTagName("LMSZID").Cast<XmlNode>().FirstOrDefault();
                        if (users.Where(u => u.Birthsday.Equals(user.Birthsday) && u.FirstName.Equals(user.FirstName) && u.Gender.Equals(user.Gender) && u.MidleName.Equals(user.MidleName) && u.SecondName.Equals(user.SecondName)).FirstOrDefault() != null)
                        {
                            var bufUser = users.Where(u => u.Birthsday.Equals(user.Birthsday) && u.FirstName.Equals(user.FirstName) && u.Gender.Equals(user.Gender) && u.MidleName.Equals(user.MidleName) && u.SecondName.Equals(user.SecondName)).FirstOrDefault();
                            if (bufUser.MyMSZPs.Count == 0)
                            {
                                users.Where(u => u.Birthsday.Equals(user.Birthsday) && u.FirstName.Equals(user.FirstName) && u.Gender.Equals(user.Gender) && u.MidleName.Equals(user.MidleName) && u.SecondName.Equals(user.SecondName)).FirstOrDefault().MyMSZPs.Add(xlmsz.InnerText);
                                bufUser = null;
                                bufUser = users.Where(u => u.Birthsday.Equals(user.Birthsday) && u.FirstName.Equals(user.FirstName) && u.Gender.Equals(user.Gender) && u.MidleName.Equals(user.MidleName) && u.SecondName.Equals(user.SecondName)).FirstOrDefault();
                            }
                            else if (!bufUser.MyMSZPs.Contains(xlmsz.InnerText))
                            {
                                users.Where(u => u.Birthsday.Equals(user.Birthsday) && u.FirstName.Equals(user.FirstName) && u.Gender.Equals(user.Gender) && u.MidleName.Equals(user.MidleName) && u.SecondName.Equals(user.SecondName)).FirstOrDefault().MyMSZPs.Add(xlmsz.InnerText);
                            }
                        }
                    }
                }
            }
            return users;
        }

        private List<LMSZ> GetLMSZ(string pathFile)
        {
            int id = 0;
            List<LMSZ> LMSZs = new List<LMSZ>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(pathFile);
            XmlElement root = xDoc.DocumentElement;
            //XmlElement elements = root.GetElementsByTagName("elements").Cast<XmlElement>().FirstOrDefault();
            XmlNodeList nodesLmsz = root.GetElementsByTagName("etplmsz:localMSZ");
            foreach(XmlNode lm in nodesLmsz)
            {
                string egissoId = ((XmlElement)lm).GetElementsByTagName("etlmsz:ID").Cast<XmlNode>().FirstOrDefault().InnerText;
                string name = ((XmlElement)lm).GetElementsByTagName("etlmsz:title").Cast<XmlNode>().FirstOrDefault().InnerText;
                LMSZ lmsz = new LMSZ(id,name, egissoId);
                id++;
                LMSZs.Add(lmsz);
            }
            return LMSZs;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var users = GetAllUsersMSZPs(directoryPathUsersMSZP);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            directoryPathUsersMSZP = folderBrowserDialog1.SelectedPath;
            textBox1.Text = directoryPathUsersMSZP;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            filePathLMSZ = openFileDialog1.FileName;
            textBox2.Text = filePathLMSZ;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.txt";
            openFileDialog1.ShowDialog();
            textBox3.Text = openFileDialog1.FileName;            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var LMSZs = GetLMSZ(filePathLMSZ);
            List<string> text = new List<string>();
            string t = "";
            foreach (var l in LMSZs)
            {
                t = "";
                t += l.Id.ToString()+";"+l.EgissoId+";"+";"
            }
            File.WriteAllLines("d:\\result.txt",
        }
    }
}
