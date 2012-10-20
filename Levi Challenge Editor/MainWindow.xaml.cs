﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;

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

        private void PS_Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string fileName = SaveDialog();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            string isStartingShip = "false";

            if (PS_ChkBox_StartingShip.IsChecked == true)
                isStartingShip = "true";
            // Create and save the XML Document
            XmlWriter writer = XmlWriter.Create(fileName, settings);
            writer.WriteStartDocument();
            writer.WriteComment("This file was generated by Levi Challenge Editor.");
            writer.WriteStartElement("Ship");
            writer.WriteAttributeString("StartingShip", isStartingShip);
            writer.WriteElementString("Name", PS_TxtBox_Name.Text);
            writer.WriteElementString("Cost", PS_TxtBox_Cost.Text);
            writer.WriteElementString("Health", PS_TxtBox_Health.Text);
            writer.WriteElementString("Shield", PS_TxtBox_Shield.Text);
            writer.WriteElementString("Speed", PS_TxtBox_Speed.Text);
            writer.WriteElementString("HardPoints", PS_ComBox_HardPoints.Text);
            writer.WriteElementString("Armour", PS_TxtBox_Armour.Text);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

            UpdatePSList();
        }

        private string SaveDialog()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Untitled"; // Default file name
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension
            string mypath = Directory.GetCurrentDirectory();
            dlg.InitialDirectory = mypath + @"\Data\Ships";
            string filename = PS_TxtBox_Name.Text;

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
            }
            return filename;
        }

        private void UpdatePSList()
        {
            string[] PS_filePaths = Directory.GetFiles(@"Data\Player Ships\", "*.xml");
            PS_LstBox_Items.Items.Clear();
            for (int i = 0; i < PS_filePaths.Length; i++)
            {
                PS_LstBox_Items.Items.Add(PS_filePaths[i]);
            }
        }

        private void PS_Btn_New_Click(object sender, RoutedEventArgs e)
        {
            PS_TxtBox_Name.Text = "";
            PS_TxtBox_Cost.Text = "";
            PS_TxtBox_Health.Text = "";
            PS_TxtBox_Shield.Text = "";
            PS_TxtBox_Speed.Text = "";
            PS_ComBox_HardPoints.Text = "1";
            PS_TxtBox_Armour.Text = "";

            UpdatePSList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(@"Data"))
                Directory.CreateDirectory(@"Data");
            if (!Directory.Exists(@"Data\Player Ships"))
                Directory.CreateDirectory(@"Data\Player Ships");

            UpdatePSList();
        }

        private void PS_LstBox_Items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string fileName = (string)PS_LstBox_Items.SelectedItem;

            // Create and save the XML Document
            // Needs to check for valid xml nodes and such
            XmlReader reader = XmlReader.Create(fileName);
            reader.Read();
            reader.ReadStartElement();
            // Bug here to be fixed
            if (reader.ReadAttributeValue() == true)
                PS_ChkBox_StartingShip.IsChecked = true;
            else
                PS_ChkBox_StartingShip.IsChecked = false;

            PS_TxtBox_Name.Text = reader.ReadElementString();
            PS_TxtBox_Cost.Text = reader.ReadElementString();
            PS_TxtBox_Health.Text = reader.ReadElementString();
            PS_TxtBox_Shield.Text = reader.ReadElementString();
            PS_TxtBox_Speed.Text = reader.ReadElementString();
            PS_ComBox_HardPoints.Text = reader.ReadElementString();
            PS_TxtBox_Armour.Text = reader.ReadElementString();
            
            reader.Close();
        }
    }
}
