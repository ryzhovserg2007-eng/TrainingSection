using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model.Core;
using Model.Data;

namespace TrainingSection
{
    public partial class MainWindow : Window
    {
        private Serializer currentSerializer = new JsonSerializer();
        private string dataPath = "Data";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.IO.Directory.Exists(dataPath))
                System.IO.Directory.CreateDirectory(dataPath);

            DataInitializer.Initialize();

            // Заполняем списки
            cbGroups.ItemsSource = DataInitializer.Groups;
            cbAthletesFeedback.ItemsSource = DataInitializer.Groups.SelectMany(g => g.Спортсмены).Distinct().ToList();
            cbGroupsFeedback.ItemsSource = DataInitializer.Groups;

            LoadTrainersStats();
        }

        private void cbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGroups.SelectedItem is TrainingGroup group)
            {
                dgvAthletes.ItemsSource = group.Спортсмены;
            }
        }

        private void btnAddAthlete_Click(object sender, RoutedEventArgs e)
        {
            if (cbGroups.SelectedItem is TrainingGroup group)
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text)) return;

                char gender = cbGender.Text == "M" ? 'M' : 'F';
                int age = int.TryParse(txtAge.Text, out int a) ? a : 18;

                var athlete = new Athlete(txtFirstName.Text, txtLastName.Text, txtPatronymic.Text, age, gender);
                group.ДобавитьСпортсмена(athlete);

                dgvAthletes.ItemsSource = null;
                dgvAthletes.ItemsSource = group.Спортсмены;

                SaveData();
            }
        }

        private void btnRemoveAthlete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvAthletes.SelectedItem is Athlete athlete && 
                cbGroups.SelectedItem is TrainingGroup group)
            {
                group.УдалитьСпортсмена(athlete);
                dgvAthletes.ItemsSource = null;
                dgvAthletes.ItemsSource = group.Спортсмены;
                SaveData();
            }
        }

        private void btnGiveFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (cbAthletesFeedback.SelectedItem is Athlete athlete &&
                cbGroupsFeedback.SelectedItem is TrainingGroup group &&
                int.TryParse(txtRating.Text, out int rating))
            {
                try
                {
                    var feedback = new Feedback(athlete, group.Тренер, group, rating, 10);
                    group.ДобавитьОтзыв(feedback);
                    MessageBox.Show("Отзыв успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    SaveData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadTrainersStats()
        {
            dgvTrainers.ItemsSource = DataInitializer.Coaches.Select(c => new
            {
                ФИО = c.GetFullName(),
                Рейтинг = c.Rating,
                Групп = c.TrainingGroups.Count,
                Спортсменов = c.TrainingGroups.Sum(g => g.Спортсмены.Count)
            }).ToList();
        }

        private void SaveData()
        {
            string filePath = System.IO.Path.Combine(dataPath, "groups" + currentSerializer.GetFileExtension());
            currentSerializer.SaveToFile(filePath, DataInitializer.Groups);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadTrainersStats();
        }
    }
}
