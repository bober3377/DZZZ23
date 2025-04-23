using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;

namespace LogicTier
{
    public class Университет
    {
        private List<ПреподавательПозиция> _преподавателиПозиции;

        public Университет(List<ПреподавательПозиция> позиции)
        {
            _преподавателиПозиции = позиции;
        }

        public List<ПреподавательПозиция> СписокПреподавателей => _преподавателиПозиции;

       
        public Dictionary<string, int> СуммаЗадолженностейПоГруппам =>
            _преподавателиПозиции
                .GroupBy(p => p.Группа)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.КоличествоЗадолженностей));

        
        public Dictionary<int, double> СреднееЗадолженностейПоКурсам =>
            _преподавателиПозиции
                .GroupBy(p => p.Курс)
                .ToDictionary(g => g.Key, g => g.Average(p => p.КоличествоЗадолженностей));
    }
}
