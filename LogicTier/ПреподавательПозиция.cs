using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataTier;

namespace LogicTier
{
    public class ПреподавательПозиция
    {
        private Преподаватель _преподаватель;

        public ПреподавательПозиция(Преподаватель p)
        {
            _преподаватель = p;
        }

        public string ФИО
        {
            get { return _преподаватель.ФИО; }
            set { _преподаватель.ФИО = value; }
        }

        public string Группа
        {
            get { return _преподаватель.Группа; }
            set { _преподаватель.Группа = value; }
        }

        public int Курс
        {
            get { return _преподаватель.Курс; }
            set { _преподаватель.Курс = value; }
        }

        public int КоличествоЗадолженностей
        {
            get { return _преподаватель.КоличествоЗадолженностей; }
            set { _преподаватель.КоличествоЗадолженностей = value; }
        }

        public string ПредставлениеПреподавателя =>
            $"{ФИО} | Группа: {Группа} | Курс: {Курс} | Задолженностей: {КоличествоЗадолженностей}";
    }
}
