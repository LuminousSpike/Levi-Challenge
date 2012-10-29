using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Levi_Challenge
{
    class XMLEngine
    {
        public static List<Ship> PlayerShips = new List<Ship>();
        public static List<Ship> EnemyShips = new List<Ship>();
        private static string ReadElement(XmlReader reader, string name)
        {
            string Value = null;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.Read();
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name == name)
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            Value = reader.Value;
                        }
                        reader.Read();
                    }
                }
            }
            if (Value != null)
                reader.Read();
            return Value;
        }

        public static void PhraseShipXML()
        {
            if (Directory.Exists(@"Data\Ships\"))
            {
                string[] filePaths = Directory.GetFiles(@"Data\Ships\", "*.xml");
                for (int i = 0; i < filePaths.Length; i++)
                {
                    LoadShips(filePaths[i]);
                }
            }
        }

        private static void LoadShips(string fileName)
        {
            // Load the XML Document
            XmlReader reader = XmlReader.Create(fileName);

            string Name = null;
            int Health = 0;
            float Shield = 0;
            float Speed = 0;
            int HardPoints = 0;
            int WeaponClass = 0;
            int Armour = 0;
            string Texture = null;
            int Cost = 0;
            string ShipType = null;
            int Level = 0;
            int Points = 0;
            string AI = null;

            bool Correct_File = false;
            bool isPlayer = false;
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "Ship")
                {
                    if (reader.GetAttribute(0) == "true")
                        isPlayer = true;
                    Correct_File = true;
                }

                if (Correct_File == true)
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.Read();
                        while (reader.NodeType != XmlNodeType.EndElement)
                        {
                            // General Stuff
                            Name = ReadElement(reader, "Name");

                            Health = Convert.ToInt32(ReadElement(reader, "Health"));

                            Shield = (float)Convert.ToDouble(ReadElement(reader, "Shield"));

                            Speed = (float)Convert.ToDouble(ReadElement(reader, "Speed"));

                            HardPoints = Convert.ToInt32(ReadElement(reader, "HardPoints"));

                            WeaponClass = Convert.ToInt32(ReadElement(reader, "WeaponClass"));

                            Armour = Convert.ToInt32(ReadElement(reader, "Armour"));

                            Texture = ReadElement(reader, "Texture");

                            // Player Stuff
                            if (isPlayer == true)
                            {
                                Cost = Convert.ToInt32(ReadElement(reader, "Cost"));

                                ShipType = ReadElement(reader, "Type");
                            }

                            // Enemy Stuff
                            else
                            {
                                Level = Convert.ToInt32(ReadElement(reader, "Level"));

                                Points = Convert.ToInt32(ReadElement(reader, "Points"));

                                AI = ReadElement(reader, "AI");
                            }
                            reader.Read();
                        }
                    }
                    if (isPlayer == true)
                        PlayerShips.Add(new Ship(Name, Health, Shield, Speed, WeaponClass, Armour, Texture, Cost, ShipType, HardPoints));
                    else
                        EnemyShips.Add(new Ship(Name, Health, Shield, Speed, WeaponClass, Armour, Texture, Level, Points, AI, HardPoints));
                }
            }
            reader.Close();

            if (Correct_File == false)
            {
                // Error code here
            }
        }
    }
}
