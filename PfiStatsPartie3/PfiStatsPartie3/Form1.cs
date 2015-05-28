﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PfiStatsPartie3
{
    public partial class Form1 : Form
    {
        public double nbSouslaCourbe = 0;
		float IntervalMin;
		float IntervalMax;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cb_Fonction.SelectedIndex = 0;
        }

        private void Btn_Calculate_Click(object sender, EventArgs e)
        {
            lancementDesPoints();
			IntervalConfiance();

        }

        private void lancementDesPoints()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                Point p = new Point();
                p.X_ = rnd.Next(int.Parse(X_min.Text), int.Parse(X_Max.Text));
                p.Y_ = rnd.Next(0, GetMaxY());

                if (estSousLaCourbe(p))
                    nbSouslaCourbe++;	                 
            }

            Rep.Text = (calculeAire() * (nbSouslaCourbe / 10000)).ToString();
            nbSouslaCourbe = 0;
        }
		private void IntervalConfiance()
		{
			float Z = 1.96f;
			float MargeErreur = Z* float.Parse(Math.Sqrt((float.Parse(Rep.Text)/100 * (1 - float.Parse(Rep.Text)/100))/calculeAire()).ToString());
			IntervalMin = float.Parse(Rep.Text) / 100 - MargeErreur;
			IntervalMax = float.Parse(Rep.Text) / 100 + MargeErreur;
			Tb_MargeErreur.Text = MargeErreur.ToString();
			Tb_IntervalMin.Text = IntervalMin.ToString();
			Tb_IntervalMax.Text = IntervalMax.ToString();
		}
        private int GetMaxY()
        {
            //Seulement verifier quelle fonction a ete choisi pour retourner le bon max y
            int y = 0;

            switch (Cb_Fonction.SelectedItem.ToString())
            {
                case "F1":
					y = 5;
                     break;
                case "F2":
                    y = 5;
                     break;
                case "F3":
                     y = 12;
                     break;
                case "F4":
                     y = 10;
                     break;
                case "F5":
                     y = 4;
                     break;
                default:
                    break;
            }
            return y;       
        }
        private double calculeAire()
        {
            //va chercher le y max calcul la largeur et l'aire du rectangle
           double hauteur = GetMaxY();
           double largeur = double.Parse(X_Max.Text) - double.Parse(X_min.Text);
           double airB = largeur * hauteur;

            return airB;
        }
     private bool estSousLaCourbe(Point p) 
         {
     
        bool SousLaCourbe = false;

         switch (Cb_Fonction.SelectedItem.ToString())
                {
                    case "F1":
                SousLaCourbe = F1(p);   
                         break;
                    case "F2":
                SousLaCourbe = F2(p);
                         break;
                    case "F3":
				SousLaCourbe = F3(p);
                         break;
                    case "F4":
                SousLaCourbe = F4(p);
                         break;
                    case "F5":
                 SousLaCourbe = F5(p);
                         break;
                    default:
                        break;
                }
     
            return SousLaCourbe ;
         }

     private bool F1(Point p)
     {
        double value = Math.Pow(p.X_, 2);
        value = value - (16.0f * p.X_) + 63.0f;
        value = Math.Pow(value, (1.0 / 3.0)) + 4;     
        return p.Y_ <= value;
     }
   
     private bool F2(Point p)
     {
         double value = (3 * (Math.Pow(((p.X_ - 7.0f) / 5.0f), 5))) ;
           value = value  - (5 * (Math.Pow(((p.X_ - 7.0f) / 5.0f), 4))) + 3;
         return p.Y_ <= value;
       
     }

     private bool F3(Point p)
     {
		 float value = -(1.0f/ 3.0f);
		 value = value * float.Parse(Math.Pow((p.X_ - 6), 2).ToString());
	     value += 12;  
		 return float.Parse(p.Y_.ToString()) <= value;
     }
     private bool F4(Point p)
     {
         double value = p.X_ + Math.Sin(p.X_);
         return p.Y_ <= value;
     }
     private bool F5(Point p)
     {
         double value = Math.Cos(p.X_) + 3;
         return p.Y_ <= value;

     }





    }
   
    //Structure de point avoir mon x et mon y ensemble
    public struct Point
    {

       public double X_ { get; set; }
        public double Y_ { get; set; }
      
    }
}
