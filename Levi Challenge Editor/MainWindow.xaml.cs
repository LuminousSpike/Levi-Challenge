using System;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.IO;
using System.Windows.Controls;
using System.Collections;

namespace Levi_Challenge_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(@"Data"))
                Directory.CreateDirectory(@"Data");
            if (!Directory.Exists(@"Data\Ships"))
                Directory.CreateDirectory(@"Data\Ships");
            if (!Directory.Exists(@"Data\Weapons"))
                Directory.CreateDirectory(@"Data\Weapons");

            UpdateList(S_LstBox_Items, @"Data\Ships");
            UpdateList(W_LstBox_Items, @"Data\Weapons");
            UpdateComBox(S_ComBox_Texture, @"Content\Ships", "*.xnb");
            UpdateComBox(S_ComBox_Weapon1, @"Data\Weapons", "*.xml");
            UpdateComBox(S_ComBox_Weapon2, @"Data\Weapons", "*.xml");
            UpdateComBox(S_ComBox_Weapon3, @"Data\Weapons", "*.xml");
            UpdateComBox(W_ComBox_ProjectileTexture, @"Content\Sprites\Projectiles", "*.xnb");
            S_ManageObjects();
        }

        // Functions used more than once
        #region Functions

        private void UpdateList(ListBox listBox, string folder)
        {
            if (Directory.Exists(folder))
            {
                string[] filePaths = Directory.GetFiles(folder, "*.xml");
                listBox.Items.Clear();
                for (int i = 0; i < filePaths.Length; i++)
                {
                    filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
                    listBox.Items.Add(filePaths[i]);
                }
            }
        }

        private void UpdateComBox(ComboBox comboBox, string folder, string filetype)
        {
            if (Directory.Exists(folder))
            {
                string[] filePaths = Directory.GetFiles(folder, filetype);
                comboBox.Items.Clear();
                for (int i = 0; i < filePaths.Length; i++)
                {
                    if (Path.GetExtension(filePaths[i]) == ".xml")
                    {
                        XmlReader reader = XmlReader.Create(filePaths[i]);
                        bool Correct_File = false;
                        while (reader.Read())
                        {
                            if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "Weapon")
                            {
                                Correct_File = true;
                            }

                            if (Correct_File == true)
                            {
                                filePaths[i] = ReadElement(reader, "Name");
                                reader.Close();
                            }
                            // Need code to close if wrong file
                        }
                    }
                    else
                        filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
                    comboBox.Items.Add(filePaths[i]);
                }
                comboBox.SelectedIndex = 0;
            }
        }

        private string ReadElement(XmlReader reader, string name)
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

        private void S_ManageObjects()
        {
            // Enable or disable Player/Enemy controls
            if (S_ChkBox_PlayerShip.IsChecked == true)
            {
                ES_TxtBox_Level.IsEnabled = false;
                ES_TxtBox_Points.IsEnabled = false;
                ES_ComBox_AI.IsEnabled = false;

                PS_TxtBox_Cost.IsEnabled = true;
                PS_ComBox_Type.IsEnabled = true;
            }
            else
            {
                ES_TxtBox_Level.IsEnabled = true;
                ES_TxtBox_Points.IsEnabled = true;
                ES_ComBox_AI.IsEnabled = true;

                PS_TxtBox_Cost.IsEnabled = false;
                PS_ComBox_Type.IsEnabled = false;
            }

            // Enable or disable Weapon ComboBoxes
            if (Convert.ToInt32(S_ComBox_Hardpoints.SelectedIndex) > 0)
            {
                S_ComBox_Weapon1.IsEnabled = true;
                if (Convert.ToInt32(S_ComBox_Hardpoints.SelectedIndex) > 1)
                {
                    S_ComBox_Weapon2.IsEnabled = true;
                    if (Convert.ToInt32(S_ComBox_Hardpoints.SelectedIndex) > 2)
                        S_ComBox_Weapon3.IsEnabled = true;
                    else
                        S_ComBox_Weapon3.IsEnabled = false;
                }
                else
                {
                    S_ComBox_Weapon2.IsEnabled = false;
                    S_ComBox_Weapon3.IsEnabled = false;
                }
            }
            else
            {
                S_ComBox_Weapon1.IsEnabled = false;
                S_ComBox_Weapon2.IsEnabled = false;
                S_ComBox_Weapon3.IsEnabled = false;
            }
        }

        private void S_ClearObjects()
        {
            S_TxtBox_Name.Text = "";
            S_TxtBox_Health.Text = "";
            S_TxtBox_Shield.Text = "";
            S_TxtBox_Speed.Text = "";
            S_ComBox_Hardpoints.SelectedIndex = 0;
            S_ComBox_WeaponClass.SelectedIndex = 0;
            S_TxtBox_Armour.Text = "";

            ES_TxtBox_Level.Text = "";
            ES_TxtBox_Points.Text = "";
            ES_ComBox_AI.SelectedIndex = 0;

            PS_TxtBox_Cost.Text = "";
            PS_ComBox_Type.SelectedIndex = 0;
        }

        private void W_ClearObjects()
        {
            W_TxtBox_WeaponName.Text = "";
            W_ComBox_WeaponClass.SelectedIndex = 0;
            W_TxtBox_WeaponRefireRate.Text = "";
            W_ComBox_ProjectileTexture.SelectedIndex = 0;
            W_ComBox_ProjectileType.SelectedIndex = 0;
            W_TxtBox_ProjectileDamage.Text = "";
            W_TxtBox_ProjectileSpeed.Text = "";
        }

        private string SaveDialog(string filename, string folder)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = filename; // Default file name
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension
            string mypath = Directory.GetCurrentDirectory();
            dlg.InitialDirectory = mypath + @"\Data\" + folder;

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
            }
            else
                filename = null;
            return filename;
        }

        private void DeleteFile(string folder, string filename)
        {
            if (MessageBox.Show("Are you sure you wish to delete this item?", filename, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                File.Delete(folder + filename + ".xml");
            }
        }
        #endregion

        // Ships Code
        #region Ships
        private void S_Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string header;
            if (S_ChkBox_PlayerShip.IsChecked == true)
                header = "PS_";
            else
                header = "ES_";

            string fileName = SaveDialog(header + S_TxtBox_Name.Text, "Ships");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            string isPlayerShip = "false";

            if (S_ChkBox_PlayerShip.IsChecked == true)
                isPlayerShip = "true";
            if (fileName != null)
            {
                // Create and save the XML Document
                XmlWriter writer = XmlWriter.Create(fileName, settings);
                writer.WriteStartDocument();
                writer.WriteComment("This file was generated by Levi Challenge Editor.");
                writer.WriteStartElement("Ship");
                writer.WriteAttributeString("PlayerShip", isPlayerShip);

                // General Stuff
                writer.WriteElementString("Name", S_TxtBox_Name.Text);
                writer.WriteElementString("Health", S_TxtBox_Health.Text);
                writer.WriteElementString("Shield", S_TxtBox_Shield.Text);
                writer.WriteElementString("Speed", S_TxtBox_Speed.Text);
                writer.WriteElementString("Hardpoints", S_ComBox_Hardpoints.Text);
                writer.WriteElementString("WeaponClass", S_ComBox_WeaponClass.Text);
                writer.WriteElementString("Weapon1", S_ComBox_Weapon1.Text);
                if (Convert.ToInt32(S_ComBox_Hardpoints.Text) > 1)
                {
                    writer.WriteElementString("Weapon2", S_ComBox_Weapon2.Text);
                    if (Convert.ToInt32(S_ComBox_Hardpoints.Text) > 2)
                        writer.WriteElementString("Weapon3", S_ComBox_Weapon3.Text);
                }
                writer.WriteElementString("Armour", S_TxtBox_Armour.Text);
                writer.WriteElementString("Texture", S_ComBox_Texture.Text);

                // Player Stuff
                if (isPlayerShip == "true")
                {
                    writer.WriteElementString("Cost", PS_TxtBox_Cost.Text);
                    writer.WriteElementString("Type", PS_ComBox_Type.Text);
                }

                // Enemy Stuff
                else
                {
                    writer.WriteElementString("Level", ES_TxtBox_Level.Text);
                    writer.WriteElementString("Points", ES_TxtBox_Points.Text);
                    writer.WriteElementString("AI", ES_ComBox_AI.Text);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }

            UpdateList(S_LstBox_Items, @"Data\Ships");
        }

        private void S_Btn_New_Click(object sender, RoutedEventArgs e)
        {
            S_ClearObjects();
            S_ManageObjects();
        }

        private void S_Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteFile(@"Data\Ships\", (string)S_LstBox_Items.SelectedItem);
            UpdateList(S_LstBox_Items, @"Data\Ships");
        }

        private void S_LstBox_Items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            S_ClearObjects();
            string fileName = @"Data\Ships\" + (string)S_LstBox_Items.SelectedItem + ".xml";

            // Load the XML Document
            XmlReader reader = XmlReader.Create(fileName);

            bool Correct_File = false;
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "Ship")
                {
                    if (reader.GetAttribute(0) == "true")
                        S_ChkBox_PlayerShip.IsChecked = true;
                    else
                        S_ChkBox_PlayerShip.IsChecked = false;
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
                            S_TxtBox_Name.Text = ReadElement(reader, "Name");

                            S_TxtBox_Health.Text = ReadElement(reader, "Health");

                            S_TxtBox_Shield.Text = ReadElement(reader, "Shield");

                            S_TxtBox_Speed.Text = ReadElement(reader, "Speed");

                            S_ComBox_Hardpoints.Text = ReadElement(reader, "Hardpoints");

                            S_ComBox_WeaponClass.Text = ReadElement(reader, "WeaponClass");

                            S_ComBox_Weapon1.Text = ReadElement(reader, "Weapon1");

                            if (Convert.ToInt32(S_ComBox_Hardpoints.Text) > 1)
                            {
                                S_ComBox_Weapon1.Text = ReadElement(reader, "Weapon2");
                                if (Convert.ToInt32(S_ComBox_Hardpoints.Text) > 2)
                                    S_ComBox_Weapon1.Text = ReadElement(reader, "Weapon3");
                            }

                            S_TxtBox_Armour.Text = ReadElement(reader, "Armour");

                            S_ComBox_Texture.Text = ReadElement(reader, "Texture");

                            // Player Stuff
                            if (S_ChkBox_PlayerShip.IsChecked == true)
                            {
                                PS_TxtBox_Cost.Text = ReadElement(reader, "Cost");

                                PS_ComBox_Type.Text = ReadElement(reader, "Type");
                            }

                            // Enemy Stuff
                            else
                            {
                                ES_TxtBox_Level.Text = ReadElement(reader, "Level");

                                ES_TxtBox_Points.Text = ReadElement(reader, "Points");

                                ES_ComBox_AI.Text = ReadElement(reader, "AI");
                            }
                            reader.Read();
                        }
                    }
                }
            }
            reader.Close();

            if (Correct_File == false)
            {
                MessageBox.Show("Incompatible XML File", fileName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            S_ManageObjects();
        }

        private void S_ChkBox_PlayerShip_Click(object sender, RoutedEventArgs e)
        {
            S_ManageObjects();
        }

        private void S_ComBox_Hardpoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized == true)
            {
                S_ManageObjects();
            }
        }
        #endregion

        // Weapons Code
        #region Weapons
        private void W_Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string header = "WP_";

            string fileName = SaveDialog(header + W_TxtBox_WeaponName.Text, "Weapons");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            if (fileName != null)
            {
                // Create and save the XML Document
                XmlWriter writer = XmlWriter.Create(fileName, settings);
                writer.WriteStartDocument();
                writer.WriteComment("This file was generated by Levi Challenge Editor.");
                writer.WriteStartElement("Weapon");
                writer.WriteAttributeString("Type", W_ComBox_WeaponType.Text);
                writer.WriteStartElement("WeaponStats");
                writer.WriteElementString("Name", W_TxtBox_WeaponName.Text);
                writer.WriteElementString("Class", W_ComBox_WeaponClass.Text);
                writer.WriteElementString("RefireRate", W_TxtBox_WeaponRefireRate.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("ProjectileStats");
                writer.WriteElementString("Texture", W_ComBox_ProjectileTexture.Text);
                writer.WriteElementString("Type", W_ComBox_ProjectileType.Text);
                writer.WriteElementString("Damage", W_TxtBox_ProjectileDamage.Text);
                writer.WriteElementString("Speed", W_TxtBox_ProjectileSpeed.Text);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

                UpdateList(W_LstBox_Items, @"Data\Weapons");
                UpdateComBox(S_ComBox_Weapon1, @"Data\Weapons", "*.xml");
                UpdateComBox(S_ComBox_Weapon2, @"Data\Weapons", "*.xml");
                UpdateComBox(S_ComBox_Weapon3, @"Data\Weapons", "*.xml");
            }
        }

        private void W_Btn_New_Click(object sender, RoutedEventArgs e)
        {
            W_ClearObjects();
        }

        private void W_Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteFile(@"Data\Weapons\", (string)W_LstBox_Items.SelectedItem);
            UpdateList(W_LstBox_Items, @"Data\Weapons");
            UpdateComBox(S_ComBox_Weapon1, @"Data\Weapons", "*.xml");
            UpdateComBox(S_ComBox_Weapon2, @"Data\Weapons", "*.xml");
            UpdateComBox(S_ComBox_Weapon3, @"Data\Weapons", "*.xml");
        }

        private void W_LstBox_Items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            W_ClearObjects();
            string fileName = @"Data\Weapons\" + (string)W_LstBox_Items.SelectedItem + ".xml";

            // Load the XML Document
            XmlReader reader = XmlReader.Create(fileName);

            bool Correct_File = false;
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "Weapon")
                {
                    W_ComBox_WeaponType.Text = reader.GetAttribute(0);
                    Correct_File = true;
                }

                if (Correct_File == true)
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.Read();
                        while (reader.NodeType != XmlNodeType.EndElement)
                        {
                            // WeaponStats Stuff
                            if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "WeaponStats")
                            {
                                W_TxtBox_WeaponName.Text = ReadElement(reader, "Name");

                                W_ComBox_WeaponClass.Text = ReadElement(reader, "Class");

                                W_TxtBox_WeaponRefireRate.Text = ReadElement(reader, "RefireRate");
                            }

                            // ProjectileStats Stuff
                            if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "ProjectileStats")
                            {
                                W_ComBox_ProjectileTexture.Text = ReadElement(reader, "Texture");

                                W_ComBox_ProjectileType.Text = ReadElement(reader, "Type");

                                W_TxtBox_ProjectileDamage.Text = ReadElement(reader, "Damage");

                                W_TxtBox_ProjectileSpeed.Text = ReadElement(reader, "Speed");
                            }
                            reader.Read();
                        }
                    }
                }
            }
            reader.Close();

            if (Correct_File == false)
            {
                MessageBox.Show("Incompatible XML File", fileName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
