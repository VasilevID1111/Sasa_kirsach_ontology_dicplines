using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_3_курс
{
    public partial class Form1 : Form
    {
        Dictionary<int, string> Знания = new Dictionary<int, string>();
        Dictionary<int,string> Дисциплины = new Dictionary<int, string>();
        private bool Режим_Работы_С_Дисциплинами = true;

        private static string path = "C:\\Users\\Vasilev\\Desktop\\ontolis\\Моя_онтология версия 4.ont";
        //private static string path = "C:\\Users\\Vasilev\\Desktop\\ontolis\\test.ont";
        //private static string path = "C:\\Users\\rainr\\ПГНИУ\\Курсовая\\ontolis-demo-win (2)\\Моя_онтология версия 5.ont";
        //private static string path = "C:\\Users\\rainr\\ПГНИУ\\Курсовая\\ontolis-demo-win (2)\\test.ont";
        Ontology ontology = new Ontology(path);
        public Form1()
        {
            InitializeComponent();

            cbТипЗапроса.Items.Add("Я изучил");
            cbТипЗапроса.Items.Add("Могу ли я изучить");           
            cbТипЗапроса.Items.Add("Я хочу изучить");
            buttonДисциплины.Enabled = false;
            buttonЗнания.Enabled=false;

            //необходимо для прокручивания вниз - вверх
            listView1.View = View.Details;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            ColumnHeader header = new ColumnHeader();
            header.Text = "";
            header.Name = "col1";
            listView1.Columns.Add(header);
            listView1.Columns[0].Width = 400;

            //необходимо для прокручивания вниз - вверх
            listView2.View = View.Details;
            listView2.HeaderStyle = ColumnHeaderStyle.None;
            ColumnHeader header1 = new ColumnHeader();
            header1.Text = "";
            header1.Name = "col1";
            listView2.Columns.Add(header1);
            listView2.Columns[0].Width = 400;

            ontology.ReadOntology();

            Знания = ontology.GetNameOfЗнания();
            Дисциплины = ontology.GetNameOfДисциплины();
            Знания = Знания.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); //cортировка
            Дисциплины = Дисциплины.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private void cbТипЗапроса_SelectedIndexChanged(object sender, EventArgs e) //выбираем тип запроса 
        {
            listView1.ItemCheck -= ListView1_ItemCheck;
            listView1.Items.Clear();
            listView2.Items.Clear();

            switch (cbТипЗапроса.SelectedIndex)
            {
                case 0: // Я изучил 
                    buttonЗнания.Enabled = false;
                    buttonДисциплины.Enabled = false;
                    textBoxФраза.Text = "Теперь я знаю:";
                    foreach (var dis in Дисциплины)
                    {
                        listView1.Items.Add(dis.Value);
                        listView1.Items[listView1.Items.Count - 1].Tag = dis.Key;
                    }
                    break;


                case 1:
                    listView1.View = View.Details;
                    listView1.HeaderStyle = ColumnHeaderStyle.None;
                    ColumnHeader header = new ColumnHeader();
                    header.Text = "";
                    header.Name = "col1";
                    listView1.Columns.Add(header);

                    buttonЗнания.Enabled = false;
                    buttonДисциплины.Enabled = false;
                    textBoxФраза.Text = "Какие знания у меня есть:";
                    Dictionary<int, string> ordered_Дисциплины = Дисциплины.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                    foreach (var dis in ordered_Дисциплины)
                    {
                        listView1.Items.Add(dis.Value);
                        listView1.Items[listView1.Items.Count - 1].Tag = dis.Key;
                    }
                    listView1.ItemCheck += ListView1_ItemCheck;

                    listView2.CheckBoxes = true;
                    ColumnHeader header1 = new ColumnHeader();
                    header1.Text = "";
                    header1.Name = "col1";
                    listView2.View = View.Details;
                    listView2.HeaderStyle = ColumnHeaderStyle.None;
                    listView2.Columns.Add(header1);

                    Dictionary<int, string> ordered_знания = Знания.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                    foreach (var dis in ordered_знания)
                    {
                        listView2.Items.Add(dis.Value);
                        listView2.Items[listView2.Items.Count - 1].Tag = dis.Key;
                    }
                    break;


                case 2: // Я хочу изучить
                    buttonЗнания.Enabled = true;
                    buttonДисциплины.Enabled = false;
                    textBoxФраза.Text = "Мне нужно знать:";
                    foreach (var dis in Дисциплины)
                    {
                        listView1.Items.Add(dis.Value);
                        listView1.Items[listView1.Items.Count - 1].Tag = dis.Key;;
                    }
                        break;
            }
                

        }

        private void ListView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var listView = sender as ListView;
            if (listView != null && e.NewValue == CheckState.Checked)
            {
                foreach (ListViewItem checkedItem in listView.CheckedItems)
                {
                    checkedItem.Checked = false;
                }
            }
        }

        private void buttonДисциплины_Click(object sender, EventArgs e) // кнопка дисциплины 
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            buttonЗнания.Enabled = true;
            buttonДисциплины.Enabled = false;
            Режим_Работы_С_Дисциплинами = true;
            textBoxФраза.Text = "Мне нужно знать:";

            // это для того, чтобы была прокрутка вниз-вверх  (сделано выше)
            //listView1.View = View.Details;
            //listView1.HeaderStyle = ColumnHeaderStyle.None;
            //ColumnHeader header = new ColumnHeader();
            //header.Text = "";
            //header.Name = "col1";
            //listView1.Columns.Add(header);

            foreach (var dis in Дисциплины) // выводим список дисциплин  на экран 
            {
                listView1.Items.Add(dis.Value);
                listView1.Items[listView1.Items.Count - 1].Tag = dis.Key; ;
            }
        }

        private void buttonЗнания_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            buttonЗнания.Enabled = false;
            buttonДисциплины.Enabled = true;
            Режим_Работы_С_Дисциплинами = false;
            textBoxФраза.Text = "Мне нужно изучить следующие дисциплины:";


            // это для того, чтобы была прокрутка вниз-вверх (при создании формы написал)
            //listView1.View = View.Details;
            //listView1.HeaderStyle = ColumnHeaderStyle.None;
            //ColumnHeader header = new ColumnHeader();
            //header.Text = "";
            //header.Name = "col1";
            //listView1.Columns.Add(header);

            foreach (var dis in Знания) // выводим список знаний  на экран 
            {
                listView1.Items.Add(dis.Value);
                listView1.Items[listView1.Items.Count - 1].Tag = dis.Key; ;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonВыполнить_Click(object sender, EventArgs e)
        {
            

            switch (cbТипЗапроса.SelectedIndex)
            {
                case 0: //Я изучил
                    listView2.Items.Clear();

                    List<int> ids_дисциплины = new List<int>();
                    Dictionary<int, string> знания0 = new Dictionary<int, string>(); //список знаний
                    foreach (ListViewItem res in listView1.CheckedItems)//проходимся по всем выбранным дисциплинам 
                    { 
                        ids_дисциплины.Add(Convert.ToInt32(res.Tag));
                        ontology.WhatWeKnow(Convert.ToInt32(res.Tag), ref знания0);
                    }
                    listView2.CheckBoxes = false;
                    знания0 = знания0.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

                    

                    
                    listView2.Items.Add("Дисциплины, которые могу изучить");
                    listView2.Items[listView2.Items.Count - 1].ForeColor = Color.Green;
                    List<string> дисциплины1 = new List<string>(ontology.WhatWeKnowAboutAccomodation(ids_дисциплины, знания0.Keys.ToList<int>()));
                    foreach (string zn in дисциплины1)
                    {
                        listView2.Items.Add(zn);
                    }

                    listView2.Items.Add("");
                    listView2.Items.Add("Изученные знания");
                    listView2.Items[listView2.Items.Count - 1].ForeColor = Color.Blue;

                    foreach (KeyValuePair<int, string> zn in знания0)
                    {
                        listView2.Items.Add(zn.Value);
                    }

                    break;

                case 1:
                    int id = Convert.ToInt32(listView1.CheckedItems[0].Tag);
                    Dictionary<int ,string> знания1 = new Dictionary<int, string>(); //список знаний
                    foreach (ListViewItem res in listView2.CheckedItems)//проходимся по всем выбранным дисциплинам 
                    {
                        знания1.Add(Convert.ToInt32(res.Tag), res.Text);
                    }
                    List<string> ЗнанияНужны = new List<string>(ontology.WhatWeKnowAboutKnowledge(id,знания1));
                    ЗнанияНужны.Sort();
                    string result = "";
                    foreach (string знание in ЗнанияНужны)
                    {
                        result += знание + "\n";
                    }
                    if (result == "")
                    {
                        MessageBox.Show("Вы можете изучить дисциплину \"" + listView1.CheckedItems[0].Text + "\"\n" + result);
                    } else
                    {
                        MessageBox.Show("Вам необходимы следующие знания, чтобы изучить дисциплину \"" + listView1.CheckedItems[0].Text + "\":\n\n" + result);
                    }
                    break;


                case 2://Я хочу изучить 
                    listView2.Items.Clear();

                    if (Режим_Работы_С_Дисциплинами)
                    {
                        List<string> знания = new List<string>(); //список знаний 
                        Dictionary<int, string> used = new Dictionary<int, string>(); // список использованных вершин 

                        foreach (ListViewItem res in listView1.CheckedItems)//проходимся по всем выбранным дисциплинам 
                        {
                            ontology.WhatWeMustToKnow(Convert.ToInt32(res.Tag), ref знания);
                        }

                        List<string> знания2 = new List<string>();
                        foreach (ListViewItem res in listView1.CheckedItems)//проходимся по всем выбранным дисциплинам 
                        {
                            ontology.WhatWeMustToKnowDisconnect(Convert.ToInt32(res.Tag), знания2, ref used);
                        }
                        foreach (ListViewItem res in listView1.CheckedItems)//проходимся по всем выбранным дисциплинам 
                        {
                            used.Remove(Convert.ToInt32(res.Tag));
                        }

                        List<string> distinct = знания.Distinct().ToList(); //отсекаем повторяющиеся элементы 
                        listView2.CheckBoxes = false;
                        distinct.Sort();
                        listView2.Items.Add("Дисциплины");
                        listView2.Items[listView2.Items.Count - 1].ForeColor = Color.Green;
                        foreach (KeyValuePair<int, string> entry in used)
                        {
                            listView2.Items.Add(entry.Value);
                        }
                        listView2.Items.Add("");
                        listView2.Items.Add("Знания");
                        listView2.Items[listView2.Items.Count - 1].ForeColor = Color.Blue;
                        foreach (var zn in distinct)
                        {
                            listView2.Items.Add(zn);
                        }
                    } else
                    {
                        Dictionary<int, string> used_Дисциплины = new Dictionary<int, string>(); // список использованных вершин 
                        List <int> Tags = new List<int>();

                        foreach (ListViewItem res in listView1.CheckedItems)//проходимся по всем выбранным дисциплинам 
                        {
                            Tags.Add(Convert.ToInt32(res.Tag));
                        }

                        used_Дисциплины = ontology.WhatWeMustToKnowUnlucky(Tags);

                        listView2.CheckBoxes = false;
                        List<string> дисциплины = used_Дисциплины.Values.ToList<string>();
                        дисциплины.Sort();
                        foreach (string name in дисциплины)
                        {
                            listView2.Items.Add(name);
                        }

                    }
                    
                    break;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
