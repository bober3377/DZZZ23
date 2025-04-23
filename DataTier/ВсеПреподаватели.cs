using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier
{
    public class ВсеПреподаватели
    {
        public static List<Преподаватель> ПолучитьВсеПреподавателиИзФайла()
        {
            List<Преподаватель> list = new List<Преподаватель>();
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string FilePath = openFileDialog.FileName;
                using (StreamReader sreader = new StreamReader(FilePath, Encoding.UTF8))
                {
                    while (!sreader.EndOfStream)
                    {
                        string[] line = sreader.ReadLine().Split('*');

                        Преподаватель преподаватель = new Преподаватель()
                        {
                            ФИО = line[0].Trim(),
                            Группа = line[1].Trim(),
                            Курс = int.Parse(line[2].Trim()),
                            КоличествоЗадолженностей = int.Parse(line[3].Trim())
                        };
                        list.Add(преподаватель);
                    }
                }
            }
            return list;
        }
    }
}