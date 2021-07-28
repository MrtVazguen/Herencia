using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace ReunioSocial
{
    public class Simpaties : Grid
    {
        public Simpaties(TaulaPersones gent)
        {
            this.Background = new SolidColorBrush(Colors.LightBlue);
            ShowGridLines = true;
            int convidats = gent.NHomes + gent.NDones;

            RowDefinition rd;
            ColumnDefinition cd;
            for (int i = 0; i <= convidats; i++)
            {
                rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                RowDefinitions.Add(rd);

                cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinitions.Add(cd);
            }

            cd = new ColumnDefinition();
            cd.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinitions.Add(cd);

            TextBlock plusSexe = new TextBlock();
            plusSexe.Text = "Plus de sexe";
            plusSexe.FontSize = 18;
            plusSexe.HorizontalAlignment = HorizontalAlignment.Center;
            plusSexe.VerticalAlignment = VerticalAlignment.Center;
            plusSexe.FontFamily = new FontFamily("Comic Sans MS");
            SetRow(plusSexe, 0);
            SetColumn(plusSexe, 1);
            Children.Add(plusSexe);

            IntegerUpDown iud;
            TextBlock nomConvidat;
            // RotateTransform rt = new RotateTransform(90);
            int nConvidats = 1;
            for (int i = 0; i < gent.Count; i++)
            {
                if (gent[i].EsConvidat)
                {
                    nomConvidat = new TextBlock();
                    nomConvidat.Text = gent[i].Nom;
                    nomConvidat.FontSize = 18;
                    nomConvidat.HorizontalAlignment = HorizontalAlignment.Center;
                    nomConvidat.VerticalAlignment = VerticalAlignment.Center;
                    nomConvidat.FontFamily = new FontFamily("Comic Sans MS");
                    SetRow(nomConvidat, nConvidats);
                    SetColumn(nomConvidat, 0);
                    Children.Add(nomConvidat);

                    nomConvidat = new TextBlock();
                    nomConvidat.Text = gent[i].Nom;
                    nomConvidat.FontSize = 18;
                    nomConvidat.HorizontalAlignment = HorizontalAlignment.Center;
                    nomConvidat.VerticalAlignment = VerticalAlignment.Center;
                    nomConvidat.FontFamily = new FontFamily("Comic Sans MS");
                    // nomConvidat.RenderTransformOrigin = new Point(0.5, 0.5);
                    // nomConvidat.RenderTransform = rt;
                    SetColumn(nomConvidat, nConvidats + 1);
                    SetRow(nomConvidat, 0);
                    Children.Add(nomConvidat);

                    iud = new IntegerUpDown();
                    iud.Value = ((Convidat)gent[i]).PlusSexe;
                    iud.Minimum = 0;
                    iud.Maximum = 3;
                    SetRow(iud, nConvidats);
                    SetColumn(iud, 1);
                    iud.Tag = gent[i];
                    iud.ValueChanged += IudPlusSexe_ValueChanged;
                    Children.Add(iud);


                    for (int convidat = 0; convidat < ((Convidat)gent[i]).Simpaties.Count; convidat++)
                    {
                        for (int j = 0; j < gent.Count; j++)
                        {
                            if (gent[j].EsConvidat && gent[i].Nom != gent[j].Nom)
                            {
                                iud = new IntegerUpDown();
                                iud.Value = ((Convidat)gent[i]).Simpaties[gent[j].Nom];
                                iud.Minimum = -5;
                                iud.Maximum = 5;
                                SetRow(iud, i + 1);
                                SetColumn(iud, j + 2);
                                iud.Tag = new KeyValuePair<string, SortedDictionary<string, int>>(gent[j].Nom, ((Convidat)gent[i]).Simpaties);
                                iud.ValueChanged += IudSimpaties_ValueChanged;
                                Children.Add(iud);
                            }
                        }
                    }

                    nConvidats++;
                }
            }

            /*
            TextBlock txt1 = new TextBlock();
            txt1.Text = "2005 Products Shipped";
            txt1.FontSize = 20;
            txt1.FontWeight = FontWeights.Bold;
            Grid.SetColumnSpan(txt1, 3);
            Grid.SetRow(txt1, 0);
            // Add the TextBlock elements to the Grid Children collection
            myGrid.Children.Add(txt1);*/
           
        }

        private void IudPlusSexe_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is IntegerUpDown && ((IntegerUpDown)sender).Value != null)
            {
                Convidat convidat = (Convidat)((IntegerUpDown)sender).Tag;
                convidat.PlusSexe = (int)((IntegerUpDown)sender).Value;
            }
        }

        private void IudSimpaties_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is IntegerUpDown && ((IntegerUpDown)sender).Value != null)
            {
                KeyValuePair<string, SortedDictionary<string, int>> clauSimpaties =
                    (KeyValuePair<string, SortedDictionary<string, int>>)((IntegerUpDown)sender).Tag;

                clauSimpaties.Value[clauSimpaties.Key] = (int)((IntegerUpDown)sender).Value;
            }
        }
    }
}
