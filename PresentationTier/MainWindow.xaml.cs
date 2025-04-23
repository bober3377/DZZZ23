using System.IO;
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
using DataTier;
using LogicTier;
using MathNet.Numerics;
using Microsoft.Win32;

namespace PresentationTier
{
    public partial class MainWindow : System.Windows.Window
    {
        private Университет университет;
        private string selectedFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_open_file_Click(object sender, RoutedEventArgs e)
        {
            List<Преподаватель> преподавателиИзФайла = ВсеПреподаватели.ПолучитьВсеПреподавателиИзФайла();

            if (преподавателиИзФайла.Count == 0)
            {
                MessageBox.Show("Нет преподавателей в файле!");
                return;
            }

            List<ПреподавательПозиция> позиции = преподавателиИзФайла
                .Select(p => new ПреподавательПозиция(p))
                .ToList();

            университет = new Университет(позиции);
            this.DataContext = университет;
        }

        private void btn_add_to_file_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Выберите файл для сохранения"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                selectedFilePath = saveFileDialog.FileName;
                MessageBox.Show($"Файл выбран: {selectedFilePath}");
            }

            string фио = FIO.Text;
            string группа = (myComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string курс = Kaf.Text;
            string задолженности = ZP.Text;

            if (string.IsNullOrWhiteSpace(фио) ||
                string.IsNullOrWhiteSpace(группа) ||
                string.IsNullOrWhiteSpace(курс) ||
                string.IsNullOrWhiteSpace(задолженности))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!int.TryParse(курс, out int course) || course <= 0)
            {
                MessageBox.Show("Введите корректное значение для курса (положительное число).");
                return;
            }

            if (!int.TryParse(задолженности, out int debts) || debts < 0)
            {
                MessageBox.Show("Введите корректное значение для задолженностей (неотрицательное число).");
                return;
            }

            string строкаДляЗаписи = $"{фио}*{группа}*{course}*{debts}";
            File.AppendAllText(selectedFilePath, строкаДляЗаписи + Environment.NewLine);
            MessageBox.Show("Данные успешно добавлены!");

            FIO.Clear();
            myComboBox.SelectedIndex = -1;
            Kaf.Clear();
            ZP.Clear();
        }

        private void btn_delete_file_Click(object sender, RoutedEventArgs e)
        {
            if (MainList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выделите элементы для удаления.", "Предупреждение");
                return;
            }

            var универ = (Университет)DataContext;
            var itemsToRemove = MainList.SelectedItems.Cast<ПреподавательПозиция>().ToList();

            foreach (var item in itemsToRemove)
            {
                универ.СписокПреподавателей.Remove(item);
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Текстовые файлы(*.txt)|*.txt|Скрипты(*.sql)|*.sql|Документы(*.docx)|*.docx"
            };

            if (sfd.ShowDialog() == true)
            {
                string FilePath = sfd.FileName;
                using (StreamWriter swriter = new StreamWriter(FilePath))
                {
                    var универ1 = (Университет)DataContext;
                    foreach (var препод in универ1.СписокПреподавателей)
                    {
                        string строка = $"{препод.ФИО}*{препод.Группа}*{препод.Курс}*{препод.КоличествоЗадолженностей}";
                        swriter.WriteLine(строка);
                    }
                }
                MessageBox.Show($"Данные сохранены в файл {sfd.FileName}");
            }
            else
            {
                MessageBox.Show("Пользователь отказался от окна сохранения");
            }
        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myComboBox.SelectedItem != null)
            {
                var selectedItem = (ComboBoxItem)myComboBox.SelectedItem;
                MessageBox.Show($"Выбрана группа: {selectedItem.Content}");
            }
        }
    }
}