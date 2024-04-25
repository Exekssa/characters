using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.Windows.Controls.Primitives;

namespace Characters
{

    public partial class MainWindow : Window

    {

        private Character firstCharacter;
        private Character secondCharacter;
        private Character thirdCharacter;
        private Character fourthCharacter;
        private Character fifthCharacter;
        List<Character> characters = new List<Character>();
        private int next = 1;
        private int previous = 0;
        private string tempImgPath = "ChoosePhoto/3.jpg";
     


        public MainWindow()
        {
            InitializeComponent();

            firstCharacter = new Character("Маркс", "Фортуна", new DateTime(1795, 3, 31), "капитан корабля", 250000, "наблюдение за дельфинами", "«Логика может привести Вас от пункта А к пункту Б, а воображение — куда угодно.»", "Img/Marks.png");
            secondCharacter = new Character("Луи", "Оливьер", new DateTime(1783, 12, 6), "первый помощник капитана", 150000, "сочинение стихов", "«Свобода ничего не стоит, если она не включает в себя свободу ошибаться.»", "Img/Lois.jpg");
            thirdCharacter = new Character("Ричард", "Кроули", new DateTime(1782, 1, 25), "квартирмейстер", 120000, "пение", "«Есть только один способ избежать критики: ничего не делайте, ничего не говорите и будьте никем.»", "Img/Richard.jpg");
            fourthCharacter = new Character("Аннабель", "Форест", new DateTime(1790, 11, 11), "судовой врач", 140000, "астрология", "«У всего есть своя красота, но не каждый может ее увидеть.»", "Img/Ana.jpg");
            fifthCharacter = new Character("Борис", "Кафрен", new DateTime(1799, 3, 7), "штурман", 100000, "шахматы", "«Ни разу не упасть — не самая большая заслуга в жизни. Главное каждый раз подниматься.»", "Img/Boris.jpg");

            characters.Add(firstCharacter);
            characters.Add(secondCharacter);
            characters.Add(thirdCharacter);
            characters.Add(fourthCharacter);
            characters.Add(fifthCharacter);

            ShowFirstCharacter();


        }

        private void ShowFirstCharacter()
        {
            characterInfo.Text = characters[0].ToString();
            characterPhoto.Source = new BitmapImage(new Uri(characters[0].ImagePath, UriKind.RelativeOrAbsolute));
        }

        private void Search_Click(object sender, EventArgs e)
        {
        
            string searchData = textBoxSearch.Text.Replace(" ", "").ToLower();
            foreach (var searchCharacter in characters)
            {
                string characterData = searchCharacter.Name.ToLower() + searchCharacter.Surname.ToLower();
                
                if (searchData== characterData)
                {
                    int indexOfCharacter = characters.IndexOf(searchCharacter);
                    next = indexOfCharacter + 1;
                    previous = indexOfCharacter - 1;
                    characterInfo.Text = characters[indexOfCharacter].ToString();
                    characterPhoto.Source = new BitmapImage(new Uri(characters[indexOfCharacter].ImagePath, UriKind.Relative));
                }
            }
        }

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            characters.Sort(Character.SortByName);
            ShowFirstCharacter();
            ChangeColorOfSortButton(buttonName);           
        }

        private void SortBySurName_Click(object sender, RoutedEventArgs e)
        {
            characters.Sort(Character.SortBySurname);
            ShowFirstCharacter();
            ChangeColorOfSortButton(buttonSurname);          
        }

        private void SortByProfession_Click(object sender, RoutedEventArgs e)
        {
            characters.Sort(Character.SortByProfession);
            ShowFirstCharacter();
            ChangeColorOfSortButton(buttonProfession);  
        }

        private void SortByYear_Click(object sender, RoutedEventArgs e)
        {
            characters.Sort(Character.SortByYear);
            ShowFirstCharacter();
            ChangeColorOfSortButton(buttonYear);
        }

        private void ChangeColorOfSortButton(Button activeButton)
        {
            foreach(var sortButton in SortButtons.Children.OfType<Button>())
            {
                if(sortButton.Name != activeButton.Name)
                {
                    sortButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7ABFFF"));
                    sortButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#173559"));
                }
            }

            activeButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4A8CFF"));
            activeButton.Foreground = Brushes.White;
            next = 1;
            previous = 0;
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (characters.Count == 0 || next == characters.Count)
            {
                return;
            }
        
            characterInfo.Text = characters[next].ToString();
            characterPhoto.Source = new BitmapImage(new Uri(characters[next].ImagePath, UriKind.Relative));
            previous = next - 1;
            next += 1;
     
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (characters.Count == 0 || previous == -1)
            {
                return;
            }
        
            characterInfo.Text = characters[previous].ToString();
            characterPhoto.Source = new BitmapImage(new Uri(characters[previous].ImagePath, UriKind.Relative));
            next = previous + 1;
            previous -= 1;
     
        }

        private void AddCharacter_Click(object sender, RoutedEventArgs e)
        {
            AddCharacterPopup.IsOpen= true;        
        }

        private bool AreAllTextBoxesCorrect()
        {
            foreach (var child in TextBoxesNewCharacter.Children)
            {
                if (child is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    return false;
                }      
            }

            return true; 
        }

        private void AddCharacterToList_Click(object sender, RoutedEventArgs e)
        {
            bool isInputValid = AreAllTextBoxesCorrect();

            if (isInputValid)
            {
                Character newCharacter = new Character();

                newCharacter.Name = textBoxName.Text;
                newCharacter.Surname = textBoxSurName.Text;

                if (!DateTime.TryParse(textBoxDate.Text, out _))
                {
                    textBoxDate.ToolTip = "Введите дату в верном формате!";
                    textBoxDate.Background = Brushes.HotPink;
                    isInputValid = false;
                }
                else
                {
                    newCharacter.DateOfBirth = DateTime.Parse(textBoxDate.Text);
                    textBoxDate.ToolTip = null;
                    textBoxDate.Background = Brushes.Transparent; 
                }

                if (!double.TryParse(textBoxSalary.Text, out _))
                {
                    textBoxSalary.ToolTip = "Введите числовое значение для зарплаты!";
                    textBoxSalary.Background = Brushes.HotPink;
                    isInputValid = false;
                }
                else
                {
                    newCharacter.Salary = double.Parse(textBoxSalary.Text);
                    textBoxSalary.ToolTip = null;
                    textBoxSalary.Background = Brushes.Transparent;
                }

                newCharacter.Profession = textBoxProfession.Text;
                newCharacter.Hobby = textBoxHobby.Text;
                newCharacter.FavoriteAphorism = textBoxAphorism.Text;

                newCharacter.ImagePath = tempImgPath;
              
                characters.Add(newCharacter);

                if(characters.Count == 1)
                {
                    characterInfo.Text = characters[0].ToString();
                    characterPhoto.Source = new BitmapImage(new Uri(characters[0].ImagePath, UriKind.Relative));
                    next = 1;
                    previous = 0;
    }

                AddCharacterPopup.IsOpen = false;
                MessageBox.Show("Вы добавили нового персонажа!");

                foreach (var child in TextBoxesNewCharacter.Children)
                {
                    if (child is TextBox textBox)
                    {
                        textBox.Text = null;
                    }
                }
                tempImgPath = "ChoosePhoto/3.jpg";

            }

            if (!isInputValid)
            {

                errorMessage.Visibility = Visibility.Visible;
                AddCharacterPopup.IsOpen = true;
            }
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Все файлы (*.*)|*.*";

            //if (openFileDialog.ShowDialog() == true)
            //{
                
            //    string filePath = openFileDialog.FileName;
            //    string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Img";
            //    MessageBox.Show($"projectRoot: {projectRoot}");

            //    string fileName = System.IO.Path.GetFileName(filePath);
            //    string destinationFullPath = System.IO.Path.Combine(projectRoot, fileName);
            //    MessageBox.Show($"Full Path: {destinationFullPath}");
            //    MessageBox.Show($"Project Folder Path: {projectRoot}");


            //        using (FileStream sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            //        {
            //            using (FileStream destinationStream = new FileStream(destinationFullPath, FileMode.Create, FileAccess.Write))
            //            {
                        
            //                sourceStream.CopyTo(destinationStream);
            //            }
            //        }
    
            //    tempImgPath = @"Img/" + fileName;
            //    MessageBox.Show($"ImagePath: {tempImgPath}");


            OpenFileDialog openFileDialog = new OpenFileDialog();
            Application.Current.MainWindow.Topmost = true;
            openFileDialog.Filter = "Изображения (*.png;*.jpg;*.jpeg;*.gif)|*.png;*.jpg;*.jpeg;*.gif|Все файлы (*.*)|*.*";
            Application.Current.MainWindow.Topmost = false;

           
            string relativePath = "ChoosePhoto";
            DirectoryInfo parentDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent;
            string absolutePath = System.IO.Path.Combine(parentDirectory.FullName, relativePath);
         

            openFileDialog.InitialDirectory = absolutePath;

            if (openFileDialog.ShowDialog() == true)
            {
                tempImgPath = "ChoosePhoto/" + System.IO.Path.GetFileName(openFileDialog.FileName);
               
            }

        }
                        
     

        private void DeleteCharacter_Click(object sender, RoutedEventArgs e)
        {
            if(characters.Count == 0)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить информацию о данном персонаже?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                characters.Remove(characters[next - 1]);

                if (characters.Count != 0)
                {
                   characterInfo.Text = characters[0].ToString();
                   characterPhoto.Source = new BitmapImage(new Uri(characters[0].ImagePath, UriKind.Relative));
                   next = 1;

                }

                else
                {
                    characterInfo.Text = "Нет персонажей для отображения!";
                    characterPhoto.Source = new BitmapImage(new Uri("ChoosePhoto/22.png", UriKind.RelativeOrAbsolute));
                }      
            }
        }

        private void Close_form_Click(object sender, RoutedEventArgs e)
        {
            AddCharacterPopup.IsOpen = false;

            foreach (var child in TextBoxesNewCharacter.Children)
            {
                if (child is TextBox textBox)
                {
                    textBox.Text = null;
                }
            }
            tempImgPath = null;
        }

    }
}

