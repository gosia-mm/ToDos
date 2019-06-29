using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.DependencyInjection;
using ToDos.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToDoPage2 : ContentPage
	{
        ToDo newTodo;
        public AddToDoPage2 ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            newTodo = new ToDo() { Title = "", Description = "", Category = "Osobiste" };
            BindingContext = newTodo;
        }


        async private void Button_Clicked(object sender, EventArgs e)
        {
            //App.DB.addOrUpdateToDoAsync(new ToDo() { Title = titleEdit.Text , Description = descEdit.Text });

            await App.DB.addOrUpdateToDoAsync(newTodo);
            await Navigation.PopAsync();
        }

        private async void BtnChooseImg_Clicked(object sender, EventArgs e)
        {
            btnChooseImg.IsEnabled = false;
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                var imgLocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), newTodo.Image + ".jpg");

                var imgMemory = new MemoryStream(); 
                stream.CopyTo(imgMemory); // zapisanie obrazka do pamięci RAM
                    
                File.WriteAllBytes(imgLocalPath, imgMemory.ToArray()); // pobranie danych z pamięci RAM

                //imgTodo.Source = ImageSource.FromStream(() => imgMemory);
                imgTodo.Source = ImageSource.FromFile(imgLocalPath); 
                imgTodo.BackgroundColor = Color.Gray;
            }

            btnChooseImg.IsEnabled = true;
        }
    }
}