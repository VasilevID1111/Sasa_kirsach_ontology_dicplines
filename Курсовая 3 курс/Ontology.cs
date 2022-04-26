using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Курсовая_3_курс
{
    [Serializable]
    public class Node
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }// тип вершины 0-это знание, 1-это дисциплина
        public List<Relation> nodeRelations { get; set; } // отношения (ребра), связанные с вершиной 
        public Node(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.type = 0;
            //List<Relation> nodeRelations = new List<Relation>();
        }
         
    }

    [Serializable]
    public class Relation
    {
        public string name { get; set; }
        public int nodeFrom { get; set; } // id начальной вершины
        public int nodeTo { get; set; } // id конечной вершины
        public Relation(string name, int nodeFrom, int nodeTo)
        {
            this.name = name;
            this.nodeFrom = nodeFrom;
            this.nodeTo = nodeTo;
        }
    }

    [Serializable]
    public class Ontology
    {
        public StreamReader file;
        public List<Node> nodes { get; set; }
        public List<Relation> relations { get; set; }

        public Ontology(string path)
        {
            file = new StreamReader(path);
            //List<Node> nodes = new List<Node>();
            //List<Relation> relations = new List<Relation>();
        }
        public string FindResultName(string line) // находим имя вершины или отношения 
        {
            int colon_pos = line.IndexOf(":");
            int mark1 = line.IndexOf("\"", colon_pos + 1);
            int mark2 = line.IndexOf("\"", mark1 + 1);
            string res = line.Substring(mark1 + 1, mark2 - mark1 - 1);
            return res;
        }
        public void ReadOntology() // считываем онтологию 
        {
            List<int> d_id = new List<int>();
            List<string> name = new List<string>();
            List<int> s_id = new List<int>();

            nodes = new List<Node>();
            relations = new List<Relation>();

            string line;
            int flag = 0; // отметка что мы сейчас считываем: 0-пропускаем раздел с историей, 1-считываем вершины, 2-считываем отношения
            while (flag == 0) // пропускаем раздел с history 
            {
                line = file.ReadLine();
                if (line.Contains("nodes"))
                    flag = 1;
            }
            while (flag == 1) // считываем вершины
            {
                line = file.ReadLine();

                if (line.Contains("id")) //считываем id вершины 
                {
                    string res = FindResultName(line);
                    d_id.Add(Int32.Parse(res));
                }
                if (line.Contains("name") && !line.Contains("namespace")) //считываем имя вершины 
                {
                    string res = FindResultName(line);
                    name.Add(res);
                }

                if (line.Contains("relations"))
                    flag = 2;
            }

            for (int i = 0; i < d_id.Count; i++) // создаем полученные вершины и добавляем их в список 
            {
                Node node = new Node(d_id[i], name[i]);
                nodes.Add(node);
            }

            d_id.Clear(); // очищаем списки, так как в них сейчас будет записываться информация про отношения
            name.Clear();

            while ((line = file.ReadLine()) != null) // считываем отношения
            {

                if (line.Contains("destination_node_id")) // считываем nodeTo
                {
                    string res = FindResultName(line);
                    d_id.Add(Int32.Parse(res));                   
                }
                if (line.Contains("name") && !line.Contains("namespace")) // считываем название отношения 
                {
                    string res = FindResultName(line);
                    name.Add(res);
                }
                if (line.Contains("source_node_id")) // считываем nodeFrom
                {
                    string res = FindResultName(line);
                    s_id.Add(Int32.Parse(res));
                }

            }

            for (int i = 0; i < d_id.Count; i++) // создаем полученные вершины и добавляем их в список 
            {
                Relation relation = new Relation(name[i], s_id[i], d_id[i]);
                relations.Add(relation);
            }

            file.Close();

            // определяем тип вершины и записываем к вершинам их список ребер 
            for (int i = 0; i < nodes.Count; i++) // проходимся по всем вершинам 
            {
                nodes[i].nodeRelations = new List<Relation>();
                List<Relation> results = relations.FindAll( // получаем отношения, в которых эта вершина = nodeFrom
                    delegate (Relation rl)
                    {
                        return rl.nodeFrom == nodes[i].id;
                    });

                if (results.Count()!=0 && !(results[0].name == "a_part_of")) // и если отношение не "a_part_of"
                    nodes[i].type = 1; // то тип этой вершины - дисциплина

                for (int j = 0; j < results.Count; j++) // проходимся по всем получившимся ребрам и добавлем их к вершине 
                    nodes[i].nodeRelations.Add(results[j]);
                
                results.Clear();

                results = relations.FindAll( // получаем отношения, в которых эта вершина = nodeTo
                    delegate (Relation rl)
                    {
                        return rl.nodeTo == nodes[i].id;
                    });

                for (int j = 0; j < results.Count; j++) // проходимся по всем получившимся ребрам и добавлем их к вершине 
                    nodes[i].nodeRelations.Add(results[j]);               
            }

           // Console.WriteLine("win");

        }

        public Dictionary<int, string> GetNameOfДисциплины()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            for (int i = 0; i < nodes.Count; i++)
                if (nodes[i].type == 1)
                    result.Add(nodes[i].id, nodes[i].name);
            return result;
        }

        public Dictionary<int, string> GetNameOfЗнания()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            for (int i = 0; i < nodes.Count; i++)
                if (nodes[i].type == 0)
                    result.Add(nodes[i].id, nodes[i].name);
            return result;
        }

        public Node FindNodeID(int id) //находим вершину по id
        {
            Node res = nodes.Find( 
                    delegate (Node n)
                    {
                        return n.id == id;
                    });
            return res;
        }

        public List<Relation> FindNodeRelations (Node node, string X) //находим все отношения вершины с именем "X" 
        {
            List<Relation> res = node.nodeRelations.FindAll(   
                    delegate (Relation rl)
                    {
                        return rl.name == X;
                    });
            return res;
        }

        

        public Relation FindNodeRelation(Node node, string X) //находим все отношения вершины с именем "X" 
        {
            Relation res = node.nodeRelations.Find(
                    delegate (Relation rl)
                    {
                        return rl.name == X;
                    });
            return res;
        }

        public void WhatWeMustToKnowDisconnect(int id, List<string> знания, ref Dictionary<int, string> used) //функция, которая возвращает знания, которые нам нужно знать для изучения дисциплины 
        {
            Node node = FindNodeID(id); //находим вершину по id

            List<Relation> relationsТЗ = FindNodeRelations(node, "требует_знания"); //находим все отношения вершины с именем "требует_знания"  

            //if (used.Keys.Contains(node.id) || relations?.Any() != true) return; //условие выхода из рекурсии: либо нет отношений "требует_знания", либо мы уже проходили через эту дисциплину 

            //used.Add(node.id, node.name); //присваиваем использованную вершину 

            foreach (Relation rl in relationsТЗ) // проходимся по всем отношениям вершины  "требует_знания"
            {
                Node nodeTo = FindNodeID(rl.nodeTo); //находим вершину nodeTo 
                
                Relation relationИ = FindNodeRelation(nodeTo, "изучает"); //находим отношение "изучает" вершины 

                if (relationИ is null) 
                {
                    Relation relationA_Part_Of = FindNodeRelation(nodeTo, "a_part_of"); //находим отношение "a_part_of" вершины
                    if (!(relationA_Part_Of is null)) //если есть отношения a_part_of, то проходимя по ветке a_part_of до конца
                    {
                        Node nodeToAPO = new Node(1, "0");

                        while (!(relationA_Part_Of is null)) //пока у нас есть a_part_of
                        {
                            nodeToAPO = FindNodeID(relationA_Part_Of.nodeTo); //находим вершину nodeTo
                            relationИ = FindNodeRelation(nodeToAPO, "изучает"); //находим отношение "изучает" вершины 
                            if (!(relationИ is null)) break;
                            relationA_Part_Of = nodeToAPO.nodeRelations.Find( //находим все отношения "a_part_of" вершины, отличные от того, откуда мы пришли   
                            delegate (Relation rll)
                            {
                                return (rll.name == "a_part_of" && nodeToAPO.id != rll.nodeTo);
                            });
                        }
                        nodeTo = nodeToAPO; //записываем конец ветки a_part_of
                    }
                }
                Node nodeFrom = FindNodeID(relationИ.nodeFrom);
                if (!used.Keys.Contains(nodeFrom.id))
                    used.Add(nodeFrom.id, nodeFrom.name);
                //relationИ = FindNodeRelation(nodeTo, "изучает"); //находим отношение "изучает" вершины 

                //знания.Add(nodeTo.name); // записываем знания в результат 
                //WhatWeMustToKnowDisconnect(relationИ.nodeFrom, знания, ref used); //идем дальше по онтологии 
            }
            //used.Add(node.id, node.name); //присваиваем использованную вершину 
        }

        public void WhatWeMustToKnow(int id, ref List<string> знания) //функция, которая возвращает знания, которые нам нужно знать для изучения дисциплины 
        {
            Node node = FindNodeID(id); //находим вершину по id
            List<Relation> relationsТЗ = FindNodeRelations(node, "требует_знания"); //находим все отношения вершины с именем "требует_знания"  

            foreach (Relation rl in relationsТЗ) // проходимся по всем отношениям вершины  "требует_знания"
            {
                Node node1 = FindNodeID(rl.nodeTo);
                знания.Add(node1.name);
            }
        }

        public void WhatWeKnow(int id, ref Dictionary<int,string> знания0) // выводит знания, которые мы изучили 
        {
            Node node = FindNodeID(id);
            List<Relation> relations = FindNodeRelations(node, "изучает");

            foreach(Relation rl in relations)
            {
                Node nodeTo= FindNodeID(rl.nodeTo);
                знания0.Add(nodeTo.id,nodeTo.name);
            }
        }

        public List<string> WhatWeKnowAboutKnowledge(int id, Dictionary<int,string> знания)
        {
            bool APO = true; // для проверки на "a_part_of"
            bool Level = true;
            List<string> знанияНужны = new List<string>();
            List<int> знанияId = new List<int>();
            Node node = FindNodeID(id); //дисциплина
            List<Relation> relationsТЗ = FindNodeRelations(node, "требует_знания"); //знания у ноды дисциплины
            foreach (Relation rl in relationsТЗ)
            {
                Node знание = FindNodeID(rl.nodeTo); // нода знания
                Relation ЗнаниеАРО = FindNodeRelation(знание, "a_part_of"); //нашли связь "a_part_of"
                if (!(ЗнаниеАРО is null)) //проверка на  "a_part_of"
                {
                    List<int> ApoId = new List<int>(); //все id знаний над нужной
                    ApoId.Add(знание.id);
                    while (!(ЗнаниеАРО is null)) //пока у нас есть a_part_of
                    {
                        знание = FindNodeID(ЗнаниеАРО.nodeTo); //находим следующую ноду
                        ЗнаниеАРО = знание.nodeRelations.Find( //находим все отношения "a_part_of" вершины, отличные от того, откуда мы пришли   
                        delegate (Relation rll)
                        {
                            return (rll.name == "a_part_of" && знание.id != rll.nodeTo);
                        });
                        ApoId.Add(знание.id);
                    }
                    foreach (int apo in ApoId)
                    {
                        if (знания.Keys.Contains(apo))
                        {
                            APO = false;
                            break;
                        }
                    }
                }
                
                //знание = FindNodeID(rl.nodeTo); // нода знания
                //Relation ЗнаниеLevel = FindNodeRelation(знание, "level_up");
                //if (!(ЗнаниеLevel is null)) //проверка на  "level_up"
                //{
                //    List<int> LevelID = new List<int>(); //все id знаний над нужной
                //    LevelID.Add(знание.id);
                //    while (!(ЗнаниеLevel is null)) //пока у нас есть level_up
                //    {
                //        знание = FindNodeID(ЗнаниеLevel.nodeTo); //находим следующую ноду
                //        ЗнаниеLevel = знание.nodeRelations.Find( //находим все отношения "level_up" вершины, отличные от того, откуда мы пришли   
                //        delegate (Relation rll)
                //        {
                //            return (rll.name == "level_up" && знание.id != rll.nodeTo);
                //        });
                //        LevelID.Add(знание.id);
                //    }
                //    foreach (int apo in LevelID)
                //    {
                //        if (знания.Keys.Contains(apo))
                //        {
                //            APO = false;
                //            break;
                //        }
                //    }
                //}


                if (!знания.Keys.Contains(rl.nodeTo) && APO && Level) {
                    Node nodeTo = FindNodeID(rl.nodeTo);
                    знанияНужны.Add(nodeTo.name);
                }
                APO = true;
            }
            return знанияНужны;
        }


        public Dictionary<int, string> WhatWeMustToKnowUnlucky(List<int> ids)
        {
            Dictionary<int, string> used_дисциплины = new Dictionary<int, string>();
            bool skip_iteration = false;
            foreach (int id in ids)
            {
                Node node_знание = FindNodeID(id); //находим вершину по id

                Relation relation = FindNodeRelation(node_знание, "изучает"); //находим отношение "a_part_of" вершины

                Relation relationA_Part_Of = FindNodeRelation(node_знание, "a_part_of"); //находим отношение "a_part_of" вершины

                if (!(relationA_Part_Of is null)) //если есть отношения a_part_of, то проходимя по ветке a_part_of до конца
                {
                    Node nodeToAPO = new Node(1, "0");

                    while (!(relationA_Part_Of is null)) //пока у нас есть a_part_of
                    {
                        nodeToAPO = FindNodeID(relationA_Part_Of.nodeTo); //находим вершину nodeTo
                        if (ids.Contains(nodeToAPO.id) && nodeToAPO.id != node_знание.id) //Выходим из цикла, если у нас базовый уровень (потому что продвинутый круче)
                        {
                            skip_iteration = true;
                            break; 
                        }
                        //relation = FindNodeRelation(nodeToAPO, "изучает"); //находим отношение "изучает" вершины 
                        //if (!(relation is null)) break;
                        relationA_Part_Of = nodeToAPO.nodeRelations.Find( //находим все отношения "a_part_of" вершины, отличные от того, откуда мы пришли   
                        delegate (Relation rll)
                        {
                            return (rll.name == "a_part_of" && nodeToAPO.id != rll.nodeTo);
                        });
                    }
                    if (skip_iteration) //пропускаем базовый уровень
                    {
                        skip_iteration = false;
                        continue;
                    }
                    if (relation is null)
                    {
                        relationA_Part_Of = FindNodeRelation(node_знание, "a_part_of");
                        while (!(relationA_Part_Of is null)) //пока у нас есть a_part_of
                        {
                            nodeToAPO = FindNodeID(relationA_Part_Of.nodeTo); //находим вершину nodeTo
                            relation = FindNodeRelation(nodeToAPO, "изучает"); //находим отношение "изучает" вершины 
                            if (!(relation is null)) break;
                            relationA_Part_Of = nodeToAPO.nodeRelations.Find( //находим все отношения "a_part_of" вершины, отличные от того, откуда мы пришли   
                            delegate (Relation rll)
                            {
                                return (rll.name == "a_part_of" && nodeToAPO.id != rll.nodeTo);
                            });
                        }
                        node_знание = nodeToAPO;
                        relation = FindNodeRelation(node_знание, "изучает"); //находим отношение "a_part_of" вершины
                    }
                }

                Node node_дисциплина = FindNodeID(relation.nodeFrom);
                if (!used_дисциплины.Keys.Contains(node_дисциплина.id))
                    used_дисциплины.Add(node_дисциплина.id, node_дисциплина.name);
            }
            return used_дисциплины;

        }

        public List<string> WhatWeKnowAboutAccomodation (List<int> ids_дисциплины, List<int> ids_знания)
        {
            List<string> used_дисциплины = new List<string>();
            List<Node> Все_Дисциплины_Ноды = new List<Node>();

            for (int i = 0; i < nodes.Count; i++)
                if (nodes[i].type == 1)
                    Все_Дисциплины_Ноды.Add(nodes[i]);


            foreach (Node node in Все_Дисциплины_Ноды) //перебираем все ноды в графе
            {
                if (ids_дисциплины.Contains(node.id)) continue; //пропускаем итерацию, если нода уже выбрана

                int k = 0;
                List<Relation> relationsТЗ = FindNodeRelations(node, "требует_знания"); //знания у ноды дисциплины
                foreach (Relation rl in relationsТЗ) //перебираем все знания, которые необходиме ноде
                {
                    Node знание = FindNodeID(rl.nodeTo); // нода знания

                    
                    Relation Знание_изучает = FindNodeRelation(знание, "изучает"); //смотрим есть ли у нее связь "изучает"

                    if (Знание_изучает is null)
                    {
                        Relation ЗнаниеАРО = FindNodeRelation(знание, "a_part_of"); //нашли связь "a_part_of"
                        if (!(ЗнаниеАРО is null)) //проверка на  "a_part_of"
                        {
                            Node nodeToAPO = new Node(1, "0");
                            while (!(ЗнаниеАРО is null)) //пока у нас есть a_part_of
                            {
                                знание = FindNodeID(ЗнаниеАРО.nodeTo); //находим следующую ноду
                                ЗнаниеАРО = знание.nodeRelations.Find( //находим все отношения "a_part_of" вершины, отличные от того, откуда мы пришли   
                                delegate (Relation rll)
                                {
                                    return (rll.name == "a_part_of" && знание.id != rll.nodeTo);
                                });
                            }
                        }
                    }




                    if (ids_знания.Contains(знание.id))
                    {
                        k++;
                    }
                }
                if (k == relationsТЗ.Count())
                {
                    used_дисциплины.Add(node.name);
                }
            }
            return used_дисциплины;
        } 
    } 
}   


